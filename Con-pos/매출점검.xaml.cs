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
using System.Windows.Threading;

namespace Con_pos
{
    /// <summary>
    /// 매출점검.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 매출점검 : Page
    {
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        public 매출점검()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(0.01);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            Nowtime.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/경영메뉴.xaml", UriKind.Relative));
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection conn2 = new MySqlConnection(Conn))
            {
                int todaysales;
                int pointsales;
                int cashsales;
                string sql = "SELECT todaysave, cashsave, pointsave FROM salescheck;";
                MySqlCommand cmd = new MySqlCommand(sql, conn2);
                conn2.Open();
               
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        TodaySales.Text = (reader["todaysave"].ToString());
                        PointSales.Text = (reader["pointsave"].ToString());
                        CashSales.Text = (reader["cashsave"].ToString());
                    }
                }
                conn2.Close();

                todaysales = int.Parse(TodaySales.Text);
                pointsales = int.Parse(PointSales.Text);
                cashsales = int.Parse(CashSales.Text);
                TodaySales.Text = todaysales.ToString("#,##0원");
                PointSales.Text = pointsales.ToString("#,##0원");
                CashSales.Text = cashsales.ToString("#,##0원");

               
            }
        }
    }
}
