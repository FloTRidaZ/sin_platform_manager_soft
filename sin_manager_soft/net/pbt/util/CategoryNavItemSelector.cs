using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Resources;
using Microsoft.UI.Xaml.Controls;
using sin_manager_soft.net.pbt.essences;
using sin_manager_soft.net.pbt.page;
using sin_manager_soft.net.pbt.strings;

namespace sin_manager_soft.net.pbt.util
{
    public static class CategoryNavItemSelector
    {
        public static List<NavigationViewItem> GetNavItems(Category category, ResourceLoader resourceLoader)
        {
            List<NavigationViewItem> navItems = new List<NavigationViewItem>();
            switch (category)
            {
                case Category.ALBUM:
                    BuildAlbumNavList(navItems, resourceLoader);
                    break;
                case Category.ARTIST:
                    BuildArtistNavList(navItems, resourceLoader);
                    break;
                case Category.SONG:
                    BuildSongNavList(navItems, resourceLoader);
                    break;
                case Category.MAIL_SERVICE:
                    BuildMailServiceNavList(navItems, resourceLoader);
                    break;
                case Category.PRODUCT:
                    BuildProductNavList(navItems, resourceLoader);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(category), category, null);
            }

            return navItems;
        }

        private static void BuildMailServiceNavList(List<NavigationViewItem> navItems, ResourceLoader resourceLoader)
        {
            NavigationViewItem collectionItem = new NavigationViewItem
            {
                Content = resourceLoader.GetString(ResourceKey.SERVER_NAV_KEY),
                DataContext = new PageItem { Page = typeof(MailServiceListPage) }
            };
            NavigationViewItem editorItem = new NavigationViewItem
            {
                Content = resourceLoader.GetString(ResourceKey.EDITOR_KEY),
                DataContext = new PageItem { Page = typeof(MailServiceCollectionEditorPage) }
            };
            NavigationViewItem wrapperItem = new NavigationViewItem
            {
                Content = resourceLoader.GetString(ResourceKey.WRAPPER_NAV),
                DataContext = new PageItem { Page = typeof(MailServiceWrapperPage) }
            };
            navItems.Add(collectionItem);
            navItems.Add(editorItem);
            navItems.Add(wrapperItem);
        }

        private static void BuildSongNavList(List<NavigationViewItem> navItems, ResourceLoader resourceLoader)
        {
            NavigationViewItem collectionItem = new NavigationViewItem
            {
                Content = resourceLoader.GetString(ResourceKey.SERVER_NAV_KEY),
                DataContext = new PageItem { Page = typeof(SongListPage) }
            };
            NavigationViewItem editorItem = new NavigationViewItem
            {
                Content = resourceLoader.GetString(ResourceKey.EDITOR_KEY),
                DataContext = new PageItem { Page = typeof(SongCollectionEditorPage) }
            };
            NavigationViewItem wrapperItem = new NavigationViewItem
            {
                Content = resourceLoader.GetString(ResourceKey.WRAPPER_NAV),
                DataContext = new PageItem { Page = typeof(SongWrapperPage) }
            };
            navItems.Add(collectionItem);
            navItems.Add(editorItem);
            navItems.Add(wrapperItem);
        }

        private static void BuildArtistNavList(List<NavigationViewItem> navItems, ResourceLoader resourceLoader)
        {
            NavigationViewItem collectionItem = new NavigationViewItem
            {
                Content = resourceLoader.GetString(ResourceKey.SERVER_NAV_KEY),
                DataContext = new PageItem { Page = typeof(ArtistListPage) }
            };
            NavigationViewItem editorItem = new NavigationViewItem
            {
                Content = resourceLoader.GetString(ResourceKey.EDITOR_KEY),
                DataContext = new PageItem { Page = typeof(ArtistCollectionEditorPage) }
            };
            NavigationViewItem wrapperItem = new NavigationViewItem
            {
                Content = resourceLoader.GetString(ResourceKey.WRAPPER_NAV),
                DataContext = new PageItem { Page = typeof(ArtistWrapperPage) }
            };
            navItems.Add(collectionItem);
            navItems.Add(editorItem);
            navItems.Add(wrapperItem);
        }

        private static void BuildAlbumNavList(List<NavigationViewItem> navItems, ResourceLoader resourceLoader)
        {
            NavigationViewItem collectionItem = new NavigationViewItem
            {
                Content = resourceLoader.GetString(ResourceKey.SERVER_NAV_KEY),
                DataContext = new PageItem { Page = typeof(AlbumListPage) }
            };
            NavigationViewItem editorItem = new NavigationViewItem
            {
                Content = resourceLoader.GetString(ResourceKey.EDITOR_KEY),
                DataContext = new PageItem { Page = typeof(AlbumCollectionEditorPage) }
            };
            NavigationViewItem wrapperItem = new NavigationViewItem
            {
                Content = resourceLoader.GetString(ResourceKey.WRAPPER_NAV),
                DataContext = new PageItem { Page = typeof(AlbumWrapperPage) }
            };
            navItems.Add(collectionItem);
            navItems.Add(editorItem);
            navItems.Add(wrapperItem);
        }

        private static void BuildProductNavList(List<NavigationViewItem> navItems, ResourceLoader resourceLoader)
        {
            NavigationViewItem collectionItem = new NavigationViewItem
            {
                Content = resourceLoader.GetString(ResourceKey.SERVER_NAV_KEY),
                DataContext = new PageItem {Page = typeof(ProductListPage)}
            };
            NavigationViewItem editorItem = new NavigationViewItem
            {
                Content = resourceLoader.GetString(ResourceKey.EDITOR_KEY),
                DataContext = new PageItem {Page = typeof(ProductCollectionEditorPage)}
            };
            NavigationViewItem wrapperItem = new NavigationViewItem
            {
                Content = resourceLoader.GetString(ResourceKey.WRAPPER_NAV),
                DataContext = new PageItem {Page = typeof(ProductWrapperPage)}
            };
            navItems.Add(collectionItem);
            navItems.Add(editorItem);
            navItems.Add(wrapperItem);
        }
    }
}