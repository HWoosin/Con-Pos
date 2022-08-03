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
    /// 근무자관리.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 근무자관리 : Page
    {
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        public 근무자관리()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/경영메뉴.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//전체근무자조회
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM Empmem";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                AllEmpGrid.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//찾고자하는 근무자 조회
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM Empmem where Empph = '" + EmpID.Text + "';";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                FindEmpGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//선택 근무자 삭제
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("DELETE FROM Empmem where Empph = '" + EmpID.Text + "'", conn);
                msc.ExecuteNonQuery();
                //삭제하고 새로고침
                string sql = "SELECT * FROM Empmem";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                AllEmpGrid.ItemsSource = ds.Tables[0].DefaultView;
                
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)//근무자 추가
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("INSERT INTO Empmem(Empph, Empname, Empbirth, EmpAddress, StartDate, EndDate) values('" + EmpPhone.Text + "','" + Empname.Text + "'," +
                    "'" + EmpBirth.Text + "','"+EmpAddress.Text+ "','" + StartD.Text + "','" + EndD.Text + "')", conn);
                msc.ExecuteNonQuery();
                //추가하고 새로고침
                string sql = "SELECT * FROM Empmem";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                AllEmpGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
        }
    }
}
