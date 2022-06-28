using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;


namespace Con_pos
{
    public class DBMySql
    {
        MySqlConnection conn = new MySqlConnection($"server={Config.Server};uid={Config.UserID};" +
            $"pwd={Config.UserPassword};database={Config.Database};pooling=false;allow user variables=true");
        MySqlDataAdapter adpt;
        MySqlCommand cmd;

        MySqlConnection conn2 = new MySqlConnection($"server={Config2.Server};uid={Config2.UserID};" +
            $"pwd={Config2.UserPassword};database={Config2.Database};pooling=false;allow user variables=true");
        MySqlDataAdapter adpt2;
        MySqlCommand cmd2;

        public void Connection()
        {
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
            conn.Close();
        }

        public DataSet SelectAll(string table)
        {
            try
            {
                DataSet ds = new DataSet();

                string sql = $"SELECT * FROM {table}";
                adpt = new MySqlDataAdapter(sql, conn);
                adpt.Fill(ds, table);
                if (ds.Tables.Count > 0)
                {
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

        public DataSet SelectDetail(string condition, string table, string where = "")
        {
            try
            {
                DataSet ds = new DataSet();

                string sql = $"SELECT {condition} FROM {table} {where}";
                adpt = new MySqlDataAdapter(sql, conn);
                adpt.Fill(ds, table);
                if (ds.Tables.Count > 0)
                {
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

        public void Insert(string table, string value1)
        {
            try
            {
                conn2.Open();
                string sql = $"INSERT INTO {table}(Mph,Mname) VALUES ({value1})";
                //INSERT INTO user_info VALUES ('user1', 'j', 'ella', '1993/06/18', 'user1234', 1000000000)
                cmd2 = new MySqlCommand(sql, conn2);
                cmd2.ExecuteNonQuery();
                conn2.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }
        public DataSet SelectAll2(string table)//회원가입에서의 조회
        {
            try
            {
                DataSet ds = new DataSet();

                string sql = $"SELECT * FROM {table}";
                adpt2 = new MySqlDataAdapter(sql, conn2);
                adpt2.Fill(ds, table);
                if (ds.Tables.Count > 0)
                {
                    return ds;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }
        public void Update(string table, string setvalue, string wherevalue = "")
        {
            try
            {
                conn.Open();
                string sql = $"UPDATE {table} SET {setvalue} {wherevalue}";
                //UPDATE user_info SET firt_name='lee' WHERE user_id='shotslove'
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

        public void DeleteAll(string table)
        {
            try
            {
                conn.Open();
                string sql = $"DELETE FROM {table}";
                //DELETE FROM user_info WHERE user_id='user1'
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

        public void DeleteDetail(string table, string wherecol, string wherevalue)
        {
            try
            {
                conn.Open();
                string sql = $"DELETE FROM {table} WHERE {wherecol}='{wherevalue}'";
                //DELETE FROM user_info WHERE user_id='user1'
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }
    }
}
