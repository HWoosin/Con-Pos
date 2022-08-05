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
    /// 경영메뉴.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 경영메뉴 : Page
    {
        public 경영메뉴()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //로그아웃
        {
            NavigationService.Navigate(new Uri("/main.xaml", UriKind.Relative));
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//상품관리
        {
            NavigationService.Navigate(new Uri("/상품관리.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/근무자관리.xaml", UriKind.Relative));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/매장 정보.xaml", UriKind.Relative));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/회원관리.xaml", UriKind.Relative));
        }
    }
}
