using muxc = Microsoft.UI.Xaml.Controls;
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
using sin_manager_soft.net.pbt.essences;
using sin_manager_soft.net.pbt.strings;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class HomePage : Page
    {
        private readonly List<PageItem> _gridViewItems;
        private readonly ResourceLoader _resourceLoader;
        private muxc.TabView _tabView;

        public HomePage()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
            _gridViewItems = new List<PageItem>();
            BuildGridViewItems();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _tabView = e.Parameter as muxc.TabView;
        }

        private void BuildGridViewItems()
        {
            PageItem albumItem = new PageItem
            {
                Header = _resourceLoader.GetString(ResourceKey.ALBUM_VIEW_KEY),
                Page = typeof(AlbumPage)
            };
            PageItem productItem = new PageItem
            {
                Header = _resourceLoader.GetString(ResourceKey.PRODUCT_VIEW_KEY),
                Page = typeof(ProductPage)
            };
            PageItem songItem = new PageItem
            {
                Header = _resourceLoader.GetString(ResourceKey.SONG_VIEW_KEY),
                Page = typeof(SongPage)
            };
            PageItem artistItem = new PageItem
            {
                Header = _resourceLoader.GetString(ResourceKey.ARTIST_VIEW_KEY),
                Page = typeof(ArtistPage)
            };
            PageItem editorItem = new PageItem
            {
                Header = _resourceLoader.GetString(ResourceKey.EDITOR_VIEW_KEY),
                Page = typeof(EditorPage)
            };
            PageItem mailServiceItem = new PageItem
            {
                Header = _resourceLoader.GetString(ResourceKey.MAIL_VIEW_KEY),
                Page = typeof(MailServicePage)
            };
            _gridViewItems.Add(albumItem);
            _gridViewItems.Add(songItem);
            _gridViewItems.Add(artistItem);
            _gridViewItems.Add(mailServiceItem);
            _gridViewItems.Add(productItem);
            _gridViewItems.Add(editorItem);
        }

        private void GridViewItemClick(object sender, ItemClickEventArgs e)
        {
            PageItem clicked = e.ClickedItem as PageItem;
            Frame frame = new Frame();
            muxc.TabViewItem item = new muxc.TabViewItem
            {
                Header = clicked.Header,
                Content = frame
            };
            frame.Navigate(clicked.Page, _tabView);
            _tabView.TabItems.Add(item);
            _tabView.SelectedItem = item;
        }
    }
}
