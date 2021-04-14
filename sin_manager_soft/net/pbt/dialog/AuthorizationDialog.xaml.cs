using sin_manager_soft.net.pbt.strings;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using sin_manager_soft.net.pbt.sql.connector;


namespace sin_manager_soft.net.pbt.dialog
{
    public sealed partial class AuthorizationDialog : ContentDialog
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
            Connector.CreateInstance(_loginInput.Text, _passwordInput.Password);
        }

        private void ContentDialogSecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Application.Current.Exit();
        }

        private void LoginInputLoaded(object sender, RoutedEventArgs e)
        {
            SetPlaceHolderText(sender as TextBox, ResourceKey.INPUT_LOGIN_KEY);
        }

        private void SetPlaceHolderText(TextBox textBox, string key)
        {
            textBox.PlaceholderText = _resourceLoader.GetString(key);
        }

        private void PasswordInputLoaded(object sender, RoutedEventArgs e)
        {
            SetPlaceHolderText(sender as TextBox, ResourceKey.INPUT_PASSWORD_KEY);
        }
    }
}
