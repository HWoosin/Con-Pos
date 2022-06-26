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
        public static DBMySql _db = new DBMySql();

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
            LoadUserInfo();
            CheckID_PW(tb1.Text, tb2.Text);
        }
        public static void LoadUserInfo()
        {
            //데이터베이스에서 사용자 정보 가져오기
            Config.user_ds = _db.SelectAll(Config.Tables[(int)eTName._user]);

        }

        private void CheckID_PW(string id, string pw)
        {
            //사용자 정보와 비교해서 ID / Password 일치하는 지 확인
            if (Config.user_ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in Config.user_ds.Tables[0].Rows)
                {
                    if (id == row["Eid"].ToString() || id == row["Eid"].ToString() )
                    {
                        if (pw == row["Epwd"].ToString())
                        {
                            MessageBox.Show("로그인에 성공했습니다.");
                            NavigationService.Navigate(new Uri("/경영메뉴.xaml", UriKind.Relative));
                        }
                        else
                            MessageBox.Show("비밀번호가 일치하지 않습니다. 확인 후 다시 입력해주세요.");
                    }
                    else
                    {
                        MessageBox.Show("사용자 정보가 없습니다.");
                    }
                }
            }
        }
    }  
}
