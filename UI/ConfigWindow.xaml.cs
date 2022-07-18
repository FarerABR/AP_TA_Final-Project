using System.Windows;
using System.Windows.Input;
using DAL.Entity;

namespace UI
{
    public delegate void CloseWindow();
    /// <summary>
    /// Interaction logic for ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public static CloseWindow closeWindow;
        public static User _user;
        public ConfigWindow(User user)
        {
            _user = user;
            InitializeComponent();
            closeWindow += ClosePage;
        }

        private void ClosePage()
        {
            this.Close();
        }

        private void btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = System.Windows.WindowState.Minimized;
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void brd_Head_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}