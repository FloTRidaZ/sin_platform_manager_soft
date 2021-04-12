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
using Microsoft.UI.Xaml.Controls;
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

        private void RootTabViewTabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);
        }

        private void RootTabViewAddTabButtonClick(TabView sender, object args)
        {
            TabViewItem newTab = new TabViewItem
            {
                Header = _resourceLoader.GetString(ResourceKey.HOME_TAB_VIEW_KEY)
            };
            Frame frame = new Frame();
            newTab.Content = frame;
            frame.Navigate(typeof(HomePage));
            sender.TabItems.Add(newTab);
            sender.SelectedItem = newTab;
        }
    }
}
