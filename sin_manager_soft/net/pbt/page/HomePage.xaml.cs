using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TabView = Microsoft.UI.Xaml.Controls.TabView;
using TabViewItem = Microsoft.UI.Xaml.Controls.TabViewItem;
using Windows.UI.Xaml.Navigation;
using sin_manager_soft.net.pbt.strings;
using sin_manager_soft.net.pbt.util;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class HomePage
    {
        private readonly ResourceLoader _resourceLoader;
        private TabView _tabView;

        public HomePage()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _tabView = e.Parameter as TabView;
        }

        private void OnAlbumBtnLoaded(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.DataContext = 0;
            btn.Content = _resourceLoader.GetString(ResourceKey.ALBUM_VIEW_KEY);
        }

        private void OnArtistBtnLoaded(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.DataContext = 1;
            btn.Content = _resourceLoader.GetString(ResourceKey.ARTIST_VIEW_KEY);
        }

        private void OnSongBtnLoaded(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.DataContext = 2;
            btn.Content = _resourceLoader.GetString(ResourceKey.SONG_VIEW_KEY);
        }

        private void OnProductBtnLoaded(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.DataContext = 4;
            btn.Content = _resourceLoader.GetString(ResourceKey.PROD_VIEW_KEY);
        }

        private void OnMailServiceBtnLoaded(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.DataContext = 3;
            btn.Content = _resourceLoader.GetString(ResourceKey.MAIL_VIEW_KEY);
        }

        private void OnEditorBtnLoaded(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Content = _resourceLoader.GetString(ResourceKey.LOCAL_STORE_KEY);
        }

        private void OnEditorBtnClick(object sender, RoutedEventArgs e)
        {
            CreateNewTab(typeof(LocalStorePage), -1, _resourceLoader.GetString(ResourceKey.LOCAL_STORE_KEY));
        }

        private void CreateNewTab(Type page, int category, string header)
        {
            TabViewItem newTab = new TabViewItem
            {
                Header = header
            };
            Frame frame = new Frame();
            newTab.Content = frame;
            frame.Navigate(page, category);
            _tabView.TabItems.Add(newTab);
            _tabView.SelectedItem = newTab;
        }

        private void OnCategoryBtnClick(object sender, RoutedEventArgs e)
        {
            int category = (int) (sender as Button).DataContext;
            CreateNewTab(typeof(CategoryPage), category,
                CategoryHeaderSelector.GetHeader((Category) category, _resourceLoader));
        }
    }
}