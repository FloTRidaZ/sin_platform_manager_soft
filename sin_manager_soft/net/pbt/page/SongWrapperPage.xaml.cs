using System;
using System.Collections.ObjectModel;
using System.IO;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Dapper;
using sin_manager_soft.net.pbt.sql.connector;
using sin_manager_soft.net.pbt.sql.sqlessences;
using sin_manager_soft.net.pbt.strings;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class SongWrapperPage
    {
        private readonly ObservableCollection<Song> _songs;
        private readonly ResourceLoader _resourceLoader;

        public SongWrapperPage()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
            _songs = SinCollection.GetLocalCollection().SongList;
        }

        private void SongWrapperContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 0)
            {
                return;
            }

            args.RegisterUpdateCallback(BindNameToSong);
        }

        private void BindNameToSong(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 1)
            {
                return;
            }

            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            TextBlock songNameTextBlock = parent.Children[1] as TextBlock;
            Song song = args.Item as Song;
            string rawStr = _resourceLoader.GetString(ResourceKey.RAW_STR_KEY);
            string songName = string.Format(rawStr, _resourceLoader.GetString(ResourceKey.NAME_KEY), song.Name);
            songNameTextBlock.Text = songName;
            songNameTextBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindAlbumToSong);
        }

        private void BindAlbumToSong(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 2)
            {
                return;
            }

            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            TextBlock albumNameTextBlock = parent.Children[2] as TextBlock;
            Song song = args.Item as Song;
            Album album = SinCollection.GetServerCollection().AlbumList.AsList().Find(obj => obj.Songs.Contains(song));
            string rawStr = _resourceLoader.GetString(ResourceKey.RAW_STR_KEY);
            string albumName = string.Format(rawStr, _resourceLoader.GetString(ResourceKey.ALBUM_KEY), album.Name);
            albumNameTextBlock.Text = albumName;
            albumNameTextBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindPictureToSong);
        }

        private async void BindPictureToSong(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 3)
            {
                return;
            }

            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            Image img = parent.Children[0] as Image;
            Song song = args.Item as Song;
            Album album = SinCollection.GetServerCollection().AlbumList.AsList().Find(obj => obj.Songs.Contains(song));
            BitmapImage bitmapImage = new BitmapImage();
            await bitmapImage.SetSourceAsync(new MemoryStream(album.Picture.FileStream).AsRandomAccessStream());
            img.Source = bitmapImage;
            img.Opacity = 1;
        }

        private void SendBtnOnLoaded(object sender, RoutedEventArgs e)
        {
            (sender as Button).Content = _resourceLoader.GetString(ResourceKey.SEND_BTN_KEY);
        }

        private void SendBtnOnClick(object sender, RoutedEventArgs e)
        {
            Connector.GetInstance().SendSongList();
        }
    }
}