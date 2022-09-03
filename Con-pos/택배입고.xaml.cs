using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// 택배입고.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 택배입고 : Page
    {
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        string Conn2 = "Server=localhost;Database=ConStore_sell;Uid=root;Pwd=dntls88;";
        public static int Parcelmoney;
        public 택배입고()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//뒤로가기
        {
            using (MySqlConnection conn2 = new MySqlConnection(Conn2))
            {
                conn2.Open();
                string sql = "drop table " + ParcelReceiptNum.Text + "";
                MySqlCommand cmd = new MySqlCommand(sql, conn2);
                cmd.ExecuteNonQuery();
            }
            NavigationService.Navigate(new Uri("/서비스판매.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//운임비확인
        {
            
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT Sentprice FROM ProposalPackage where ProPnum = '" + checkPnum.Text + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                      checkPrice.Text = (reader["Sentprice"].ToString());
                    }
                }
                conn.Close();
            }
        
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//현금결제
        {
            try
            {
                if (CusPay.Text == "")
                {
                    MessageBox.Show("결제금액을 확인해주세요.");
                }
                else
                {
                    if (int.Parse(checkPrice.Text) == 0)
                    {
                        MessageBox.Show("운임비를 확인해주세요!");
                    }
                    else if (int.Parse(checkPrice.Text) > int.Parse(CusPay.Text))
                    {
                        MessageBox.Show("받은금액이 적습니다.");
                    }
                    else
                    {
                        string PaymentTime = DateTime.Now.ToString();
                        int result;
                        result = int.Parse(CusPay.Text) - int.Parse(checkPrice.Text);
                        MessageBox.Show($"거스름돈: {result.ToString("#,##0원")}", "결제완료");

                        using (MySqlConnection conn = new MySqlConnection(Conn))
                        {
                            conn.Open();
                            MySqlCommand msc = new MySqlCommand("INSERT INTO SentPackage(SentPnum) values( '" + checkPnum.Text + "')", conn);
                            MySqlCommand msc2 = new MySqlCommand("UPDATE ProposalPackage SET PayDone = 'YES' where ProPnum ='" + checkPnum.Text + "'", conn);
                            msc.ExecuteNonQuery();
                            msc2.ExecuteNonQuery();
                            MessageBox.Show("택배가 입고되었습니다.");
                        }
                        using (MySqlConnection conn2 = new MySqlConnection(Conn2))
                        {
                            conn2.Open();
                            MySqlCommand msc = new MySqlCommand("INSERT INTO " + ParcelReceiptNum.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice)  values('1','근무자:','" + 근무자교대.worker + "','" + PaymentTime + "')", conn2);
                            msc.ExecuteNonQuery();
                            MySqlCommand msc2 = new MySqlCommand("INSERT INTO " + ParcelReceiptNum.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice)  values('2','선불택배','현금결제 합계:','" + checkPrice.Text + "')", conn2);
                            msc2.ExecuteNonQuery();
                            MySqlCommand msc3 = new MySqlCommand("INSERT INTO " + ParcelReceiptNum.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice)  values('3','','받은금액:','" + CusPay.Text + "')", conn2);
                            msc3.ExecuteNonQuery();
                            MySqlCommand msc4 = new MySqlCommand("INSERT INTO " + ParcelReceiptNum.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice)  values('4','','거스름돈:','" + result + "')", conn2);
                            msc4.ExecuteNonQuery();
                        }
                        using (MySqlConnection conn = new MySqlConnection(Conn))//금고와 매출에 더함
                        {
                            conn.Open();
                            MySqlCommand msc = new MySqlCommand("Update SalesCheck Set todaysave=todaysave+'" + checkPrice.Text + "', cashsave=cashsave+'" + checkPrice.Text + "', safemoney=safemoney+'" + checkPrice.Text + "';", conn);
                            msc.ExecuteNonQuery();
                        }
                        Parcelmoney = int.Parse(checkPrice.Text);
                        CompletePay.IsEnabled = true;
                        backtomenu.IsEnabled = false;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("이미 입고된 택배입니다.");
                CompletePay.IsEnabled = false;
                backtomenu.IsEnabled = true;
            }
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//착불 배송
        {
            try
            {
                if (CusPay.Text == "")
                {
                    MessageBox.Show("결제금액을 확인해주세요.");
                }
                else if (checkPrice.Text !="0")
                {
                    MessageBox.Show("운임비를 확인해주세요.");
                }
                else
                {
                    if (int.Parse(checkPrice.Text) > int.Parse(CusPay.Text) || int.Parse(checkPrice.Text) < int.Parse(CusPay.Text))
                    {
                        MessageBox.Show("운임비를 확인해주세요!");
                    }
                    else 
                    {
                        string PaymentTime = DateTime.Now.ToString();
                        int result;
                        result = int.Parse(CusPay.Text) - int.Parse(checkPrice.Text);
                        MessageBox.Show($"거스름돈: {result.ToString("#,##0원")}", "결제완료");

                        using (MySqlConnection conn = new MySqlConnection(Conn))
                        {
                            conn.Open();
                            MySqlCommand msc = new MySqlCommand("INSERT INTO SentPackage(SentPnum) values( '" + checkPnum.Text + "')", conn);
                            MySqlCommand msc2 = new MySqlCommand("UPDATE ProposalPackage SET PayDone = 'YES' where ProPnum ='" + checkPnum.Text + "'", conn);
                            msc.ExecuteNonQuery();
                            msc2.ExecuteNonQuery();
                            MessageBox.Show("택배가 입고되었습니다.");
                        }
                        using (MySqlConnection conn2 = new MySqlConnection(Conn2))
                        {
                            conn2.Open();
                            MySqlCommand msc = new MySqlCommand("INSERT INTO " + ParcelReceiptNum.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice)  values('1','근무자:','" + 근무자교대.worker + "','" + PaymentTime + "')", conn2);
                            msc.ExecuteNonQuery();
                            MySqlCommand msc2 = new MySqlCommand("INSERT INTO " + ParcelReceiptNum.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice)  values('2','착불택배','현금결제 합계:','" + checkPrice.Text + "')", conn2);
                            msc2.ExecuteNonQuery();
                            MySqlCommand msc3 = new MySqlCommand("INSERT INTO " + ParcelReceiptNum.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice)  values('3','','받은금액:','" + CusPay.Text + "')", conn2);
                            msc3.ExecuteNonQuery();
                            MySqlCommand msc4 = new MySqlCommand("INSERT INTO " + ParcelReceiptNum.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice)  values('4','','거스름돈:','" + result + "')", conn2);
                            msc4.ExecuteNonQuery();
                        }
                        CompletePay.IsEnabled = true;
                        backtomenu.IsEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("이미 입고된 택배입니다.");
                CompletePay.IsEnabled = false;
            }
        }

        private void ParcelReceiptNum_Loaded(object sender, RoutedEventArgs e)
        {
            ParcelReceiptNum.Text = 서비스판매.ParcelReceipt;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/서비스판매.xaml", UriKind.Relative));
        }
    }
}
