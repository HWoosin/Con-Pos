﻿using MySql.Data.MySqlClient;
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
        string Conn = "Server=localhost;Database=ConStore_sell;Uid=root;Pwd=dntls88;";
        public static string ParcelReceipt;
        public static string PointChargeReceipt;
        int Charge;
        int Parcel;
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
            Random pointchargereceiptNum = new Random(); //영수증번호8자리 랜덤생성
            Charge = pointchargereceiptNum.Next(10, 99);
            PointChargeReceipt = "charge" + Charge.ToString();
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                string sql = "create table " + PointChargeReceipt + "(SellPDnum char(100) primary key, SellPDname CHAR(100) , Sellcount CHAR(100), Sellprice CHAR(100));";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            NavigationService.Navigate(new Uri("/포인트충전.xaml", UriKind.Relative));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) //택배입고 페이지
        {
            Random parcelreceiptNum = new Random(); //영수증번호8자리 랜덤생성
            Parcel = parcelreceiptNum.Next(10, 99);
            ParcelReceipt = "parcel" + Parcel.ToString();
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                string sql = "create table " + ParcelReceipt + "(SellPDnum char(100) primary key, SellPDname CHAR(100) , Sellcount CHAR(100), Sellprice CHAR(100));";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            NavigationService.Navigate(new Uri("/택배입고.xaml", UriKind.Relative));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/택배픽업.xaml", UriKind.Relative));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/예약상품픽업.xaml", UriKind.Relative));
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Con_pos.SecondWindow secondWindow = new Con_pos.SecondWindow();
            secondWindow.ShowDialog();
        }
    }
}
