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
    /// 상품발주.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 상품발주 : Page
    {
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        public 상품발주()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//뒤로가기
        {
            NavigationService.Navigate(new Uri("/경영메뉴.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//발주가능 상품조회
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM Buyproduct";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
                AllGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//바코드 조회하면 텍스트박스 자동채워지는 조회
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT PDname, PDmaker, PDevent FROM Buyproduct where PDnum = '" + PDnum.Text + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        PDname.Text = (reader["PDname"].ToString());
                        PDmaker.Text = (reader["PDmaker"].ToString());
                        PDevent.Text = (reader["PDevent"].ToString());
                    }
                }
                conn.Close();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//장바구니에 추가
        {
            if (PDcount.Text == "")
            {
                MessageBox.Show("상품갯수를 확인해주세요.");
            }
            else
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(Conn))
                    {
                        conn.Open();
                        MySqlCommand msc = new MySqlCommand("INSERT INTO TemProduct values( '" + PDnum.Text + "','" + PDname.Text + "','" + PDcount.Text + "','" + PDmaker.Text + "','" + PDevent.Text + "')", conn);
                        msc.ExecuteNonQuery();

                        string sql = "SELECT * FROM TemProduct";
                        MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        selectGrid.ItemsSource = ds.Tables[0].DefaultView;
                        MessageBox.Show("선택상품이 장바구니에서 추가되었습니다!");
                    }
                }catch (Exception ex)
                {
                    using (MySqlConnection conn = new MySqlConnection(Conn))
                    {
                        conn.Open();
                        MySqlCommand msc = new MySqlCommand("Update TemProduct Set TPDcount= TPDcount+'" + PDcount.Text + "' Where TPDnum ='" + PDnum.Text + "';", conn);
                        msc.ExecuteNonQuery();

                        string sql = "SELECT * FROM TemProduct";
                        MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        selectGrid.ItemsSource = ds.Tables[0].DefaultView;
                        MessageBox.Show("장바구니에서 갯수가 추가되었습니다!");
                    }
                }
                
                
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)//선택상품취소=장바구니에서 삭제
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand msc = new MySqlCommand("DELETE FROM TemProduct where TPDnum = '" + PDnum.Text + "'", conn);
                msc.ExecuteNonQuery();
                //삭제하고 새로고침
                string sql = "SELECT * FROM TemProduct";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                selectGrid.ItemsSource = ds.Tables[0].DefaultView;
                MessageBox.Show("선택상품이 장바구니에서 삭제되었습니다!");
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)//발주
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();
                    MySqlCommand msc = new MySqlCommand("INSERT INTO ShopProduct values( '" + PDnum.Text + "','" + PDname.Text + "','" + PDcount.Text + "','" + PDmaker.Text + "','" + PDevent.Text + "')", conn);
                    msc.ExecuteNonQuery();
                    MySqlCommand msc2 = new MySqlCommand("DELETE FROM TemProduct where TPDnum = '" + PDnum.Text + "'", conn);
                    msc2.ExecuteNonQuery();
                    string sql = "SELECT * FROM TemProduct";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    selectGrid.ItemsSource = ds.Tables[0].DefaultView;
                    MessageBox.Show("신규발주 완료되었습니다!");
                }
            }
            catch(Exception ex) 
            {
                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();
                    MySqlCommand msc = new MySqlCommand("Update ShopProduct Set SPDcount= SPDcount+'" + PDcount.Text + "' Where SPDnum ='" + PDnum.Text + "';", conn);
                    msc.ExecuteNonQuery();

                    MySqlCommand msc2 = new MySqlCommand("DELETE FROM TemProduct where TPDnum = '" + PDnum.Text + "'", conn);
                    msc2.ExecuteNonQuery();
                    string sql = "SELECT * FROM TemProduct";
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    selectGrid.ItemsSource = ds.Tables[0].DefaultView;
                    MessageBox.Show("기존 상품이 발주되었습니다!");
                }
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)//장바구니 새로고침
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT * FROM TemProduct";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                selectGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
        }
    }
}
