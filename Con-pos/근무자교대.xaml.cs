using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Con_pos
{
    /// <summary>
    /// 근무자교대.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 근무자교대 : Page
    {
        public static string worker;
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        public 근무자교대()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //근무자교대
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT Empname FROM Empmem where Empph = '" + wokerNum.Text + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        worker = (reader["Empname"].ToString());
                    }
                }
                conn.Close();
            }
            MessageBox.Show("입력완료! 근무자교대를 눌러주세요.");
        }
    }
}
