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
    /// 점포경영관리.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 점포경영관리 : Page
    {
        string Conn = "Server=localhost;Database=ConStore;Uid=root;Pwd=dntls88;";
        public 점포경영관리()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // 메인으로 돌아가기
        {
            NavigationService.Navigate(new Uri("/main.xaml", UriKind.Relative));
        }

        private void login_Click(object sender, RoutedEventArgs e) //로그인 버튼
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                string sql = "SELECT Eid FROM Emlogin";
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                
                
            }
        }
    }
}
