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
    public sealed partial class SongCollectionEditorPage
    {
        private readonly ObservableCollection<Album> _albums;
        private readonly ResourceLoader _resourceLoader;
        private string _name;
        private SinFile _src;
        private Album _album;

        public SongCollectionEditorPage()
        {
            this.InitializeComponent();
            _resourceLoader = ResourceLoader.GetForCurrentView();
            _albums = SinCollection.GetServerCollection().AlbumList;
        }

        private void InputNameTextBlockOnLoaded(object sender, RoutedEventArgs e)
        {
            (sender as TextBlock).Text = _resourceLoader.GetString(ResourceKey.NAME_KEY);
        }

        private void InputNameTextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            _name = (sender as TextBox).Text;
        }

        private void SrcTextBlockOnLoaded(object sender, RoutedEventArgs e)
        {
            (sender as TextBlock).Text = _resourceLoader.GetString(ResourceKey.SONG_SRC_KEY);
        }

        private void FilManagerTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            (sender as TextBlock).Text = _resourceLoader.GetString(ResourceKey.FILE_MANAGER_KEY);
        }

        private async void SrcFileManagerBtnClick(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.Desktop,
                FileTypeFilter = {".mp3"}
            };
            StorageFile file = await picker.PickSingleFileAsync();
            if (file == null)
            {
                return;
            }

            _src = new SinFile
            {
                Name = file.Name,
                StreamId = Guid.NewGuid(),
                FileStream = await StorageFileExtension.GetBytes(file)
            };
            SrcFileNameTextBlock.Text = _src.Name;
            DeleteSrcFileBtn.Visibility = Visibility.Visible;
        }

        private void DeleteSrcBtnClick(object sender, RoutedEventArgs e)
        {
            _src = null;
            SrcFileNameTextBlock.Text = "";
            DeleteSrcFileBtn.Visibility = Visibility.Collapsed;
        }

        private void AlbumTextBlockOnLoaded(object sender, RoutedEventArgs e)
        {
            (sender as TextBlock).Text = _resourceLoader.GetString(ResourceKey.ALBUM_KEY);
        }

        private void SaveSongBtnOnLoaded(object sender, RoutedEventArgs e)
        {
            (sender as Button).Content = _resourceLoader.GetString(ResourceKey.SAVE_BTN_KEY);
        }

        private void SaveSongBtnOnClick(object sender, RoutedEventArgs e)
        {
            Song song = new Song
            {
                Id = Guid.NewGuid(),
                Name = _name,
                Src = _src
            };
            _album.Songs.Add(song);
            SinCollection.GetLocalCollection().SongList.Add(song);
        }

        private void AlbumOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RadioButtons radioButtons = sender as RadioButtons;
            _album = radioButtons.SelectedItem as Album;
        }
    }
}