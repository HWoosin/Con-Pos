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
        public static string checkReceipt;
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
            try
            {
                if (selectReceipt.Text == "")
                {
                    MessageBox.Show("영수증번호를 입력해주세요.");
                }
                else
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
            catch (Exception ex)
            {
                MessageBox.Show("없는 영수증입니다.");
            }
           
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//환불
        {
            checkReceipt = selectReceipt.Text;
            NavigationService.Navigate(new Uri("/환불.xaml", UriKind.Relative));
        }

        private void AllReceiptGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRow = (DataRowView)AllReceiptGrid.SelectedItem;
                string s1 = dataRow.Row.ItemArray[0].ToString();
                selectReceipt.Text = s1;
            }
            catch (Exception ex) { }
        }
    }
}
