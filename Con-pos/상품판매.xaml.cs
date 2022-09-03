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
    /// 상품판매.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 상품판매 : Page
    {
        string Conn2 = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";//상품DB접근
        string Conn = "Server=localhost;Database=ConStore_sell;Uid=root;Pwd=dntls88;";//영수증DB접근

        public static int totalprice;
        public static int totalcount;
        public static int safecash;
        public 상품판매()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)// 뒤로가기
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                string sql = "drop table " + Receiptname.Text + "";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                NavigationService.Navigate(new Uri("/main.xaml", UriKind.Relative));
            }
            totalprice = 0;
            totalcount = 0;
        }

        private void ReceiptNum_Loaded(object sender, RoutedEventArgs e)//영수증번호 표기
        {
            Receiptname.Text = main.ReceiptNum;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//상품입력, 상품정보 같으면 갯수추가, 총 가격오름
        {
            try
            {
                if (Pnum.Text == "")
                {
                    MessageBox.Show("상품코드를 입력해주세요.");
                }
                else
                {
                    deleteButton.IsEnabled = true;
                    backtoMain.IsEnabled = false;
                    using (MySqlConnection conn2 = new MySqlConnection(Conn2))
                    {
                        string sql = "SELECT SPDnum, SPDname, SPDprice, SPDevent FROM ShopProduct where SPDnum = '" + Pnum.Text + "';";
                        MySqlCommand cmd = new MySqlCommand(sql, conn2);
                        conn2.Open();

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                shownum.Text = (reader["SPDnum"].ToString());//Hidden
                                Pname.Text = (reader["SPDname"].ToString());//Hidden
                                Pprice.Text = (reader["SPDprice"].ToString());//Hidden
                                Pevent.Text = (reader["SPDevent"].ToString());
                            }
                        }
                        conn2.Close();
                    }
                    try
                    {
                        if (Pnum.Text == shownum.Text)
                        {
                            int Pcount = 1;
                            using (MySqlConnection conn = new MySqlConnection(Conn))
                            {
                                conn.Open();
                                MySqlCommand msc = new MySqlCommand("INSERT INTO " + Receiptname.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice) values('" + Pnum.Text + "','" + Pname.Text + "','" + Pcount + "','" + Pprice.Text + "')", conn);
                                msc.ExecuteNonQuery();
                                string sql = "SELECT SellPDname, Sellcount, Sellprice FROM " + Receiptname.Text + ";";

                                string sql2 = "SELECT  Sellcount FROM " + Receiptname.Text + " where SellPDnum ='" + Pnum.Text + "';";//갯수가 음수로 내려가는지 검사
                                MySqlCommand cmd = new MySqlCommand(sql2, conn);
                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {

                                    while (reader.Read())
                                    {
                                        PDcount.Text = (reader["Sellcount"].ToString());//Hidden
                                    }
                                }
                                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                                DataSet ds = new DataSet();
                                da.Fill(ds);
                                SellGrid.ItemsSource = ds.Tables[0].DefaultView;
                                totalprice += int.Parse(Pprice.Text);
                                TotalPrice.Text = totalprice.ToString();
                                totalcount++;
                                TotalCount.Text = totalcount.ToString();
                                //shownum.Text = "";
                            }
                            using (MySqlConnection conn2 = new MySqlConnection(Conn2))//판매후 재고변동
                            {
                                conn2.Open();
                                MySqlCommand msc2 = new MySqlCommand("Update ShopProduct Set SPDcount=SPDcount-'" + Pcount + "' Where SPDnum ='" + Pnum.Text + "';", conn2);
                                msc2.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            MessageBox.Show("등록되지않은 상품입니다.");
                            //deleteButton.IsEnabled = false;
                            backtoMain.IsEnabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        int Pcount = 1;
                        int Price = int.Parse(Pprice.Text);
                        using (MySqlConnection conn = new MySqlConnection(Conn))
                        {
                            conn.Open();
                            MySqlCommand msc = new MySqlCommand("Update  " + Receiptname.Text + " Set Sellcount= Sellcount+'" + Pcount + "', Sellprice=Sellprice+'" + Price + "' Where SellPDnum ='" + Pnum.Text + "';", conn);
                            msc.ExecuteNonQuery();

                            string sql = "SELECT SellPDname, Sellcount, Sellprice FROM " + Receiptname.Text + ";";
                            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            SellGrid.ItemsSource = ds.Tables[0].DefaultView;
                            totalprice += int.Parse(Pprice.Text);
                            TotalPrice.Text = totalprice.ToString();
                            totalcount++;
                            TotalCount.Text = totalcount.ToString();
                            //shownum.Text = "";
                            using (MySqlConnection conn2 = new MySqlConnection(Conn2))//판매후 재고변동
                            {
                                conn2.Open();
                                MySqlCommand msc2 = new MySqlCommand("Update ShopProduct Set SPDcount=SPDcount-'" + Pcount + "' Where SPDnum ='" + Pnum.Text + "';", conn2);
                                msc2.ExecuteNonQuery();
                            }
                            string sql2 = "SELECT  Sellcount FROM " + Receiptname.Text + " where SellPDnum ='" + Pnum.Text + "';";//갯수가 음수로 내려가는지 검사
                            MySqlCommand cmd = new MySqlCommand(sql2, conn);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {

                                while (reader.Read())
                                {
                                    PDcount.Text = (reader["Sellcount"].ToString());//Hidden
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류입니다.");
            }
            if (totalcount <= 0 || totalprice <= 0)
            {
                TotalCount.Text = "0";
                TotalPrice.Text = "0";
                deleteButton.IsEnabled = false;
                backtoMain.IsEnabled = true;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//삭제
        {
            try
            {
                if (Pnum.Text == "")
                {
                    MessageBox.Show("상품코드를 입력해주세요.");
                }
                else
                {
                    deleteButton.IsEnabled = true;
                    backtoMain.IsEnabled = false;
                    using (MySqlConnection conn2 = new MySqlConnection(Conn2))
                    {
                        string sql = "SELECT SPDnum, SPDname, SPDprice FROM ShopProduct where SPDnum = '" + Pnum.Text + "';";
                        MySqlCommand cmd = new MySqlCommand(sql, conn2);
                        conn2.Open();

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                shownum.Text = (reader["SPDnum"].ToString());//Hidden
                                Pname.Text = (reader["SPDname"].ToString());//Hidden
                                Pprice.Text = (reader["SPDprice"].ToString());//Hidden
                            }
                        }

                        conn2.Close();
                    }
                    try
                    {
                        if (Pnum.Text == shownum.Text)
                        {
                            int Price = int.Parse(Pprice.Text);
                            using (MySqlConnection conn = new MySqlConnection(Conn))
                            {
                                conn.Open();
                                string sql2 = "SELECT  Sellcount FROM " + Receiptname.Text + " where SellPDnum ='" + Pnum.Text + "';";//갯수가 음수로 내려가는지 검사
                                MySqlCommand cmd = new MySqlCommand(sql2, conn);
                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {

                                    while (reader.Read())
                                    {
                                        PDcount.Text = (reader["Sellcount"].ToString());//Hidden
                                    }
                                }

                                MySqlCommand msc2 = new MySqlCommand("INSERT INTO " + Receiptname.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice) values('" + Pnum.Text + "','0','0','0')", conn);//예외로 넘겨 삭제위함
                                msc2.ExecuteNonQuery();
                                MySqlCommand msc = new MySqlCommand("DELETE FROM " + Receiptname.Text + " Where SellPDnum = '" + Pnum.Text + "';", conn);
                                msc.ExecuteNonQuery();

                                string sql = "SELECT SellPDname, Sellcount, Sellprice FROM " + Receiptname.Text + ";";
                                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                                DataSet ds = new DataSet();
                                da.Fill(ds);
                                SellGrid.ItemsSource = ds.Tables[0].DefaultView;
                                shownum.Text = "";
                                using (MySqlConnection conn2 = new MySqlConnection(Conn2))//취소후 재고변동
                                {
                                    conn2.Open();
                                    MySqlCommand msc3 = new MySqlCommand("Update ShopProduct Set SPDcount=SPDcount+'" + PDcount.Text + "' Where SPDnum ='" + Pnum.Text + "';", conn2);
                                    msc3.ExecuteNonQuery();
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("등록되지않은 상품입니다.");
                        }
                    }
                    catch (Exception ex)
                    {
                        using (MySqlConnection conn = new MySqlConnection(Conn))
                        {
                            conn.Open();
                            MySqlCommand msc = new MySqlCommand("DELETE FROM " + Receiptname.Text + " Where SellPDnum = '" + Pnum.Text + "';", conn);
                            msc.ExecuteNonQuery();
                            string sql = "SELECT SellPDname, Sellcount, Sellprice FROM " + Receiptname.Text + ";";

                            string sql2 = "SELECT  Sellcount FROM " + Receiptname.Text + " where SellPDnum ='" + Pnum.Text + "';";//갯수가 음수로 내려가는지 검사
                            MySqlCommand cmd = new MySqlCommand(sql2, conn);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {

                                while (reader.Read())
                                {
                                    PDcount.Text = (reader["Sellcount"].ToString());//Hidden
                                }
                            }
                            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                            //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            SellGrid.ItemsSource = ds.Tables[0].DefaultView;
                            totalprice -= int.Parse(Pprice.Text) * int.Parse(PDcount.Text);
                            TotalPrice.Text = totalprice.ToString();
                            totalcount -= int.Parse(PDcount.Text);
                            TotalCount.Text = totalcount.ToString();
                            shownum.Text = "";
                        }
                        using (MySqlConnection conn2 = new MySqlConnection(Conn2))//취소후 재고변동
                        {
                            conn2.Open();
                            MySqlCommand msc2 = new MySqlCommand("Update ShopProduct Set SPDcount=SPDcount+'" + PDcount.Text + "' Where SPDnum ='" + Pnum.Text + "';", conn2);
                            msc2.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류입니다.");
            }

            if (totalcount <= 0 || totalprice <= 0)
            {
                TotalCount.Text = "0";
                TotalPrice.Text = "0";
                deleteButton.IsEnabled = false;
                backtoMain.IsEnabled = true;
            }
            //목록상품의 갯수가 음수가 되지않도록 수정
            if (int.Parse(PDcount.Text) <= 0)
            {
                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();
                    MySqlCommand msc = new MySqlCommand("DELETE FROM " + Receiptname.Text + " Where SellPDnum = '" + Pnum.Text + "';", conn);
                    msc.ExecuteNonQuery();
                    string sql = "SELECT SellPDname, Sellcount, Sellprice FROM " + Receiptname.Text + ";";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    SellGrid.ItemsSource = ds.Tables[0].DefaultView;
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//현금결제
        {
            if (Price.Text == "")
            {
                MessageBox.Show("결제금액을 확인해주세요.");
            }
            else
            {
                if (int.Parse(TotalPrice.Text) == 0)
                {
                    MessageBox.Show("총 가격을 확인해주세요!");
                }
                else if (int.Parse(TotalPrice.Text) > int.Parse(Price.Text))
                {
                    MessageBox.Show("받은금액이 적습니다.");
                }
                else
                {
                    string PaymentTime = DateTime.Now.ToString();
                    int result;
                    result = int.Parse(Price.Text) - int.Parse(TotalPrice.Text);
                    MessageBox.Show($"거스름돈: {result.ToString("#,##0원")}", "결제완료");
                    //결제완료후 영수증으로 남기기위한 정보 삽입
                    using (MySqlConnection conn = new MySqlConnection(Conn))
                    {
                        conn.Open();
                        MySqlCommand msc = new MySqlCommand("INSERT INTO " + Receiptname.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice) values('1','근무자:','" + 근무자교대.worker + "','" + PaymentTime + "')", conn);
                        msc.ExecuteNonQuery();
                        MySqlCommand msc2 = new MySqlCommand("INSERT INTO " + Receiptname.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice) values('2','현금결제 합계:','" + TotalCount.Text + "','" + TotalPrice.Text + "')", conn);
                        msc2.ExecuteNonQuery();
                        MySqlCommand msc3 = new MySqlCommand("INSERT INTO " + Receiptname.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice) values('3','','받은금액:','" + Price.Text + "')", conn);
                        msc3.ExecuteNonQuery();
                        MySqlCommand msc4 = new MySqlCommand("INSERT INTO " + Receiptname.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice) values('4','','거스름돈:','" + result + "')", conn);
                        msc4.ExecuteNonQuery();

                        string sql = "SELECT SellPDname, Sellcount, Sellprice FROM " + Receiptname.Text + ";";
                        MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                        //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        SellGrid.ItemsSource = ds.Tables[0].DefaultView;
                        EnterButton.IsEnabled = false;
                        CompleteButton.IsEnabled = true;
                        safecash = totalprice;
                        totalprice = 0;//정적이므로 다시 0으로 만들어줌
                        totalcount = 0;
                        //main.Safemoney += totalprice;
                    }
                    using (MySqlConnection conn2 = new MySqlConnection(Conn2))//금고와 매출에 더함
                    {
                        conn2.Open();
                        MySqlCommand msc = new MySqlCommand("Update SalesCheck Set todaysave=todaysave+'" + safecash + "', cashsave=cashsave+'" + safecash + "', safemoney=safemoney+'" + safecash + "';", conn2);
                        msc.ExecuteNonQuery();
                    }
                }
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)//결제완료
        {
            NavigationService.Navigate(new Uri("/main.xaml", UriKind.Relative));
            totalprice = 0;
            totalcount = 0;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)//포인트결제
        {
            if (totalprice == 0 || TotalPrice.Text == "")
            {
                MessageBox.Show("결제금액을 확인해주세요!");
            }
            else
            {
                Con_pos.FourthWindow fourthWindow = new Con_pos.FourthWindow();
                fourthWindow.ShowDialog();
                CompleteButton.IsEnabled = true;
                CashButton.IsEnabled = false;
            }

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (totalprice == 0 || TotalPrice.Text == "")
            {
                MessageBox.Show("결제금액을 확인해주세요!");
            }
            else
            {
                Con_pos.FifthWindow fifthWindow = new Con_pos.FifthWindow();
                fifthWindow.ShowDialog();
            }
        }
    }
}