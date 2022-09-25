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
    /// 택배픽업.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 택배픽업 : Page
    {
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        public 택배픽업()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//뒤로가기
        {
            NavigationService.Navigate(new Uri("/서비스판매.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//전체조회
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM ReceptionPackage";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                pickGrid.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//입고
        {
            try
            {
                if (RecepPnum.Text == "")
                {
                    MessageBox.Show("운송장번호를 다시 확인해주세요.");
                }
                else
                {
                    using (MySqlConnection conn = new MySqlConnection(Conn))
                    {
                        conn.Open();
                        MySqlCommand msc = new MySqlCommand("INSERT INTO ReceptionPackage(RecepPnum) values( '" + RecepPnum.Text + "')", conn);
                        msc.ExecuteNonQuery();
                        string sql = "SELECT * FROM ReceptionPackage where RecepPnum = '" + RecepPnum.Text + "';";
                        MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                        //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        pickGrid.ItemsSource = ds.Tables[0].DefaultView;
                        MessageBox.Show("택배가 입고되었습니다.");
                    }
                }
               
            }
            catch(Exception ex)
            {
                MessageBox.Show("운송장번호를 다시 확인해주세요.");
            }
           
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//출고
        {
            if (RecepPnum.Text == "")
            {
                MessageBox.Show("운송장번호를 다시 확인해주세요.");
            }
            else
            {
                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();
                    MySqlCommand msc = new MySqlCommand("DELETE FROM ReceptionPackage where RecepPnum = '" + RecepPnum.Text + "'", conn);
                    msc.ExecuteNonQuery();
                    string sql = "SELECT * FROM ReceptionPackage";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    pickGrid.ItemsSource = ds.Tables[0].DefaultView;
                    MessageBox.Show("택배가 고객님께 전달되었습니다.");
                }
            }
        }

        private void pickGrid_Loaded(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM ReceptionPackage";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                pickGrid.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void pickGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRow = (DataRowView)pickGrid.SelectedItem;
                string s1 = dataRow.Row.ItemArray[0].ToString();
                RecepPnum.Text = s1;
            }
            catch (Exception ex) { }
        }
    }
}
