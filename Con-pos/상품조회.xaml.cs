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
    /// 상품조회.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 상품조회 : Page
    {
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        public 상품조회()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/조회업무.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//전체조회
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM Shopproduct";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                shopPDGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//검색과 이벤트 텍스트블록에 출력
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT SPDnum, SPDname, SPDcount, SPDmaker FROM Shopproduct where SPDnum = '" + PDnum.Text + "';";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                selectPDGrid.ItemsSource = ds.Tables[0].DefaultView;

                string sql2 = "SELECT SPDevent FROM Shopproduct where SPDnum = '" + PDnum.Text + "';";
                MySqlCommand cmd = new MySqlCommand(sql2, conn);
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PDEvent.Text = (reader["SPDevent"].ToString());
                    }
                }
                conn.Close();
            }
        }
    }
}
