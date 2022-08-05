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
    /// 택배접수.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 택배접수 : Page
    {
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        int pnum;
        public 택배접수()
        {
            InitializeComponent();
        }
    

        private void Button_Click(object sender, RoutedEventArgs e)//입력
        {
            Random pronum = new Random(); //운송장번호8자리 랜덤생성
            pnum = pronum.Next(10000000, 99999999);
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("INSERT INTO ProposalPackage(ProPnum, sentname, sentph, sentAddress, recepname, recepph, recepAddress) values( '"+pnum+"','" + Sname.Text + "','" + Sphone.Text + "'," +
                    "'" + Saddress.Text + "','" + Rname.Text + "','" + Rphone.Text + "','" + Raddress.Text + "')", conn);
                msc.ExecuteNonQuery();
                string sql = "SELECT ProPnum, sentname, recepname FROM ProposalPackage where ProPnum = '" + pnum + "';";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                checkGrid.ItemsSource = ds.Tables[0].DefaultView;
                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//삭제
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("DELETE FROM ProposalPackage where ProPnum = '" + pnum+ "'", conn);
                msc.ExecuteNonQuery();
                //삭제하고 새로고침
                string sql = "SELECT ProPnum, sentname, recepname FROM ProposalPackage where ProPnum = '" + pnum + "';";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                checkGrid.ItemsSource = ds.Tables[0].DefaultView;

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//선불
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("UPDATE ProposalPackage SET Sentprice = 3000 where ProPnum ='"+pnum+"'", conn);
                msc.ExecuteNonQuery();
                MessageBox.Show("입력이 완료되었습니다. 결제를 진행해주세요!");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//착불
        {
            MessageBox.Show("입력이 완료되었습니다. 입고를 진행해주세요!");
        }
    }
}
