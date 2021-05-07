using System.Collections.ObjectModel;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;
using sin_manager_soft.net.pbt.sql.sqlessences;
using sin_manager_soft.net.pbt.strings;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class MailServiceListPage
    {
        private readonly ObservableCollection<MailService> _mailServices;
        private readonly ResourceLoader _resourceLoader;

        public MailServiceListPage()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
            _mailServices = SinCollection.GetServerCollection().MailServiceList;
        }

        private void MailServiceListContainerContentChanging(ListViewBase sender,
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
    }
}