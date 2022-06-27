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
    /// 회원가입.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class 회원가입 : Page
    {
        public static DBMySql _db2 = new DBMySql();
        public 회원가입()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)//뒤로가기
        {
            NavigationService.Navigate(new Uri("/서비스판매.xaml", UriKind.Relative));
        }
        public static void LoadUserInfo()
        {
            //데이터베이스에서 사용자 정보 가져오기
            Config2.user_ds2 = _db2.SelectAll2(Config2.Tables[(int)eTName2._user]);
            
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)//회원가입
        {
            LoadUserInfo();
            _db2.Connection2();
            JoinMem(tb1.Text);

            string value = $"'{tb1.Text}','{tb2.Text}'";
            _db2.Insert(Config2.Tables[(int)eTName2._user], value);
            MessageBox.Show("회원가입을 완료했습니다.");

        }

        public static bool JoinMem(string Text)
        {
            if (Config2.user_ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in Config2.user_ds2.Tables[0].Rows)
                {
                    if (Text == row["Mph"].ToString())
                    {
                        MessageBox.Show("이미 존재하는 전화번호입니다.");
                        //tb1.Text = String.Empty;
                        //tb2.Text = String.Empty;
                        return false;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return true;
        }
        
    }
}
