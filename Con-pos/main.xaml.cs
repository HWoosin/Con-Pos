using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using System.Windows.Threading;
using System.Threading;
using System.IO;
using System.Net;

namespace Con_pos
{
    /// <summary>
    /// main.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class main : Page
    {
        public static string ReceiptNum;
        public static int Safemoney=100000;
        int Receipt;
        string Conn = "Server=localhost;Database=ConStore_sell;Uid=root;Pwd=dntls88;";
        public main()
        {

            InitializeComponent();

            //동적시간구현
            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(0.01);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        StreamReader streamReader1;  // 데이타 읽기 위한 스트림리더
        StreamWriter streamWriter1;  // 데이타 쓰기 위한 스트림라이터  
        private void timer_Tick(object sender, EventArgs e)
        {
            DateTimebox.Text = DateTime.Now.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e) //상품판매 버튼
        {
            Random receiptNum = new Random(); //영수증번호8자리 랜덤생성
            Receipt= receiptNum.Next(10, 99);
            ReceiptNum = "sell"+Receipt.ToString();
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                string sql = "create table "+ReceiptNum+ "(SellPDnum char(100) primary key, SellPDname CHAR(100) , Sellcount CHAR(100), Sellprice CHAR(100));";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
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

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/시재점검.xaml", UriKind.Relative));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)//근무자변경
        {
            workerBox.Text = 근무자교대.worker;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)//근무자 입력
        {
            workerBox.Text = "입력 중~~";
            Con_pos.ThirdWindow thirdWindow = new Con_pos.ThirdWindow();
            thirdWindow.ShowDialog();
        }

        private void workerBox_Loaded(object sender, RoutedEventArgs e)
        {
            workerBox.Text = 근무자교대.worker;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)//영수증 조회
        {
            NavigationService.Navigate(new Uri("/영수증조회.xaml", UriKind.Relative));
        }

        private void safeMoney_Loaded(object sender, RoutedEventArgs e)//금고금액 표시
        {
            Safemoney = 상품판매.safecash+ 택배입고.Parcelmoney +포인트충전.PointChargemoney+ Safemoney;//금고금액에 결제 후 더해주기
            상품판매.safecash = 0;
            택배입고.Parcelmoney = 0;
            포인트충전.PointChargemoney = 0;
            safeMoney.Text = Safemoney.ToString("#,##0");
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/금고입금.xaml", UriKind.Relative));
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/폐기등록.xaml", UriKind.Relative));
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)//서버오픈
        {
            /*
           TcpListener serverSocket = new TcpListener(7000);
           TcpClient clientSocket = default(TcpClient);

           serverSocket.Start();
           IPbox.Text = "started.";
           clientSocket = serverSocket.AcceptTcpClient();
           Portbox.Text = "accept.";

           while (true)
           {
               try
               {
                   NetworkStream networkStream = clientSocket.GetStream();
                   byte[] bytesFrom = new byte[clientSocket.ReceiveBufferSize];
                   networkStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize);
                   string dataFromClient = Encoding.ASCII.GetString(bytesFrom);
                   dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    messageBox.Text = ">>Data from Client--" + dataFromClient;

                   string serverResponse = "Last Message from Client:" + dataFromClient;
                   byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                   networkStream.Write(sendBytes, 0, sendBytes.Length);
                   networkStream.Flush();
                    messageBox.Text = ">>server send Data--" + serverResponse;
               }
               catch (Exception ex) { }
           }
            
           clientSocket.Close();
           serverSocket.Stop();
           */
            /*
            Thread thread1 = new Thread(serverconnect); // Thread 객채 생성, Form과는 별도 쓰레드에서 connect 함수가 실행됨.
            thread1.IsBackground = true; // Form이 종료되면 thread1도 종료.
            thread1.Start(); // thread1 시작.
            */
        }
        /*
        private void serverconnect()  // thread1에 연결된 함수. 메인폼과는 별도로 동작한다.
        {
            TcpListener tcpListener1 = new TcpListener(IPAddress.Parse(IPbox.Text), int.Parse(Portbox.Text)); // 서버 객체 생성 및 IP주소와 Port번호를 할당
            tcpListener1.Start();  // 서버 시작
            writeMessageTextbox("서버 준비...클라이언트 기다리는 중...");

            TcpClient tcpClient1 = tcpListener1.AcceptTcpClient(); // 클라이언트 접속 확인
            writeMessageTextbox("클라이언트 연결됨...");

            streamReader1 = new StreamReader(tcpClient1.GetStream());  // 읽기 스트림 연결
            streamWriter1 = new StreamWriter(tcpClient1.GetStream());  // 쓰기 스트림 연결
            streamWriter1.AutoFlush = true;  // 쓰기 버퍼 자동으로 뭔가 처리..

            while (tcpClient1.Connected)  // 클라이언트가 연결되어 있는 동안
            {
                string receiveData1 = streamReader1.ReadLine();  // 수신 데이타를 읽어서 receiveData1 변수에 저장
                writeMessageTextbox(receiveData1); // 데이타를 수신창에 쓰기                  
            }
        }
        private void writeMessageTextbox(string str)  // richTextbox1 에 쓰기 함수
        {
            messageBox.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate { messageBox.AppendText(str + "\r\n"); })); // 데이타를 수신창에 표시, 반드시 invoke 사용. 충돌피함.
            messageBox.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate { messageBox.ScrollToLine(0); }));  // 스크롤을 젤 밑으로.
        }
            */
        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            //string sendData1 = "확인";  // 내용확인을 sendData1 변수에 저장
            //streamWriter1.WriteLine(sendData1);  // 스트림라이터를 통해 데이타를 전송
        }
        
    }
}
