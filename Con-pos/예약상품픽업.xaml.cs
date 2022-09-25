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
    /// 예약상품픽업.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 예약상품픽업 : Page
    {
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        public 예약상품픽업()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/서비스판매.xaml", UriKind.Relative));
        }
        private void Allgrid_Loaded(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM ReservePD";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                Allgrid.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//전체조회
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM ReservePD";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                Allgrid.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//선택조회
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM ReservePD where RePDnum = '" + ReserveNum.Text + "';";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                Allgrid.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//고객수령
        {
            if (ReserveNum.Text == "")
            {
                MessageBox.Show("운송장번호를 다시 확인해주세요.");
            }
            else
            {
                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();
                    MySqlCommand msc = new MySqlCommand("DELETE FROM ReservePD where RePDnum = '" + ReserveNum.Text + "'", conn);
                    msc.ExecuteNonQuery();
                    string sql = "SELECT * FROM ReservePD";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    Allgrid.ItemsSource = ds.Tables[0].DefaultView;
                    MessageBox.Show("예약상품이 고객님께 전달되었습니다.");
                }
            }
            
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)//예약상품 입고
        {
            try
            {
                if (checkNum.Text == "" || checkCount.Text == ""|| checkCusNum.Text == "")
                {
                    MessageBox.Show("모두 입력해주세요.");
                }
                else
                {
                    using (MySqlConnection conn = new MySqlConnection(Conn))
                    {
                        conn.Open();
                        MySqlCommand msc = new MySqlCommand("INSERT INTO ReservePD(RePDnum, RePDcount, Cusnum)  values( '" + checkNum.Text + "','" + checkCount.Text + "','" + checkCusNum.Text + "')", conn);
                        msc.ExecuteNonQuery();
                        string sql = "SELECT * FROM ReservePD where RePDnum = '" + checkNum.Text + "';";
                        MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                        //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        Allgrid.ItemsSource = ds.Tables[0].DefaultView;
                        MessageBox.Show("예약상품이 입고되었습니다.");
                    }
                }
               
            }
            catch(Exception ex)
            {
                MessageBox.Show("예약번호를 다시 확인해주세요.");
            }
           
        }

        private void Allgrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRow = (DataRowView)Allgrid.SelectedItem;
                string s1 = dataRow.Row.ItemArray[0].ToString();
                ReserveNum.Text = s1;
            }
            catch (Exception ex) { }
        }
    }
}
