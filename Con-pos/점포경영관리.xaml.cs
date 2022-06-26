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
    /// 점포경영관리.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 점포경영관리 : Page
    {
        MySQLManager manager = new MySQLManager();
        public 점포경영관리()
        {
            InitializeComponent();
            Loaded += login_Loaded;
        }
        private void login_Loaded(object sender, RoutedEventArgs e)
        {
            // DB Connection
            manager.Initialize();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // 메인으로 돌아가기
        {
            NavigationService.Navigate(new Uri("/main.xaml", UriKind.Relative));
        }
        public class LoginEventArgs : EventArgs
        {
            public bool isSuccess;
        }
        private void login_Click(object sender, RoutedEventArgs e) //로그인 버튼
        {
            LoginEventArgs args = new LoginEventArgs();

            manager.Select("Emlogin", 3, tb1.Text, tb2.Text);

            if (MySQLManager.DataSearchResult == true)
            {
                args.isSuccess = true;
            }

            if (args.isSuccess == true)
            {
                MessageBox.Show("로그인에 성공하셨습니다!");
                NavigationService.Navigate(new Uri("/main.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("로그인에 실패하셨습니다.");
            }
        }
    }  
}
