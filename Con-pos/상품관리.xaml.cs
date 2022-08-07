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
    /// 상품관리.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 상품관리 : Page
    {
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        public 상품관리()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/경영메뉴.xaml", UriKind.Relative));
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
                string sql = "SELECT * FROM Shopproduct where SPDnum = '" + PDnum.Text + "';";
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

        private void Button_Click_3(object sender, RoutedEventArgs e)//갯수 수정
        {
            if (PDcount.Text == "")
            {
                MessageBox.Show("수량을 입력하세요.");
            }
            else
            {
                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();
                    MySqlCommand msc = new MySqlCommand("Update ShopProduct Set SPDcount='" + PDcount.Text + "' Where SPDnum ='" + PDnum.Text + "';", conn);
                    msc.ExecuteNonQuery();

                    string sql = "SELECT * FROM ShopProduct Where SPDnum ='" + PDnum.Text + "';";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    selectPDGrid.ItemsSource = ds.Tables[0].DefaultView;
                    MessageBox.Show("상품개수 수정완료!");
                }
            }   
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)//이벤트수정
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("Update ShopProduct Set SPDevent='" + PDEvent.Text + "' Where SPDnum ='" + PDnum.Text + "';", conn);
                msc.ExecuteNonQuery();

                string sql = "SELECT * FROM ShopProduct Where SPDnum ='" + PDnum.Text + "';";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                selectPDGrid.ItemsSource = ds.Tables[0].DefaultView;
                MessageBox.Show("이벤트수정완료!");

                string sql2 = "SELECT * FROM ShopProduct";
                MySqlDataAdapter da2 = new MySqlDataAdapter(sql2, conn);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                shopPDGrid.ItemsSource = ds2.Tables[0].DefaultView;
                
            }
        }
    }
}
