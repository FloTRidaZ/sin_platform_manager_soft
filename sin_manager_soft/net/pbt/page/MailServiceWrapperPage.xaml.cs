using System.Collections.ObjectModel;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using sin_manager_soft.net.pbt.sql.connector;
using sin_manager_soft.net.pbt.sql.sqlessences;
using sin_manager_soft.net.pbt.strings;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class MailServiceWrapperPage
    {
        private readonly ObservableCollection<MailService> _mailServices;
        private readonly ResourceLoader _resourceLoader;

        public MailServiceWrapperPage()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
            _mailServices = SinCollection.GetLocalCollection().MailServiceList;
        }

        private void MailServiceWrapperContainerContentChanging(ListViewBase sender,
            ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 0)
            {
                return;
            }

            args.RegisterUpdateCallback(BindNameToMailService);
        }

        private void BindNameToMailService(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 1)
            {
                return;
            }

            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            TextBlock mailServiceNameTextBlock = parent.Children[0] as TextBlock;
            MailService mailService = args.Item as MailService;
            string rawStr = _resourceLoader.GetString(ResourceKey.RAW_STR_KEY);
            string mailServiceName =
                string.Format(rawStr, _resourceLoader.GetString(ResourceKey.NAME_KEY), mailService.Name);
            mailServiceNameTextBlock.Text = mailServiceName;
            mailServiceNameTextBlock.Opacity = 1;
        }

        private void SendBtnOnLoaded(object sender, RoutedEventArgs e)
        {
            (sender as Button).Content = _resourceLoader.GetString(ResourceKey.SEND_BTN_KEY);
        }

        private void SendBtnOnClick(object sender, RoutedEventArgs e)
        {
            Connector.GetInstance().SendMailServiceList();
        }
    }
}