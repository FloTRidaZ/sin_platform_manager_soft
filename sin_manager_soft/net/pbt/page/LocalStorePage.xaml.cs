using System.Collections.Generic;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using sin_manager_soft.net.pbt.essences;
using sin_manager_soft.net.pbt.strings;
using NavigationViewItem = Microsoft.UI.Xaml.Controls.NavigationViewItem;
using NavigationView = Microsoft.UI.Xaml.Controls.NavigationView;
using NavigationViewSelectionChangedEventArgs = Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class LocalStorePage
    {
        private readonly ResourceLoader _resourceLoader;

        public LocalStorePage()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
        }

        private void EditorNavigationViewLoaded(object sender, RoutedEventArgs e)
        {
            NavigationViewItem prodWrapperItem = new NavigationViewItem
            {
                Content = _resourceLoader.GetString(ResourceKey.PROD_VIEW_KEY),
                DataContext = new PageItem {Page = typeof(ProductWrapperPage)}
            };
            NavigationViewItem artistWrapperItem = new NavigationViewItem
            {
                Content = _resourceLoader.GetString(ResourceKey.ARTIST_VIEW_KEY),
                DataContext = new PageItem {Page = typeof(ArtistWrapperPage)}
            };
            NavigationViewItem albumWrapperItem = new NavigationViewItem
            {
                Content = _resourceLoader.GetString(ResourceKey.ALBUM_VIEW_KEY),
                DataContext = new PageItem {Page = typeof(AlbumWrapperPage)}
            };
            NavigationViewItem songWrapperItem = new NavigationViewItem
            {
                Content = _resourceLoader.GetString(ResourceKey.SONG_VIEW_KEY),
                DataContext = new PageItem {Page = typeof(SongWrapperPage)}
            };
            NavigationViewItem mailServiceWrapperItem = new NavigationViewItem
            {
                Content = _resourceLoader.GetString(ResourceKey.MAIL_VIEW_KEY),
                DataContext = new PageItem {Page = typeof(MailServiceWrapperPage)}
            };
            List<NavigationViewItem> items = new List<NavigationViewItem>
            {
                prodWrapperItem,
                artistWrapperItem,
                albumWrapperItem,
                songWrapperItem,
                mailServiceWrapperItem
            };
            NavigationView navigationView = sender as NavigationView;
            navigationView.MenuItemsSource = items;
            navigationView.SelectedItem = items[0];
        }

        private void EditorNavigationViewSelectionChanged(NavigationView sender,
            NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;
            PageItem pageItem = item.DataContext as PageItem;
            NavContent.Navigate(pageItem.Page);
        }
    }
}