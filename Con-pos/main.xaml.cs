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
    /// main.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class main : Page
    {
        public main()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //상품판매 버튼
        {
            NavigationService.Navigate(new Uri("/상품판매.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //서비스판매 버튼
        {
            NavigationService.Navigate(new Uri("/서비스판매.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //점포경영관리 버튼
        {
            NavigationService.Navigate(new Uri("/점포경영관리.xaml", UriKind.Relative));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/조회업무.xaml", UriKind.Relative));
        }
    }
}
