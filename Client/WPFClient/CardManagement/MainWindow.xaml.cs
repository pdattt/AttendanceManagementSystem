using CardManagement.MVVM.ViewModel.MainView;
using System.Windows;
using System.Windows.Input;

namespace CardManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            string confirmMessage = "Are you sure you want to logout?";
            MessageBoxResult messageBoxResult = MessageBox.Show(confirmMessage, "Logout Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Close();
            }
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            string confirmMessage = "Are you sure you want to exit the application?";
            MessageBoxResult messageBoxResult = MessageBox.Show(confirmMessage, "Exit Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void btn_CardManagement_Checked(object sender, RoutedEventArgs e)
        {
            new MainViewModel().GoToCardManagementView();
        }

        private void btn_AccountManagement_Checked(object sender, RoutedEventArgs e)
        {
            new MainViewModel().GoToAccountManagementView();
        }
    }
}