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
    /// 폐기등록.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 폐기등록 : Page
    {
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        public 폐기등록()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/main.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//선택상품정보조회
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT SPDnum, SPDname, SPDprice, SPDmaker FROM ShopProduct where SPDnum = '" + Pnum.Text + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        showNum.Text = (reader["SPDnum"].ToString());//Hidden
                        showname.Text = (reader["SPDname"].ToString());//Hidden
                        showprice.Text = (reader["SPDprice"].ToString());//Hidden
                        showCompany.Text = (reader["SPDmaker"].ToString());
                    }
                }
            }
        }

        private void Allgrid_Loaded(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM trashproduct";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                Allgrid.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//폐기
        {
            string PaymentTime = DateTime.Now.ToString();
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("INSERT INTO trashproduct(TRPDnum, TRPDname, TRPDcount, TRPDprice, TRPDmaker, TRDate) values('" + Pnum.Text + "','" + showname.Text + "'," +
                    "'" + Pcount.Text + "','" + showprice.Text + "','" + showCompany.Text + "','" + PaymentTime + "')", conn);
                msc.ExecuteNonQuery();
                //추가하고 새로고침
                string sql = "SELECT * FROM trashproduct";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                Allgrid.ItemsSource = ds.Tables[0].DefaultView;
                //폐기후 재고변경
                MySqlCommand msc2 = new MySqlCommand("Update ShopProduct Set SPDcount=SPDcount-'" + Pcount.Text + "' Where SPDnum ='" + Pnum.Text + "';", conn);
                msc2.ExecuteNonQuery();
                MessageBox.Show("등록되었습니다.");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//취소
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("DELETE FROM trashproduct where TRPDnum = '" + Pnum.Text + "'", conn);
                msc.ExecuteNonQuery();
                //추가하고 새로고침
                string sql = "SELECT * FROM trashproduct";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                Allgrid.ItemsSource = ds.Tables[0].DefaultView;
                //취소후 재고변경
                MySqlCommand msc2 = new MySqlCommand("Update ShopProduct Set SPDcount=SPDcount+'" + Pcount.Text + "' Where SPDnum ='" + Pnum.Text + "';", conn);
                msc2.ExecuteNonQuery();
                MessageBox.Show("취소되었습니다.");
            }
        }
    }
}

