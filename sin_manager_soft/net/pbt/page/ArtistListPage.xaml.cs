using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using sin_manager_soft.net.pbt.sql.sqlessences;
using sin_manager_soft.net.pbt.strings;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class ArtistListPage
    {
        private readonly ObservableCollection<Artist> _artists;
        private readonly ResourceLoader _resourceLoader;

        public ArtistListPage()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
            _artists = SinCollection.GetServerCollection().ArtistList;
        }

        private void ArtistListContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 0)
            {
                return;
            }

            args.RegisterUpdateCallback(BindNameToArtist);
        }

        private void BindNameToArtist(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 1)
            {
                return;
            }

            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            TextBlock nameTextBlock = parent.Children[1] as TextBlock;
            Artist artist = args.Item as Artist;
            string rawString = _resourceLoader.GetString(ResourceKey.RAW_STR_KEY);
            string artistName = string.Format(rawString, _resourceLoader.GetString(ResourceKey.ARTIST_NAME_KEY),
                artist.Name);
            nameTextBlock.Text = artistName;
            nameTextBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindDescriptionToArtist);
        }

        private void BindDescriptionToArtist(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 2)
            {
                return;
            }

            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            TextBlock descriptionTextBlock = parent.Children[2] as TextBlock;
            Artist artist = args.Item as Artist;
            descriptionTextBlock.Text = Encoding.UTF8.GetString(artist.Description.FileStream);
            descriptionTextBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindImageToArtist);
        }

        private async void BindImageToArtist(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 3)
            {
                return;
            }

            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            Image img = parent.Children[0] as Image;
            Artist artist = args.Item as Artist;
            BitmapImage bitmap = new BitmapImage();
            await bitmap.SetSourceAsync(new MemoryStream(artist.Picture.FileStream).AsRandomAccessStream());
            img.Source = bitmap;
            img.Opacity = 1;
        }
    }
}