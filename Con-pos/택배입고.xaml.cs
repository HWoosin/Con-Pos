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
        
        public 택배입고()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
            if (CusPay.Text == "")
            {
                MessageBox.Show("결제금액을 확인해주세요.");
            }
            else 
            {
                if (int.Parse(checkPrice.Text) == 0)
                {
                    MessageBox.Show("선불택배입니다. 운임비를 확인해주세요!");
                }
                else if (int.Parse(checkPrice.Text) > int.Parse(CusPay.Text))
                {
                    MessageBox.Show("받은금액이 적습니다.");
                }
                else
                {
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
                }
            }
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//착불 배송
        {
            if (CusPay.Text == "")
            {
                MessageBox.Show("결제금액을 확인해주세요.");
            }
            else 
            {
                if (int.Parse(checkPrice.Text) > int.Parse(CusPay.Text) || int.Parse(checkPrice.Text) < int.Parse(CusPay.Text))
                {
                    MessageBox.Show("착불택배입니다. 운임비를 확인해주세요!");
                }
                else //if (int.Parse(checkPrice.Text) == int.Parse(CusPay.Text))
                {
                    using (MySqlConnection conn = new MySqlConnection(Conn))
                    {
                        conn.Open();
                        MySqlCommand msc = new MySqlCommand("INSERT INTO SentPackage(SentPnum) values( '" + checkPnum.Text + "')", conn);
                        MySqlCommand msc2 = new MySqlCommand("UPDATE ProposalPackage SET PayDone = 'YES' where ProPnum ='" + checkPnum.Text + "'", conn);
                        msc.ExecuteNonQuery();
                        msc2.ExecuteNonQuery();
                        MessageBox.Show("택배가 입고되었습니다.");
                    }
                }
            }
        }
    }
}
