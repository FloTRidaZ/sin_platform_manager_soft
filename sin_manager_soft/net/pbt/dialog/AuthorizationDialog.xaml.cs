using sin_manager_soft.net.pbt.strings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Dapper;
using System.Data.SqlClient;

// Документацию по шаблону элемента "Диалоговое окно содержимого" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

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
        }

        private void ContentDialogPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
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
