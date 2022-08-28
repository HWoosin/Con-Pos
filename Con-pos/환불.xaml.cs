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
    /// 환불.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 환불 : Page
    {
        string Conn = "Server=localhost;Database=ConStore_sell;Uid=root;Pwd=dntls88;";//영수증DB접근
        string Conn2 = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";//상품DB접근

        int Refundprice;
        public 환불()
        {
            InitializeComponent();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)//영수증로드
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("DELETE FROM " + 영수증조회.checkReceipt + " Where SellPDnum =1;", conn);
                msc.ExecuteNonQuery();
                MySqlCommand msc2 = new MySqlCommand("Update " + 영수증조회.checkReceipt + " Set Sellcount='' Where SellPDnum =2;", conn);
                msc2.ExecuteNonQuery();
                MySqlCommand msc3 = new MySqlCommand("DELETE FROM " + 영수증조회.checkReceipt + " Where SellPDnum =3;", conn);
                msc3.ExecuteNonQuery();
                MySqlCommand msc4 = new MySqlCommand("DELETE FROM " + 영수증조회.checkReceipt + " Where SellPDnum =4;", conn);
                msc4.ExecuteNonQuery();
                MySqlCommand msc5 = new MySqlCommand("DELETE FROM " + 영수증조회.checkReceipt + " Where SellPDnum =5;", conn);
                msc5.ExecuteNonQuery();

                string sql = "SELECT SellPDname, Sellcount, Sellprice FROM " + 영수증조회.checkReceipt + ";";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ReceiptLoad.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/영수증조회.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//현금환불
        {
            string PaymentTime = DateTime.Now.ToString();
            using (MySqlConnection conn2 = new MySqlConnection(Conn2))
            {
                conn2.Open();
                MySqlCommand msc = new MySqlCommand("Update SalesCheck Set  todaysave=todaysave-'" + Refundprice + "', cashsave=cashsave-'" + Refundprice + "', safemoney= safemoney-'" + Totalprice.Text + "';", conn2);
                msc.ExecuteNonQuery();
            }
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("INSERT INTO " + 영수증조회.checkReceipt + "(SellPDnum, SellPDname, Sellcount, Sellprice) values('1','근무자:','" + 근무자교대.worker + "','" + PaymentTime + "')", conn);
                msc.ExecuteNonQuery();
                MySqlCommand msc2 = new MySqlCommand("INSERT INTO " + 영수증조회.checkReceipt + "(SellPDnum, SellPDname, Sellcount, Sellprice) values('3','현금','환불완료.','')", conn);
                msc2.ExecuteNonQuery();
            }
                MessageBox.Show("현금환불완료.");
            FindMem.IsEnabled = false;
            RefundCash.IsEnabled = false;
            RefundPoint.IsEnabled = false;
            backbutton.IsEnabled = true;
        }

        private void ReceiptLoad_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)//셀 선택시 텍스트박스에 나옴
        {
            try
            {
                DataRowView dataRow = (DataRowView)ReceiptLoad.SelectedItem;
                string s1 = dataRow.Row.ItemArray[0].ToString();
                PDname.Text = s1;
                string s2 = dataRow.Row.ItemArray[1].ToString();
                PDcount.Text = s2;
                string s3 = dataRow.Row.ItemArray[2].ToString();
                PDprice.Text = s3;
                deleteButton.IsEnabled = true;
            }
            catch(Exception ex) { }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//삭제
        {
            if (PDcount.Text == "")
            {
                MessageBox.Show("선택을 확인해주십시오.");
            }
            else if (int.Parse(PDcount.Text) <= 0)
            {
                MessageBox.Show("이미 삭제된 목록입니다.");
            }
            else
            {
                using (MySqlConnection conn2 = new MySqlConnection(Conn2))//취소후 재고변동
                {
                    conn2.Open();
                    MySqlCommand msc = new MySqlCommand("Update ShopProduct Set SPDcount=SPDcount+'" + PDcount.Text + "' Where SPDname ='" + PDname.Text + "';", conn2);
                    msc.ExecuteNonQuery();
                }
                using (MySqlConnection conn = new MySqlConnection(Conn))//취소후 영수증변동
                {
                    conn.Open();
                    MySqlCommand msc = new MySqlCommand("Update " + 영수증조회.checkReceipt + " Set Sellcount=-'" + PDcount.Text + "', Sellprice=-'" + PDprice.Text + "' Where SellPDname ='" + PDname.Text + "';", conn);
                    msc.ExecuteNonQuery();

                    string sql = "SELECT SellPDname, Sellcount, Sellprice FROM " + 영수증조회.checkReceipt + ";";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    ReceiptLoad.ItemsSource = ds.Tables[0].DefaultView;
                }
                MessageBox.Show("환불목록 수정 완료.");
                PDcount.Text = "0";
                //PDprice.Text = "0";
                Refundprice = Refundprice+int.Parse(PDprice.Text);
                Totalprice.Text = Refundprice.ToString();
                deleteButton.IsEnabled = false;
                backbutton.IsEnabled = false;
            }
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//고객조회
        {
            try
            {
                if (CusMem.Text == "")
                {
                    MessageBox.Show("고객번호를 입력해주세요.");
                }
                else
                {
                    using (MySqlConnection conn2 = new MySqlConnection(Conn2))
                    {
                        string sql = "SELECT Mname, Mpoint FROM CMem where Mph = '" + CusMem.Text + "';";
                        MySqlCommand cmd = new MySqlCommand(sql, conn2);
                        conn2.Open();

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                CusName.Text = (reader["Mname"].ToString());
                                CusPoint.Text = (reader["Mpoint"].ToString());
                            }
                        }
                        conn2.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("존재하지않는 회원입니다.");
            }
           

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)//포인트환불
        {
            if(CusName.Text=="-"|| CusPoint.Text=="-")
            {
                MessageBox.Show("고객을 조회해주세요");
            }
            else
            {
                string PaymentTime = DateTime.Now.ToString();
                using (MySqlConnection conn2 = new MySqlConnection(Conn2))//해당회원의 포인트 차감과 매출에 포인트 더함
                {
                    conn2.Open();
                    MySqlCommand msc = new MySqlCommand("Update Cmem Set Mpoint= Mpoint+'" + Refundprice + "' Where Mph ='" + CusMem.Text + "';", conn2);
                    msc.ExecuteNonQuery();
                    MySqlCommand msc2 = new MySqlCommand("Update SalesCheck Set todaysave=todaysave-'" + Refundprice + "', pointsave=pointsave-'" + Refundprice + "';", conn2);
                    msc2.ExecuteNonQuery();
                }
                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();
                    MySqlCommand msc = new MySqlCommand("INSERT INTO " + 영수증조회.checkReceipt + "(SellPDnum, SellPDname, Sellcount, Sellprice) values('1','근무자:','" + 근무자교대.worker + "','" + PaymentTime + "')", conn);
                    msc.ExecuteNonQuery();
                    MySqlCommand msc2 = new MySqlCommand("INSERT INTO " + 영수증조회.checkReceipt + "(SellPDnum, SellPDname, Sellcount, Sellprice) values('3','포인트','환불완료.','')", conn);
                    msc2.ExecuteNonQuery();
                }
                MessageBox.Show("포인트환불완료.");
                FindMem.IsEnabled = false;
                RefundCash.IsEnabled = false;
                RefundPoint.IsEnabled = false;
                backbutton.IsEnabled = true;
            }
            
        }
    }
}
