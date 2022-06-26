using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Diagnostics;

namespace Con_pos
{
    public class MySQLManager
    {
        public static MySqlConnection connection;

        public static bool DataSaveResult;
        public static bool DataSearchResult;
        public void Initialize()
        {
            //Debug.WriteLine("DataBase Initialize");

            string connectionPath = $"SERVER=localhost;DATABASE=ConStore;UID=root;PASSWORD=dntls88";
            MySQLManager.connection = new MySqlConnection(connectionPath);
        }

        // Create MySqlCommand
        public MySqlCommand CreateCommand(string query)
        {
            MySqlCommand command = new MySqlCommand(query, MySQLManager.connection);
            return command;
        }

        // DataBase Connection
        public bool OpenMySqlConnection()
        {
            try
            {
                MySQLManager.connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        //Debug.WriteLine("Unable to Connect to Server");
                        break;
                    case 1045:
                        //Debug.WriteLine("Please check your ID or PassWord");
                        break;
                }
                return false;
            }
        }

        // DataBase Close
        public bool CloseMySqlConnection()
        {
            try
            {
                MySQLManager.connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        // Queyr Executer(Insert, Delete, Update ...)
        public void MySqlQueryExecuter(string userQuery)
        {
            string query = userQuery;

            if (OpenMySqlConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, MySQLManager.connection);

                if (command.ExecuteNonQuery() == 1)
                {
                    //Debug.WriteLine("값 저장 성공");
                    MySQLManager.DataSaveResult = true;
                }
                else
                {
                    //Debug.WriteLine("값 저장 실패");
                    MySQLManager.DataSaveResult = false;
                }

                CloseMySqlConnection();
            }
        }

        public List<string>[] Select(string tableName, int columnCnt, string id, string pw)
        {
            string query = "SELECT * FROM" + " " + tableName;

            List<string>[] element = new List<string>[columnCnt];

            for (int index = 0; index < element.Length; index++)
            {
                element[index] = new List<string>();
            }

            if (this.OpenMySqlConnection() == true)
            {
                MySqlCommand command = CreateCommand(query);
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    element[0].Add(dataReader["Eid"].ToString());
                    element[1].Add(dataReader["Epwd"].ToString());
                }

                // 추가된 코드
                if (element != null)
                {
                    for (int i = 0; i < element[0].Count; i++)
                    {
                        if (element[0][i].Contains(id))
                        {
                            for (int j = 0; j < element[1].Count; i++)
                            {
                                if (element[1][i].Contains(pw))
                                {
                                    MySQLManager.DataSearchResult = true;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }

                dataReader.Close();
                this.CloseMySqlConnection();

                return element;
            }
            else
            {
                return null;
            }
        }
    }
}