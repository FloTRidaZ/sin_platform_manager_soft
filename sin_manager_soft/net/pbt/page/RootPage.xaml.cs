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
using muxc = Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.Resources;
using sin_manager_soft.net.pbt.strings;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class RootPage : Page
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
    }
}
