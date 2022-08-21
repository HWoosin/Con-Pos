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

namespace Con_pos
{
    /// <summary>
    /// 포인트적립.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 포인트적립 : Page
    {
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        string Conn2 = "Server=localhost;Database=ConStore_sell;Uid=root;Pwd=dntls88;";//영수증DB접근
        public 포인트적립()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//고객조회
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT Mname, Mpoint, Mgrade FROM CMem where Mph = '" + CusMem.Text + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        CusName.Text = (reader["Mname"].ToString());
                        CusPoint.Text = (reader["Mpoint"].ToString());
                        CusLevel.Text = (reader["Mgrade"].ToString());
                    }
                }
                conn.Close();
            }
            GetPoint_Loaded();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (CusName.Text == "")
            {
                MessageBox.Show("고객조회를 확인해주세요.");
            }
            else
            {
                if (int.Parse(TotalPrice.Text) == 0)
                {
                    MessageBox.Show("총 가격을 확인해주세요!");
                }
                else
                {
                    using (MySqlConnection conn2 = new MySqlConnection(Conn2))
                    {
                        conn2.Open();
                        MySqlCommand msc = new MySqlCommand("INSERT INTO " + main.ReceiptNum + "(SellPDnum, SellPDname, Sellcount, Sellprice) values('5','포인트적립:','" +GetPoint.Text+ "','')", conn2);
                        msc.ExecuteNonQuery();
                    }
                    using (MySqlConnection conn = new MySqlConnection(Conn))//해당회원에게 포인트적립
                    {
                        conn.Open();
                        MySqlCommand msc = new MySqlCommand("Update Cmem Set Mpoint= Mpoint+'" + GetPoint.Text + "' Where Mph ='" + CusMem.Text + "';", conn);
                        msc.ExecuteNonQuery();
                    }
                    Window.GetWindow(this).Close();//적립창 자동닫기
                }
            }
        }


        private void TotalPrice_Loaded(object sender, RoutedEventArgs e)
        {
            TotalPrice.Text = 상품판매.totalprice.ToString();
        }

        private void GetPoint_Loaded()
        {
            if(CusLevel.Text=="VIP")
                GetPoint.Text= (상품판매.totalprice*0.04).ToString();
            else if(CusLevel.Text=="Gold")
                GetPoint.Text = (상품판매.totalprice * 0.03).ToString();
            else if (CusLevel.Text == "Silver")
                GetPoint.Text = (상품판매.totalprice * 0.02).ToString();
            else
                GetPoint.Text = (상품판매.totalprice * 0.01).ToString();
        }
    }
}
    
