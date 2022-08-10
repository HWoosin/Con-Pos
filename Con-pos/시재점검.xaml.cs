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
    /// 시재점검.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 시재점검 : Page
    {
        public 시재점검()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/main.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(_50000.Text=="" || _10000.Text == "" || _5000.Text=="" || _1000.Text=="" || _500.Text=="" || _100.Text==""  || _50.Text=="" || _10.Text=="")
            {
                MessageBox.Show("모두입력해주세요!");
            }
            else
            {
                int checkSafe = (50000 * int.Parse(_50000.Text)) + (10000 * int.Parse(_10000.Text)) + (5000 * int.Parse(_5000.Text)) + (1000 * int.Parse(_1000.Text)) + (500 * int.Parse(_500.Text))
                    + (100 * int.Parse(_100.Text)) + (50 * int.Parse(_50.Text)) + (10 * int.Parse(_10.Text));
                int result= checkSafe - main.Safemoney;
                if(result>0)
                    MessageBox.Show($"시재 점검결과: {result.ToString("#,##0원 많습니다.")}", "점검오류");
                else if (result<0)
                    MessageBox.Show($"시재 점검결과: {result.ToString("#,##0원 입니다.")}", "점검오류");
                else
                    MessageBox.Show($"시재 점검결과: {result.ToString("#,##0원 입니다.")}", "점검완료");

            }
            
        }
    }
}
