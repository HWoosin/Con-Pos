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
    /// 금고입금.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 금고입금 : Page
    {
        public static int resultmoney;
        public 금고입금()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//뒤로가기
        {
            NavigationService.Navigate(new Uri("/main.xaml", UriKind.Relative));
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            Safemoney.Text = main.Safemoney.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//입금했을때 남은금액까지 확인하기
        {
            if (Insertmoney.Text == "")
            {
                MessageBox.Show("입금액을 확인해주세요.");
            }
            else
            {
                if (int.Parse(Insertmoney.Text) == 0 )
                {
                    MessageBox.Show("입금액을 확인해주세요!");
                }
                else if (int.Parse(Insertmoney.Text) == int.Parse(Safemoney.Text))
                {
                    MessageBox.Show("금고액이 적습니다.");
                }
                else
                {
                    resultmoney = int.Parse(Safemoney.Text) - int.Parse(Insertmoney.Text);
                    aftermoney.Text = resultmoney.ToString();
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//입금
        {
            if (Insertmoney.Text == "")
            {
                MessageBox.Show("입금액을 확인해주세요.");
            }
            else
            {
                if (int.Parse(Insertmoney.Text) == 0)
                {
                    MessageBox.Show("입금액을 확인해주세요!");
                }
                else if (int.Parse(Insertmoney.Text) > int.Parse(Safemoney.Text))
                {
                    MessageBox.Show("금고액이 적습니다.");
                }
                else
                {
                    main.Safemoney = int.Parse(aftermoney.Text);
                    MessageBox.Show("입금 완료!");
                    resultmoney = 0;
                    NavigationService.Navigate(new Uri("/main.xaml", UriKind.Relative));
                }
            }
        }
    }
}
