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
using System.Windows.Forms;
using System.IO;

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
            System.Diagnostics.Process.Start(System.Environment.CurrentDirectory + "\\FavoImgs.exe", Args);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(System.Environment.CurrentDirectory + "\\Cache.db"))
            {
                File.Create(System.Environment.CurrentDirectory + "\\Cache.db");
            }
            if (!File.Exists(System.Environment.CurrentDirectory + "\\FavoImgs.exe"))
            {
                System.Windows.MessageBox.Show("FavoImgs Do Not Exist.");
                Environment.Exit(0);
            }
        }

        private void btnPathReset_Click(object sender, RoutedEventArgs e)
        {
            RunFavoImgs("--reset_path");
            lblPath.Content = "";
        }

        private void btnSetPath_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            lblPath.Content = dialog.SelectedPath;
        }

        private void cbList_Selected(object sender, RoutedEventArgs e)
        {
            txtListName.IsEnabled = true;
        }

        private void cbList_Unselected(object sender, RoutedEventArgs e)
        {
            txtListName.IsEnabled = false;
        }

        private void btnGetAll_Click(object sender, RoutedEventArgs e)
        {
            RunFavoImgs("--all");
        }

        private void btnGet_Click(object sender, RoutedEventArgs e)
        {
            string Args = null;
            if (ckCont.IsChecked == true)
            {
                Args += "--continue ";
            }
            Args += "--source=" + ((ComboBoxItem)cbSource.SelectedItem).Content.ToString() + " ";
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
            if (lblPath.Content != "")
            {
                Args += "--path=\"" + lblPath.Content + "\"";
            }
            //System.Windows.MessageBox.Show(Args);
            RunFavoImgs(Args);
        }
    }
}
