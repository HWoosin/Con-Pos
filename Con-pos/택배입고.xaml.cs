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
                //conn.Open();
                //MySqlCommand msc = new MySqlCommand("INSERT INTO SentPackage(SentPnum) values( '" + checkPnum.Text + "')", conn);
                //msc.ExecuteNonQuery();
                string sql = "SELECT Sentprice FROM ProposalPackage where ProPnum = '" + checkPnum.Text + "';";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                //MySqlCommandBuilder cb = new MySqlCommandBuilder(daCountry);
                DataSet ds = new DataSet();
                da.Fill(ds);
               //checkPrice.value = ds.Tables[0].DefaultView;
            }
        }
    }
}
