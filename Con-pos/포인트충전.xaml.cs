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
    /// 포인트충전.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 포인트충전 : Page
    {
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        string Conn2 = "Server=localhost;Database=ConStore_sell;Uid=root;Pwd=dntls88;";

        public static int PointChargemoney;
        public 포인트충전()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection conn2 = new MySqlConnection(Conn2))
            {
                conn2.Open();
                string sql = "drop table " + PointChargeNum.Text + "";
                MySqlCommand cmd = new MySqlCommand(sql, conn2);
                cmd.ExecuteNonQuery();
            }
            NavigationService.Navigate(new Uri("/서비스판매.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //전체회원 조회
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM CMem";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dg1.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//찾고자 하는 회원 조회
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM CMem where Mph = '" + Mphtb.Text + "';";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dg2.ItemsSource = ds.Tables[0].DefaultView;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//충전
        {
            if(Mpttb.Text=="")
            {
                MessageBox.Show("충전 금액을 입력해주세요.");
            }
            else
            {
                string PaymentTime = DateTime.Now.ToString();
                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();
                    MySqlCommand msc = new MySqlCommand("Update CMem Set Mpoint= Mpoint+'" + Mpttb.Text + "' Where Mph ='" + Mphtb.Text + "';", conn);
                    msc.ExecuteNonQuery();
                    MessageBox.Show("충전이 완료되었습니다!");

                    //충전후 새로고침
                    string sql = "SELECT * FROM CMem where Mph = '" + Mphtb.Text + "';";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dg2.ItemsSource = ds.Tables[0].DefaultView;
                }
                using (MySqlConnection conn2 = new MySqlConnection(Conn2))
                {
                    conn2.Open();
                    MySqlCommand msc2 = new MySqlCommand("INSERT INTO " + PointChargeNum.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice)  values('1','근무자:','" + 근무자교대.worker + "','" + PaymentTime + "')", conn2);
                    msc2.ExecuteNonQuery();
                    MySqlCommand msc3 = new MySqlCommand("INSERT INTO " + PointChargeNum.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice)  values('2','포인트충전','현금결제 합계:','" + Mpttb.Text + "')", conn2);
                    msc3.ExecuteNonQuery();
                    MySqlCommand msc4 = new MySqlCommand("INSERT INTO " + PointChargeNum.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice)  values('3','','받은금액:','" + Mpttb.Text + "')", conn2);
                    msc4.ExecuteNonQuery();
                }
                using (MySqlConnection conn = new MySqlConnection(Conn))//금고와 매출에 더함
                {
                    conn.Open();
                    MySqlCommand msc = new MySqlCommand("Update SalesCheck Set todaysave=todaysave+'" + Mpttb.Text + "', cashsave=cashsave+'" + Mpttb.Text + "', safemoney=safemoney+'" + Mpttb.Text + "';", conn);
                    msc.ExecuteNonQuery();
                }
                PointChargemoney = int.Parse(Mpttb.Text);
                CompleteCharge.IsEnabled = true;
                backtomenu.IsEnabled = false;
            }
            
        }

        private void dg1_Loaded(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM CMem";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dg1.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void PointChargeNum_Loaded(object sender, RoutedEventArgs e)
        {
            PointChargeNum.Text = 서비스판매.PointChargeReceipt;
        }

        private void CompleteCharge_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/서비스판매.xaml", UriKind.Relative));
        }
    }
}
