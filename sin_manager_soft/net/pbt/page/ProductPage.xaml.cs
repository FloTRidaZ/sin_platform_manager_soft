using muxc = Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Resources;
using sin_manager_soft.net.pbt.essences;
using sin_manager_soft.net.pbt.strings;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class ProductPage : Page
    {
        private readonly ResourceLoader _resourceLoader;
        private readonly List<muxc.NavigationViewItem> _menuItems;

        public ProductPage()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
            _menuItems = new List<muxc.NavigationViewItem>();
            BuildMenuItems();
        }

        private void BuildMenuItems()
        {
            muxc.NavigationViewItem productCollectionItem = new muxc.NavigationViewItem {
                Content = _resourceLoader.GetString(ResourceKey.PRODUCT_LIST_NAV_KEY)
            };
            productCollectionItem.DataContext = new PageItem
            {
                Page = typeof(ProductListPage)
            };
            muxc.NavigationViewItem productEditorItem = new muxc.NavigationViewItem
            {
                Content = _resourceLoader.GetString(ResourceKey.EDITOR_KEY)
            };
            productEditorItem.DataContext = new PageItem
            {
                Page = typeof(ProductCollectionEditorPage)
            };
            muxc.NavigationViewItem productWrapperItem = new muxc.NavigationViewItem
            {
                Content = _resourceLoader.GetString(ResourceKey.WRAPPER_NAV)
            };
            productWrapperItem.DataContext = new PageItem
            {
                Page = typeof(ProductWrapperPage)
            };
            _menuItems.Add(productCollectionItem);
            _menuItems.Add(productEditorItem);
            _menuItems.Add(productWrapperItem);
        }

        private void NavigationViewLoaded(object sender, RoutedEventArgs e)
        {
            muxc.NavigationView navView = sender as muxc.NavigationView;
            navView.MenuItemsSource = _menuItems;
            navView.SelectedItem = _menuItems[0];
        }

        private void NavigationViewSelectionChanged(muxc.NavigationView sender, muxc.NavigationViewSelectionChangedEventArgs args)
        {
            muxc.NavigationViewItem selecteditem = args.SelectedItem as muxc.NavigationViewItem;
            PageItem pageInstance = selecteditem.DataContext as PageItem;
            _navContent.Navigate(pageInstance.Page);
        }
    }
}
