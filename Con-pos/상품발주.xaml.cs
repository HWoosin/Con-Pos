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
                string sql = "SELECT PDname, PDmaker, PDprice, PDevent FROM Buyproduct where PDnum = '" + PDnum.Text + "';";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        PDname.Text = (reader["PDname"].ToString());
                        PDmaker.Text = (reader["PDmaker"].ToString());
                        PDprice.Text = (reader["PDprice"].ToString());
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
                        MySqlCommand msc = new MySqlCommand("INSERT INTO temProduct values( '" + PDnum.Text + "','" + PDname.Text + "','" + PDcount.Text + "','" + PDprice.Text + "','" + PDmaker.Text + "','" + PDevent.Text + "')", conn);
                        msc.ExecuteNonQuery();

                        string sql = "SELECT * FROM temProduct";
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
                        MySqlCommand msc = new MySqlCommand("Update temProduct Set TPDcount= TPDcount+'" + PDcount.Text + "' Where TPDnum ='" + PDnum.Text + "';", conn);
                        msc.ExecuteNonQuery();

                        string sql = "SELECT * FROM temProduct";
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
            if (PDcount.Text == "")
            {
                MessageBox.Show("상품갯수를 확인해주세요.");
            }
            else
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
            
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)//발주
        {
            if (PDcount.Text == "")
            {
                MessageBox.Show("상품갯수를 확인해주세요.");
            }
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Conn))
                {
                    conn.Open();
                    MySqlCommand msc = new MySqlCommand("INSERT INTO ShopProduct values( '" + PDnum.Text + "','" + PDname.Text + "','" + PDcount.Text + "','" + PDprice.Text + "','" + PDmaker.Text + "','" + PDevent.Text + "')", conn);
                    msc.ExecuteNonQuery();
                    MySqlCommand msc2 = new MySqlCommand("DELETE FROM temProduct where TPDnum = '" + PDnum.Text + "'", conn);
                    msc2.ExecuteNonQuery();
                    string sql = "SELECT * FROM temProduct";
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

                    MySqlCommand msc2 = new MySqlCommand("DELETE FROM temProduct where TPDnum = '" + PDnum.Text + "'", conn);
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
                string sql = "SELECT * FROM temProduct";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                selectGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
        }

        private void AllGrid_Loaded(object sender, RoutedEventArgs e)
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

        private void selectGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRow = (DataRowView)selectGrid.SelectedItem;
                string s1 = dataRow.Row.ItemArray[0].ToString();
                PDnum.Text = s1;
                string s2 = dataRow.Row.ItemArray[1].ToString();
                PDname.Text = s2;
                string s3 = dataRow.Row.ItemArray[2].ToString();
                PDcount.Text = s3;
                string s4 = dataRow.Row.ItemArray[3].ToString();
                PDprice.Text = s4;
                string s5 = dataRow.Row.ItemArray[4].ToString();
                PDmaker.Text = s5;
                string s6 = dataRow.Row.ItemArray[5].ToString();
                PDevent.Text = s6;
            }
            catch (Exception ex) { }
        }

        private void AllGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                DataRowView dataRow = (DataRowView)AllGrid.SelectedItem;
                string s1 = dataRow.Row.ItemArray[0].ToString();
                PDnum.Text = s1;
                string s2 = dataRow.Row.ItemArray[1].ToString();
                PDname.Text = s2;
                string s4 = dataRow.Row.ItemArray[2].ToString();
                PDprice.Text = s4;
                string s5 = dataRow.Row.ItemArray[3].ToString();
                PDmaker.Text = s5;
                string s6 = dataRow.Row.ItemArray[4].ToString();
                PDevent.Text = s6;
            }
            catch (Exception ex) { }
        }
    }
}
