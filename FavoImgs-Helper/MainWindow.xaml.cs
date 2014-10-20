using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Windows.Forms;

namespace FavoImgs_Helper
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RunFavoImgs(string Args)
        {
            //주어진 옵션대로 FavoImgs.exe 실행
            System.Diagnostics.Process.Start(System.Environment.CurrentDirectory + "\\FavoImgs.exe", Args);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //관트짤줍이 없는 경우
            if (!File.Exists(System.Environment.CurrentDirectory + "\\FavoImgs.exe"))
            {
                //자동 다운로드 X
                System.Windows.MessageBox.Show("관트짤줍이 존재하지 않습니다.\n프로그램을 다운로드받고 관트짤줍 헬퍼를 다시 실행해 주세요.");
                //다운로드 페이지 열기
                System.Diagnostics.Process.Start("explorer.exe", "http://azyu.tumblr.com/post/89925086759/favoimgs");
                Environment.Exit(0);
                /*//자동 다운로드 O
                System.Windows.MessageBox.Show("관트짤줍이 존재하지 않습니다.\n관트짤줍을 다운로드합니다.");
                WebClient webCL = new WebClient();
                Utility.downloadFile(Environment.CurrentDirectory, "http://niceb5y.net/data/favoimgs/SQLite.Interop.dll", "http://niceb5y.net/data/favoimgs/FavoImgs.exe");
                System.Windows.MessageBox.Show("관트짤줍 다운로드 완료.");
                */
            }
            /*관트짤줍이 알아서 하도록
            if (!File.Exists(System.Environment.CurrentDirectory + "\\Cache.db"))
            {
                //Cache.db가 없으면 데이터 베이스 연결에 실패하였다며 진행되지 않으므로, 빈 파일을 생성
                File.Create(System.Environment.CurrentDirectory + "\\Cache.db").Close();
            }*/
        }

        private void btnPathReset_Click(object sender, RoutedEventArgs e)
        {
            //경로 초기화
            //Rub_ResetDownloadPath.cmd에 대응
            RunFavoImgs("--reset_path");
            lblPath.Content = "";
        }

        private void btnSetPath_Click(object sender, RoutedEventArgs e)
        {
            //폴더 선택
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            lblPath.Content = dialog.SelectedPath;
        }

        private void cbList_Selected(object sender, RoutedEventArgs e)
        {
            //리스트 이름 텍스트박스 활성화
            txtListName.IsEnabled = true;
        }

        private void cbList_Unselected(object sender, RoutedEventArgs e)
        {
            //리스트 이름 텍스트박스 비활성화
            txtListName.IsEnabled = false;
        }

        private void cbSoneone_Selected(object sender, RoutedEventArgs e)
        {
            //특정 인물 텍스트박스 활성화
            txtAccount.IsEnabled = true;
        }

        private void cbSomeone_Unselected(object sender, RoutedEventArgs e)
        {
            //특정 인물 텍스트박스 비활성화
            txtAccount.IsEnabled = false;
        }

        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            //옵션 변수 생성
            string Args = null;
            //소스 옵션
            Args += "--source=" + ((ComboBoxItem)cbSource.SelectedItem).Content.ToString() + " ";
            //리스트 이름 옵션
            if (((ComboBoxItem)cbSource.SelectedItem).Content.ToString() == "list")
            {
                if (txtListName.Text == "")
                {
                    System.Windows.MessageBox.Show("리스트 이름을 입력하세요.");
                    return;
                }
                else
                {
                    Args += "--slug=\"" + txtListName.Text + "\" ";
                }
            }
            //경로옵션
            if ((string)lblPath.Content != "")
            {
                Args += "--path=\"" + lblPath.Content + "\" ";
            }
            //특정인물 옵션
            if (cbSomeone.IsChecked == true)
            {
                if (txtAccount.Text == "")
                {
                    System.Windows.MessageBox.Show("짤줍할 계정의 이름을 입력하세요.");
                    return;
                }
                else
                {
                    Args += "--screen_name=" + txtAccount.Text + " ";
                }
            }
            if (cbExclude.IsChecked == true)
            {
                Args += "--exclude_rts";
            }
            //실행
            RunFavoImgs(Args);
        }

        private void menuInfo_Click(object sender, RoutedEventArgs e)
        {
            //정보창 띄우기
            InfoWindow _infoWindow = new InfoWindow();
            _infoWindow.Show();
        }
    }
}
