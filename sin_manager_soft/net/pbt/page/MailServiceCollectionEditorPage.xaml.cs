using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using sin_manager_soft.net.pbt.sql.sqlessences;
using sin_manager_soft.net.pbt.strings;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class MailServiceCollectionEditorPage
    {
        private readonly ResourceLoader _resourceLoader;
        private string _name;

        public MailServiceCollectionEditorPage()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
        }

        private void InputNameTextBlockOnLoaded(object sender, RoutedEventArgs e)
        {
            (sender as TextBlock).Text = _resourceLoader.GetString(ResourceKey.NAME_KEY);
        }

        private void InputNameTextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            _name = (sender as TextBox).Text;
        }

        private void SaveMailServiceBtnOnLoaded(object sender, RoutedEventArgs e)
        {
            (sender as Button).Content = _resourceLoader.GetString(ResourceKey.SAVE_BTN_KEY);
        }

        private void SaveMailServiceBtnOnClick(object sender, RoutedEventArgs e)
        {
            MailService mailService = new MailService
            {
                Id = Guid.NewGuid(),
                Name = _name
            };
            SinCollection.GetLocalCollection().MailServiceList.Add(mailService);
        }
    }
}