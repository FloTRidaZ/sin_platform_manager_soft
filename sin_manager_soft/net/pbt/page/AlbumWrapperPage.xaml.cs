using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
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
    public sealed partial class AlbumWrapperPage
    {
        private readonly ObservableCollection<Album> _albums;
        private readonly ResourceLoader _resourceLoader;

        public AlbumWrapperPage()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
            _albums = SinCollection.GetLocalCollection().AlbumList;
        }

        private void AlbumWrapperContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 0)
            {
                return;
            }

            args.RegisterUpdateCallback(BindNameToAlbum);
        }

        private void BindNameToAlbum(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 1)
            {
                return;
            }

            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            TextBlock nameTextBlock = parent.Children[1] as TextBlock;
            Album album = args.Item as Album;
            string rawString = _resourceLoader.GetString(ResourceKey.RAW_STR_KEY);
            string albumName = string.Format(rawString, _resourceLoader.GetString(ResourceKey.ALBUM_NAME_KEY),
                album.Name);
            nameTextBlock.Text = albumName;
            nameTextBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindArtistToAlbum);
        }

        private void BindArtistToAlbum(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 2)
            {
                return;
            }

            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            TextBlock artistTextBlock = parent.Children[2] as TextBlock;
            Album album = args.Item as Album;
            Artist artist = SinCollection.GetServerCollection().ArtistList.AsList()
                .Find(obj => obj.Albums.Contains(album));
            string rawString = _resourceLoader.GetString(ResourceKey.RAW_STR_KEY);
            string artistName = string.Format(rawString, _resourceLoader.GetString(ResourceKey.ARTIST_KEY),
                artist.Name);
            artistTextBlock.Text = artistName;
            artistTextBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindProduceDateToAlbum);
        }

        private void BindProduceDateToAlbum(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 3)
            {
                return;
            }

            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            TextBlock produceDateTextBlock = parent.Children[3] as TextBlock;
            Album album = args.Item as Album;
            string rawString = _resourceLoader.GetString(ResourceKey.RAW_STR_KEY);
            string produceDate = string.Format(rawString, _resourceLoader.GetString(ResourceKey.ALBUM_RELEASE_DATE_KEY),
                album.ProduceDate.ToShortDateString());
            produceDateTextBlock.Text = produceDate;
            produceDateTextBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindDescriptionToAlbum);
        }

        private void BindDescriptionToAlbum(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 4)
            {
                return;
            }

            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            TextBlock descriptionTextBlock = parent.Children[4] as TextBlock;
            Album album = args.Item as Album;
            descriptionTextBlock.Text = Encoding.UTF8.GetString(album.Description.FileStream);
            descriptionTextBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindPictureToAlbum);
        }

        private async void BindPictureToAlbum(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 5)
            {
                return;
            }

            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            Album album = args.Item as Album;
            Image img = parent.Children[0] as Image;
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
            Connector.GetInstance().SendAlbumList();
        }
    }
}