using System.Windows;
using System.Windows.Input;
using CardManagement.MVVM.ViewModel.LoginView;
using MaterialDesignThemes.Wpf;

namespace CardManagement
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        private LoginViewModel login = new LoginViewModel();

        public Login()
        {
            InitializeComponent();
            login.ResetToken();
        }

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private async void LogIn(object sender, RoutedEventArgs e)
        {
            txt_Error.Text = "";
            string username = txtUsername.Text.ToString();
            string password = txtPassword.Password.ToString();

            if (username.Length < 1 || password.Length < 1)
            {
                if (username.Length < 1)
                    txt_Error.Text += "Username must not null!\n";
                if (password.Length < 1)
                    txt_Error.Text += "Password must not null!\n";
                return;
            }

            var token = await login.LogIn(username, password);

            if (token != null)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                txt_Error.Text = "Account is not found!";
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}