using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.Resources;
using sin_manager_soft.net.pbt.strings;
using sin_manager_soft.net.pbt.dialog;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class RootPage
    {
        private readonly ResourceLoader _resourceLoader;

        public RootPage()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
        }

        private void TabViewTabCloseRequested(muxc.TabView sender, muxc.TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);
        }

        private void TabViewAddTabButtonClick(muxc.TabView sender, object args)
        {
            CreateNewTab(sender);
        }

        private void CreateNewTab(muxc.TabView tabView)
        {
            muxc.TabViewItem newTab = new muxc.TabViewItem
            {
                Header = _resourceLoader.GetString(ResourceKey.HOME_TAB_VIEW_KEY)
            };
            Frame frame = new Frame();
            newTab.Content = frame;
            frame.Navigate(typeof(HomePage), tabView);
            tabView.TabItems.Add(newTab);
            tabView.SelectedItem = newTab;
        }

        private void TabViewLoaded(object sender, RoutedEventArgs e)
        {
            CreateNewTab(sender as muxc.TabView);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            AuthorizationDialog dialog = new AuthorizationDialog();
            await dialog.ShowAsync();
        }
    }
}