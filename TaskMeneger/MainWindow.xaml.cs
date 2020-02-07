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

namespace TaskMeneger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Account account;
        public MainWindow()
        {
            InitializeComponent();
            NewPage.Content = new Login(TextBoxTopic, NewPage, BtnOpenMenu);
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                PackIconWindowMax.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowMaximize;
            }
            else
            {
                WindowState = WindowState.Maximized;
                PackIconWindowMax.Kind = MaterialDesignThemes.Wpf.PackIconKind.CollapseAll;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                WindowState = WindowState.Normal;
                PackIconWindowMax.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowMaximize;
            }
        }

        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            BtnOpenMenu.Visibility = Visibility.Collapsed;
            BtnCloseMenu.Visibility = Visibility.Visible;
        }

        private void BtnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            BtnOpenMenu.Visibility = Visibility.Visible;
            BtnCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void BtnRegistrate_Click(object sender, RoutedEventArgs e)
        {
            NewPage.Content = new Registrate(TextBoxTopic);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            NewPage.Content = new Login(TextBoxTopic, NewPage, BtnOpenMenu);
        }

        private void NewPage_ContentRendered(object sender, EventArgs e)
        {
            NewPage.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.Width < 1080) Width = 1080;

            if (this.Height < 600) Height = 600;

            if (WindowState == WindowState.Maximized)
            {
                PackIconWindowMax.Kind = MaterialDesignThemes.Wpf.PackIconKind.CollapseAll;
            }
        }

        private void LV_Btn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewPage.Content is Account) account = NewPage.Content as Account;
            if (NewPage.Content is Account || NewPage.Content is Task)
            {                
                if (LV_Btn.SelectedIndex == 0)
                {
                    NewPage.Content = new Account(TextBoxTopic, account.GetPerson());
                }
                else if (LV_Btn.SelectedIndex == 1)
                {
                    NewPage.Content = new Task(TextBoxTopic, account.GetPerson());
                }
                else
                {
                    
                }
            }
        }
    }
}
