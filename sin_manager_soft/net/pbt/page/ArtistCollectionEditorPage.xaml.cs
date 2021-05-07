using System;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using sin_manager_soft.net.pbt.sql.sqlessences;
using sin_manager_soft.net.pbt.strings;
using sin_manager_soft.net.pbt.util;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class ArtistCollectionEditorPage
    {
        private readonly ResourceLoader _resourceLoader;
        private SinFile _description;
        private SinFile _picture;
        private string _name;

        public ArtistCollectionEditorPage()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
        }

        private void SaveArtistBtnOnLoaded(object sender, RoutedEventArgs e)
        {
            Button instance = sender as Button;
            instance.Content = _resourceLoader.GetString(ResourceKey.SAVE_BTN_KEY);
        }

        private void SaveArtistBtnOnClick(object sender, RoutedEventArgs e)
        {
            Artist artist = new Artist
            {
                Id = Guid.NewGuid(),
                Description = _description,
                Name = _name,
                Picture = _picture
            };
            SinCollection.GetLocalCollection().ArtistList.Add(artist);
        }

        private void InputNameTextBlockOnLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock instance = sender as TextBlock;
            instance.Text = _resourceLoader.GetString(ResourceKey.ARTIST_NAME_KEY);
        }

        private void ArtistDescriptionTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock instance = sender as TextBlock;
            instance.Text = _resourceLoader.GetString(ResourceKey.FILE_MANAGER_KEY);
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
                Name =  file.Name,
                StreamId = Guid.NewGuid(),
                FileStream = await StorageFileExtension.GetBytes(file)
            };
            DescriptionFileTextBlock.Text = _description.Name;
            DeleteDescriptionFileBtn.Visibility = Visibility.Visible;
        }

        private void DeleteDescriptionBtnClick(object sender, RoutedEventArgs e)
        {
            _description = null;
            DescriptionFileTextBlock.Text = "";
            DeleteDescriptionFileBtn.Visibility = Visibility.Collapsed;
        }

        private void ArtistPictureTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock instance = sender as TextBlock;
            instance.Text = _resourceLoader.GetString(ResourceKey.FILE_MANAGER_KEY);
        }

        private async void PictureFileManagerBtnClick(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.Desktop,
                FileTypeFilter = { ".png", ".bmp", ".jpg" }
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

        private void DeletePictureBtnClick(object sender, RoutedEventArgs e)
        {
            _picture = null;
            PictureFileTextBlock.Text = "";
            DeletePictureFileBtn.Visibility = Visibility.Collapsed;
        }

        private void InputNameTextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            _name = (sender as TextBox).Text;
        }

        private void PictureTextBlockOnLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock instance = sender as TextBlock;
            instance.Text = _resourceLoader.GetString(ResourceKey.PICTURE_KEY);
        }

        private void DescriptionTextBlockOnLoaded(object sender, RoutedEventArgs e)
        {
            TextBlock instance = sender as TextBlock;
            instance.Text = _resourceLoader.GetString(ResourceKey.DESCRIPTION_KEY);
        }
    }
}