using muxc = Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Navigation;
using sin_manager_soft.net.pbt.essences;
using sin_manager_soft.net.pbt.util;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class CategoryPage
    {
        private readonly ResourceLoader _resourceLoader;
        private List<muxc.NavigationViewItem> _menuItems;
        private Category _category;

        public CategoryPage()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
        }

        private void NavigationViewLoaded(object sender, RoutedEventArgs e)
        {
            muxc.NavigationView navView = sender as muxc.NavigationView;
            navView.MenuItemsSource = _menuItems;
            navView.SelectedItem = _menuItems[0];
        }

        private void NavigationViewSelectionChanged(muxc.NavigationView sender,
            muxc.NavigationViewSelectionChangedEventArgs args)
        {
            muxc.NavigationViewItem selectedItem = args.SelectedItem as muxc.NavigationViewItem;
            PageItem pageInstance = selectedItem.DataContext as PageItem;
            NavContent.Navigate(pageInstance.Page);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _category = (Category) e.Parameter;
            _menuItems = CategoryNavItemSelector.GetNavItems(_category, _resourceLoader);
        }
    }
}