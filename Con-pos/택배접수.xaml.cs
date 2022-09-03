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
            try
            {
                Random pronum = new Random(); //운송장번호8자리 랜덤생성
                pnum = pronum.Next(10000000, 99999999);
                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();
                    MySqlCommand msc = new MySqlCommand("INSERT INTO ProposalPackage(ProPnum, sentname, sentph, sentAddress, recepname, recepph, recepAddress) values( '" + pnum + "','" + Sname.Text + "','" + Sphone.Text + "'," +
                        "'" + Saddress.Text + "','" + Rname.Text + "','" + Rphone.Text + "','" + Raddress.Text + "')", conn);
                    msc.ExecuteNonQuery();
                    string sql = "SELECT ProPnum, sentname, recepname FROM ProposalPackage where ProPnum = '" + pnum + "';";//택배 송수신 그리드출력
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    checkGrid.ItemsSource = ds.Tables[0].DefaultView;
                    makepronum.IsEnabled = false;
                    deletepronum.IsEnabled = true;

                    MessageBox.Show("입력완료! 운송장번호를 확인해주세요!");

                    string sql2 = "SELECT  ProPnum FROM ProposalPackage where ProPnum ='" + pnum + "';";//운송장번호 블록출력
                    MySqlCommand cmd = new MySqlCommand(sql2, conn);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            checkPackageNum.Text = (reader["ProPnum"].ToString());//Hidden
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("입력완료되어 운송장이 생성된 택배입니다.");    
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
                MessageBox.Show("입력한 택배를 삭제했습니다!");
                checkPackageNum.Text = "-";
                deletepronum.IsEnabled = false;
                makepronum.IsEnabled = true;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//선불
        {
            if (checkPackageNum.Text=="-")
            {
                MessageBox.Show("운송장번호가 출력되지 않았습니다.");
            }
            else
            {
                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();
                    MySqlCommand msc = new MySqlCommand("UPDATE ProposalPackage SET Sentprice = 3000 where ProPnum ='" + pnum + "'", conn);
                    msc.ExecuteNonQuery();
                    MessageBox.Show("선불택배>>입력이 완료되었습니다. 결제를 진행해주세요!");
                }
                Payfor.IsEnabled = false;
                PayforFree.IsEnabled = false;
                deletepronum.IsEnabled = false;
                makepronum.IsEnabled = false;
            }
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//착불
        {
            if (checkPackageNum.Text == "-")
            {
                MessageBox.Show("운송장번호가 출력되지 않았습니다.");
            }
            else
            {
                MessageBox.Show("착불택배>>입력이 완료되었습니다. 입고를 진행해주세요!");
                Payfor.IsEnabled = false;
                PayforFree.IsEnabled = false;
                deletepronum.IsEnabled = false;
                makepronum.IsEnabled = false;
            }
        }

        private void closewindow_Click(object sender, RoutedEventArgs e)//닫기
        {
            Window.GetWindow(this).Close();
        }
    }
}
