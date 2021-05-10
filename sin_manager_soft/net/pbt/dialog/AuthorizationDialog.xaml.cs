using sin_manager_soft.net.pbt.strings;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using sin_manager_soft.net.pbt.sql.connector;


namespace sin_manager_soft.net.pbt.dialog
{
    public sealed partial class AuthorizationDialog
    {
        private readonly ResourceLoader _resourceLoader;
        public AuthorizationDialog()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
            PrimaryButtonText = _resourceLoader.GetString(ResourceKey.AUTHORIZATION_KEY);
            SecondaryButtonText = _resourceLoader.GetString(ResourceKey.CANCEL_KEY);
            Title = _resourceLoader.GetString(ResourceKey.AUTHORIZATION_TITLE_KEY);
        }

        private void ContentDialogPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Connector.CreateInstance(LoginInput.Text, PasswordInput.Password);
            Connector.GetInstance().Connect();
        }

        private void ContentDialogSecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Application.Current.Exit();
        }

        private void LoginInputLoaded(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.PlaceholderText = _resourceLoader.GetString(ResourceKey.INPUT_LOGIN_KEY);
        }

        private void PasswordInputLoaded(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            passwordBox.PlaceholderText = _resourceLoader.GetString(ResourceKey.INPUT_PASSWORD_KEY);
        }
    }
}
