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
    /// 포인트결제.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 포인트결제 : Page
    {
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        string Conn2 = "Server=localhost;Database=ConStore_sell;Uid=root;Pwd=dntls88;";//영수증DB접근
        public 포인트결제()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//고객조회
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT Mname, Mpoint FROM CMem where Mph = '" + CusMem.Text + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        CusName.Text = (reader["Mname"].ToString());
                        CusPoint.Text = (reader["Mpoint"].ToString());
                    }
                }
                conn.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//포인트결제
        {
            if (CusPoint.Text == "")
            {
                MessageBox.Show("결제금액을 확인해주세요.");
            }
            else
            {
                if (int.Parse(TotalPrice.Text) == 0)
                {
                    MessageBox.Show("총 가격을 확인해주세요!");
                }
                else if (int.Parse(TotalPrice.Text) > int.Parse(CusPoint.Text))
                {
                    MessageBox.Show("받은금액이 적습니다.");
                }
                else
                {
                    string PaymentTime = DateTime.Now.ToString();
                    int result;
                    result = int.Parse(CusPoint.Text) - int.Parse(TotalPrice.Text);
                    MessageBox.Show($"잔여 포인트: {result.ToString("#,##0원")}", "결제완료");
                    //결제완료후 영수증으로 남기기위한 정보 삽입
                    
                    using (MySqlConnection conn2 = new MySqlConnection(Conn2))
                    {
                        conn2.Open();
                        MySqlCommand msc = new MySqlCommand("INSERT INTO " + main.ReceiptNum + "(SellPDnum, SellPDname, Sellcount, Sellprice) values('1','근무자:','" + 근무자교대.worker + "','" + PaymentTime + "')", conn2);
                        msc.ExecuteNonQuery();
                        MySqlCommand msc2 = new MySqlCommand("INSERT INTO " + main.ReceiptNum + "(SellPDnum, SellPDname, Sellcount, Sellprice) values('2','포인트결제 합계:','" + 상품판매.totalcount + "','" + TotalPrice.Text + "')", conn2);
                        msc2.ExecuteNonQuery();
                        MySqlCommand msc3 = new MySqlCommand("INSERT INTO " + main.ReceiptNum + "(SellPDnum, SellPDname, Sellcount, Sellprice) values('3','','받은포인트:','" + CusPoint.Text + "')", conn2);
                        msc3.ExecuteNonQuery();
                        MySqlCommand msc4 = new MySqlCommand("INSERT INTO " + main.ReceiptNum + "(SellPDnum, SellPDname, Sellcount, Sellprice) values('4','','포인트잔액:','" + result + "')", conn2);
                        msc4.ExecuteNonQuery();
                    }
                    
                        상품판매.totalprice = 0;//정적이므로 다시 0으로 만들어줌
                        상품판매.totalcount = 0;
                    using (MySqlConnection conn = new MySqlConnection(Conn))//해당회원의 포인트 차감
                    {
                        conn.Open();
                        MySqlCommand msc = new MySqlCommand("Update Cmem Set Mpoint= Mpoint-'" + TotalPrice.Text + "' Where Mph ='" + CusMem.Text + "';", conn);
                        msc.ExecuteNonQuery();
                    }
                }
            }
        }

        private void TotalPrice_Loaded(object sender, RoutedEventArgs e)//총 가격 가져오기
        {
            TotalPrice.Text = 상품판매.totalprice.ToString();
        }
    }
}
