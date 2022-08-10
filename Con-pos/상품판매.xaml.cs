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
        int totalprice;
        int totalcount;
        public 상품판매()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)// 메인으로 돌아가는 버튼
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                string sql = "drop table " + Receiptname.Text +"";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                NavigationService.Navigate(new Uri("/main.xaml", UriKind.Relative));
            }
        }

        private void ReceiptNum_Loaded(object sender, RoutedEventArgs e)//영수증번호 표기
        {
            Receiptname.Text = main.ReceiptNum;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//상품입력, 상품정보 같으면 갯수추가, 총 가격오름
        {
            deleteButton.IsEnabled = true;
            totalcount++;
            TotalCount.Content = totalcount;
            if (Pnum.Text == "")
            {
                MessageBox.Show("상품코드를 입력해주세요.");
            }
            else
            {
                using (MySqlConnection conn2 = new MySqlConnection(Conn2))
                {
                    string sql = "SELECT SPDnum, SPDname, SPDprice, SPDevent FROM ShopProduct where SPDnum = '" + Pnum.Text + "';";
                    MySqlCommand cmd = new MySqlCommand(sql, conn2);
                    conn2.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            shownum.Text= (reader["SPDnum"].ToString());//Hidden
                            Pname.Text = (reader["SPDname"].ToString());//Hidden
                            Pprice.Text = (reader["SPDprice"].ToString());//Hidden
                            Pevent.Text = (reader["SPDevent"].ToString());
                        }
                    }
                    conn2.Close();
                }
                try
                {
                    int Pcount = 1;
                    using (MySqlConnection conn = new MySqlConnection(Conn))
                    {
                        conn.Open();
                        MySqlCommand msc = new MySqlCommand("INSERT INTO " + Receiptname.Text + "(SellPDnum, SellPDname, Sellcount, Sellprice) values('" + Pnum.Text + "','" + Pname.Text + "','" + Pcount + "','" + Pprice.Text + "')", conn);
                        msc.ExecuteNonQuery();
                        string sql = "SELECT SellPDname, Sellcount, Sellprice FROM " + Receiptname.Text + ";";
                        MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                        //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        SellGrid.ItemsSource = ds.Tables[0].DefaultView;
                    }
                }
                catch (Exception ex)
                {
                    int Pcount = 1;
                    //int Price = Int32.(Pprice.Text);
                    using (MySqlConnection conn = new MySqlConnection(Conn))
                    {
                        conn.Open();
                        MySqlCommand msc = new MySqlCommand("Update  " + Receiptname.Text + " Set Sellcount= Sellcount+'" + Pcount + "' Where SellPDnum ='" + Pnum.Text + "';", conn);
                        msc.ExecuteNonQuery();

                        string sql = "SELECT SellPDname, Sellcount, Sellprice FROM " + Receiptname.Text + ";";
                        MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        SellGrid.ItemsSource = ds.Tables[0].DefaultView;
                    }
                }
                
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//삭제
        {
            totalcount--;
            TotalCount.Content = totalcount;
            int Pcount = 1;
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("Update  " + Receiptname.Text + " Set Sellcount= Sellcount-'" + Pcount + "' Where SellPDnum ='" + Pnum.Text + "';", conn);
                msc.ExecuteNonQuery();
                string sql = "SELECT SellPDname, Sellcount, Sellprice FROM " + Receiptname.Text + ";";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                SellGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            //total 이 음수가 되지않도록 수정
            if(totalcount==0)
            {
                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();
                    MySqlCommand msc = new MySqlCommand("DELETE FROM " + Receiptname.Text + ";", conn);
                    msc.ExecuteNonQuery();
                    string sql = "SELECT SellPDname, Sellcount, Sellprice FROM " + Receiptname.Text + ";";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    SellGrid.ItemsSource = ds.Tables[0].DefaultView;
                }
                deleteButton.IsEnabled = false;
            }
        }
    }
}
