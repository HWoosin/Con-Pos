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
    /// 회원관리.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 회원관리 : Page
    {
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        public 회원관리()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//뒤로가기
        {
            NavigationService.Navigate(new Uri("/경영메뉴.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//전체조회
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM CMem";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                CmemGrid.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//찾고자하는회원조회
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM CMem where Mph = '" + FindCmem.Text + "';";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                CmemGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//선택회원삭제
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("DELETE FROM CMem where Mph = '" + FindCmem.Text + "'", conn);
                msc.ExecuteNonQuery();
                //삭제하고 새로고침
                string sql = "SELECT * FROM CMem";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                CmemGrid.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)//회원등급 vip변경
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("UPDATE CMem SET Mgrade = 'VIP' where Mph ='" + FindCmem.Text + "'", conn);
                msc.ExecuteNonQuery();
                string sql = "SELECT * FROM CMem where Mph = '" + FindCmem.Text + "';";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                CmemGrid.ItemsSource = ds.Tables[0].DefaultView;
                MessageBox.Show("VIP 등급 변경완료");
                
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)//회원등급 gold변경
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("UPDATE CMem SET Mgrade = 'Gold' where Mph ='" + FindCmem.Text + "'", conn);
                msc.ExecuteNonQuery();
                string sql = "SELECT * FROM CMem where Mph = '" + FindCmem.Text + "';";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                CmemGrid.ItemsSource = ds.Tables[0].DefaultView;
                MessageBox.Show("Gold 등급 변경완료");
               
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)//회원등급 sliver변경
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("UPDATE CMem SET Mgrade = 'Silver' where Mph ='" + FindCmem.Text + "'", conn);
                msc.ExecuteNonQuery();
                string sql = "SELECT * FROM CMem where Mph = '" + FindCmem.Text + "';";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                CmemGrid.ItemsSource = ds.Tables[0].DefaultView;
                MessageBox.Show("Silver 등급 변경완료");
                
            }
        }

        private void CmemGrid_Loaded(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM CMem";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                CmemGrid.ItemsSource = ds.Tables[0].DefaultView;

            }
        }
    }
}
