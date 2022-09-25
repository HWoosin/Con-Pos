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
    /// 택배접수관리.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 택배접수관리 : Page
    {
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        public 택배접수관리()
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
                string sql = "SELECT * FROM SentPackage";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                SentGrid.ItemsSource = ds.Tables[0].DefaultView;

                string sql2 = "SELECT ProPnum, sentname, recepname, sentprice, paydone FROM ProposalPackage";
                MySqlDataAdapter da2 = new MySqlDataAdapter(sql2, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                PropGrid.ItemsSource = ds2.Tables[0].DefaultView;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//접수조회
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql2 = "SELECT ProPnum, sentname, recepname, sentprice, paydone FROM ProposalPackage where Propnum ='"+Packnum.Text+"';";
                MySqlDataAdapter da2 = new MySqlDataAdapter(sql2, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                PropGrid.ItemsSource = ds2.Tables[0].DefaultView;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//입고목록삭제
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("DELETE FROM SentPackage where SentPnum = '" + Packnum.Text + "'", conn);
                msc.ExecuteNonQuery();
                //삭제하고 새로고침
                string sql = "SELECT * FROM SentPackage";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                SentGrid.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)//접수목록삭제
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("DELETE FROM ProposalPackage where ProPnum = '" + Packnum.Text + "'", conn);
                msc.ExecuteNonQuery();
                //삭제하고 새로고침
                string sql = "SELECT ProPnum, sentname, recepname, sentprice, paydone FROM ProposalPackage";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                PropGrid.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)//미결제 변경
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("UPDATE ProposalPackage SET paydone = 'NO' where Propnum ='" + Packnum.Text + "'", conn);
                msc.ExecuteNonQuery();
                string sql = "SELECT ProPnum, sentname, recepname, sentprice, paydone FROM ProposalPackage where Propnum ='" + Packnum.Text + "';";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                PropGrid.ItemsSource = ds.Tables[0].DefaultView;
                MessageBox.Show("미결제 변경완료");

            }
        }

        private void PropGrid_Loaded(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql2 = "SELECT ProPnum, sentname, recepname, sentprice, paydone FROM ProposalPackage";
                MySqlDataAdapter da2 = new MySqlDataAdapter(sql2, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                PropGrid.ItemsSource = ds2.Tables[0].DefaultView;
            }
        }

        private void SentGrid_Loaded(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM SentPackage";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                SentGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
        }

        private void PropGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRow = (DataRowView)PropGrid.SelectedItem;
                string s1 = dataRow.Row.ItemArray[0].ToString();
                Packnum.Text = s1;
            }
            catch (Exception ex) { }
        }
    }
}
