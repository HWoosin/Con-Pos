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
    /// 영수증조회.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 영수증조회 : Page
    {
        string Conn = "Server=localhost;Database=ConStore_sell;Uid=root;Pwd=dntls88;";//영수증DB접근
        public 영수증조회()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//뒤로가기
        {
            NavigationService.Navigate(new Uri("/main.xaml", UriKind.Relative));
        }

        private void AllReceiptGrid_Loaded(object sender, RoutedEventArgs e)//전체영수증 로드
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "show tables;";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                AllReceiptGrid.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //영수증내용보기
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT SellPDname, Sellcount, Sellprice FROM " + selectReceipt.Text + ";";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                selectGrid.ItemsSource = ds.Tables[0].DefaultView;

            }
        }
    }
}
