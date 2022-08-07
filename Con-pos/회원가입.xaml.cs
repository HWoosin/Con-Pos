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
        public void JoinMem()
        {
            string value = $"'{tb1.Text}','{tb2.Text}'";
            _db2.Insert(Config2.Tables[(int)eTName2._user], value);
            MessageBox.Show("회원가입을 완료했습니다.");
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)//회원가입
        {
            LoadUserInfo();
            if (!CheckMem(tb1.Text)) 
            { 
                MessageBox.Show("잘못된 입력이거나 이미 가입된 회원입니다."); 
                return; 
            }
            JoinMem();

        }

        public static bool CheckMem(string Text)//가입확인
        {
            if (Config2.user_ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in Config2.user_ds2.Tables[0].Rows)
                {
                    if (Text == row["Mph"].ToString())
                    {
                        return false;
                    }
                    else if (Text=="")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}