using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace Con_pos
{
    /// <summary>
    /// main.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class main : Page
    {
        public static string ReceiptNum;
        int Receipt;
        string Conn = "Server=localhost;Database=ConStore_sell;Uid=root;Pwd=dntls88;";
        public main()
        {

            InitializeComponent();

            //동적시간구현
            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(0.01);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            DateTimebox.Text = DateTime.Now.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //상품판매 버튼
        {
            Random receiptNum = new Random(); //영수증번호8자리 랜덤생성
            Receipt= receiptNum.Next(10, 99);
            ReceiptNum = "sell"+Receipt.ToString();
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                string sql = "create table "+ReceiptNum+ "(SellPDnum char(100) primary key, SellPDname CHAR(100) , Sellcount CHAR(100), Sellprice CHAR(100));";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            NavigationService.Navigate(new Uri("/상품판매.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //서비스판매 버튼
        {
            NavigationService.Navigate(new Uri("/서비스판매.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //점포경영관리 버튼
        {
            NavigationService.Navigate(new Uri("/점포경영관리.xaml", UriKind.Relative));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/조회업무.xaml", UriKind.Relative));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/시재점검.xaml", UriKind.Relative));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)//근무자변경
        {
            workerBox.Text = 근무자교대.worker;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)//근무자 입력
        {
            workerBox.Text = "입력 중~~";
            Con_pos.ThirdWindow thirdWindow = new Con_pos.ThirdWindow();
            thirdWindow.ShowDialog();
        }

        private void workerBox_Loaded(object sender, RoutedEventArgs e)
        {
            workerBox.Text = 근무자교대.worker;
        }
    }
}
