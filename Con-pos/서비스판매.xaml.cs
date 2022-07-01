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
    /// 서비스판매.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 서비스판매 : Page
    {
        public 서비스판매()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //메인으로 돌아가기
        {
            NavigationService.Navigate(new Uri("/main.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //회원가입 페이지
        {
            NavigationService.Navigate(new Uri("/회원가입.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //포인트충전 페이지
        {
            NavigationService.Navigate(new Uri("/포인트충전.xaml", UriKind.Relative));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //택베접수 페이지
        {
            NavigationService.Navigate(new Uri("/택배접수.xaml", UriKind.Relative));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/택배픽업.xaml", UriKind.Relative));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/예약상품픽업.xaml", UriKind.Relative));
        }
    }
}
