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
        private bool initial;

        public MainWindow()
        {
            initial = true;
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
                System.Windows.MessageBox.Show("관트짤줍이 존재하지 않습니다.\n프로그램을 다운로드받고 관트짤줍 헬퍼를 다시 실행해 주세요.");
                //다운로드 페이지 열기
                System.Diagnostics.Process.Start("explorer.exe", "http://azyu.tumblr.com/post/89925086759/favoimgs");
                Environment.Exit(0);
            }
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
            string source = ((ComboBoxItem)cbSource.SelectedItem).Content.ToString();
            string Args = null;
            //소스 옵션
            Args += "--source=" + source + " ";
            //리스트/해시태그 이름 옵션
            if (txtName.IsEnabled)
            {
                if (txtName.Text == "")
                {
                    System.Windows.MessageBox.Show(source + " 이름을 입력하세요.");
                    return;
                }
                else
                {
                    if (source == "list") {
                        Args += "--slug=\"" + txtName.Text + "\" ";
                    }
                    else 
                    {
                        Args += "--hashtag=\"#" + txtName.Text + "\" ";
                    }
                }
            }
            //그룹 옵션
            if ((bool)cbGroup.IsChecked)
            {
                Args += "--group=screen_name ";
            }
            else
            {
                Args += "--group=none ";
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

        private void cbSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //값 변경
            if (((ComboBoxItem)cbSource.SelectedItem).Content.ToString() == "list" || ((ComboBoxItem)cbSource.SelectedItem).Content.ToString() == "hashtag")
            {
                txtName.IsEnabled = true;
            }
            else
            {
                if (initial)
                {
                    initial = false;
                }
                else
                {
                    txtName.IsEnabled = false;
                }
            }
        }
    }
}
