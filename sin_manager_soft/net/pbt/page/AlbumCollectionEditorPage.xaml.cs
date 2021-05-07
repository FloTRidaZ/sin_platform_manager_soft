using System;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls;
using sin_manager_soft.net.pbt.sql.sqlessences;
using sin_manager_soft.net.pbt.strings;
using sin_manager_soft.net.pbt.util;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class AlbumCollectionEditorPage
    {
        private readonly ObservableCollection<Artist> _artists;
        private readonly ResourceLoader _resourceLoader;
        private string _name;
        private DateTime _produceDate;
        private SinFile _picture;
        private SinFile _description;
        private Artist _artist;

        public AlbumCollectionEditorPage()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
            _artists = SinCollection.GetServerCollection().ArtistList;
        }

        private void InputNameTextBlockOnLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock instance = sender as TextBlock;
            instance.Text = _resourceLoader.GetString(ResourceKey.ALBUM_NAME_KEY);
        }

        private void SaveAlbumBtnOnLoaded(object sender, RoutedEventArgs e)
        {
            Button instance = sender as Button;
            instance.Content = _resourceLoader.GetString(ResourceKey.SAVE_BTN_KEY);
        }

        private void SaveAlbumBtnOnClick(object sender, RoutedEventArgs e)
        {
            Album album = new Album
            {
                Id = Guid.NewGuid(),
                Name = _name,
                Picture = _picture,
                Description = _description,
                ProduceDate = _produceDate
            };
            _artist.Albums.Add(album);
            SinCollection.GetLocalCollection().AlbumList.Add(album);
        }

        private void DeleteDescriptionBtnClick(object sender, RoutedEventArgs e)
        {
            _description = null;
            DescriptionFileTextBlock.Text = "";
            DeleteDescriptionFileBtn.Visibility = Visibility.Collapsed;
        }

        private async void DescriptionFileManagerBtnClick(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.Desktop,
                FileTypeFilter = {".txt"}
            };
            StorageFile file = await picker.PickSingleFileAsync();
            if (file == null)
            {
                return;
            }

            _description = new SinFile
            {
                Name = file.Name,
                StreamId = Guid.NewGuid(),
                FileStream = await StorageFileExtension.GetBytes(file)
            };
            DescriptionFileTextBlock.Text = _description.Name;
            DeleteDescriptionFileBtn.Visibility = Visibility.Visible;
        }

        private void AlbumDescriptionTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock instance = sender as TextBlock;
            instance.Text = _resourceLoader.GetString(ResourceKey.FILE_MANAGER_KEY);
        }

        private void DescriptionTextBlockOnLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock instance = sender as TextBlock;
            instance.Text = _resourceLoader.GetString(ResourceKey.DESCRIPTION_KEY);
        }

        private void DeletePictureBtnClick(object sender, RoutedEventArgs e)
        {
            _picture = null;
            PictureFileTextBlock.Text = "";
            DeletePictureFileBtn.Visibility = Visibility.Collapsed;
        }

        private async void PictureFileManagerBtnClick(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.Desktop,
                FileTypeFilter = {".png", ".bmp", ".jpg"}
            };
            StorageFile file = await picker.PickSingleFileAsync();
            if (file == null)
            {
                return;
            }

            _picture = new SinFile
            {
                Name = file.Name,
                StreamId = Guid.NewGuid(),
                FileStream = await StorageFileExtension.GetBytes(file)
            };
            PictureFileTextBlock.Text = _picture.Name;
            DeletePictureFileBtn.Visibility = Visibility.Visible;
        }

        private void PictureTextBlockOnLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock instance = sender as TextBlock;
            instance.Text = _resourceLoader.GetString(ResourceKey.PICTURE_KEY);
        }

        private void InputNameTextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            _name = (sender as TextBox).Text;
        }

        private void ProduceDateTextBlockOnLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock instance = sender as TextBlock;
            instance.Text = _resourceLoader.GetString(ResourceKey.ALBUM_RELEASE_DATE_KEY);
        }

        private void ArtistTextBlockOnLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock instance = sender as TextBlock;
            instance.Text = _resourceLoader.GetString(ResourceKey.ARTIST_KEY);
        }

        private void ProduceDatePickerOnDateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            _produceDate = (sender as DatePicker).Date.Date;
        }

        private void AlbumPictureTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock instance = sender as TextBlock;
            instance.Text = _resourceLoader.GetString(ResourceKey.FILE_MANAGER_KEY);
        }

        private void ArtistOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RadioButtons radioButtons = sender as RadioButtons;
            Artist artist = radioButtons.SelectedItem as Artist;
            _artist = artist;
        }
    }
}