using sin_manager_soft.net.pbt.sql.sqlessences;
using sin_manager_soft.net.pbt.strings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using sin_manager_soft.net.pbt.util;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class ProductCollectionEditorPage
    {
        private readonly List<ProductType> _productTypes;
        private readonly ResourceLoader _resourceLoader;
        private readonly SinCollection _localInstance;
        private readonly SinCollection _serverInstance;
        private readonly ObservableCollection<SinFile> _observablePictures;
        private SinFile _descriptionFile;
        private string _name;
        private int _count;
        private int _price;

        public ProductCollectionEditorPage()
        {
            this.InitializeComponent();
            _productTypes = new List<ProductType>();
            _localInstance = SinCollection.GetLocalCollection();
            _serverInstance = SinCollection.GetServerCollection();
            _resourceLoader = ResourceLoader.GetForCurrentView();
            _observablePictures = new ObservableCollection<SinFile>();
        }

        private async void ImageFileManagerButtonClick(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.Desktop
            };
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpg");
            IReadOnlyList<StorageFile> files = await picker.PickMultipleFilesAsync();
            if (files == null) return;
            StringBuilder fileNames = new StringBuilder();
            foreach (StorageFile file in files)
            {
                fileNames.Append(file.Name + ", ");
                SinFile picture = new SinFile
                {
                    Name = file.Name,
                    FileStream = await StorageFileExtension.GetBytes(file),
                    StreamId = Guid.NewGuid()
                };
                _observablePictures.Add(picture);
            }
        }

        private async void DescriptionFileManagerBtnClick(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.Desktop
            };
            picker.FileTypeFilter.Add(".txt");
            StorageFile file = await picker.PickSingleFileAsync();

            _descriptionFile = new SinFile
            {
                Name = file.Name,
                FileStream = await StorageFileExtension.GetBytes(file),
                StreamId = Guid.NewGuid()
            };
            DeleteDescriptionBtn.Visibility = Visibility.Visible;
            DescriptionFileNameTextBlock.Text = file.Name;
        }

        private void ProductNameInputTextChanged(object sender, TextChangedEventArgs e)
        {
            _name = ProductNameInput.Text;
        }

        private void ProductPriceInputTextChanged(object sender, TextChangedEventArgs e)
        {
            _price = int.Parse(ProductPriceInput.Text);
        }

        private void ProductCountInputTextChanged(object sender, TextChangedEventArgs e)
        {
            _count = int.Parse(ProductCountInput.Text);
        }

        private void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            Product product = new Product
            {
                Id = Guid.NewGuid(),
                Name = _name,
                Price = _price,
                Count = _count,
                Pictures = new List<SinFile>(_observablePictures),
                Description = _descriptionFile,
                ProductTypes = _productTypes
            };
            _localInstance.ProductList.Add(product);
        }

        private void ProductNameTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            SetContent(sender as TextBlock, ResourceKey.PROD_NAME_KEY);
        }

        private void ProductPriceTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            SetContent(sender as TextBlock, ResourceKey.PROD_PRICE_KEY);
        }

        private void ProductCountTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            SetContent(sender as TextBlock, ResourceKey.PROD_COUNT_KEY);
        }

        private void ProductPicturesTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            SetContent(sender as TextBlock, ResourceKey.PICTURE_KEY);
        }

        private void ProductTypesTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            SetContent(sender as TextBlock, ResourceKey.PROD_TYPE_KEY);
        }

        private void ProductDescriptionTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            SetContent(sender as TextBlock, ResourceKey.DESCRIPTION_KEY);
        }

        private void ButtonSaveLoaded(object sender, RoutedEventArgs e)
        {
            SetContent(sender as Button, ResourceKey.SAVE_BTN_KEY);
        }

        private void SetContent(TextBlock textBlock, string key)
        {
            textBlock.Text = _resourceLoader.GetString(key);
        }

        private void SetContent(Button btn, string key)
        {
            btn.Content = _resourceLoader.GetString(key);
        }

        private void CheckBoxGroupLoaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<ProductType> temp = _serverInstance.ProductTypes;
            foreach (ProductType type in temp)
            {
                CheckBox checkBox = new CheckBox
                {
                    Content = type.Name,
                    DataContext = type
                };
                checkBox.Click += ProductTypeCheckBoxClick;
                CheckBoxGroup.Children.Add(checkBox);
            }
        }

        private void ProductTypeCheckBoxClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is CheckBox instance)) return;
            if (instance.IsChecked == true)
            {
                _productTypes.Add(instance.DataContext as ProductType);
                return;
            }

            _productTypes.Remove(instance.DataContext as ProductType);
        }

        private void RemovePictureBtnClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button instance)) return;
            Guid pictureId = (Guid) instance.DataContext;
            foreach (SinFile picture in _observablePictures)
            {
                if (picture.StreamId != pictureId) continue;
                _observablePictures.Remove(picture);
                break;
            }
        }

        private void DeleteDescriptionBtnClick(object sender, RoutedEventArgs e)
        {
            Button instance = sender as Button;
            _descriptionFile = null;
            if (instance != null) instance.Visibility = Visibility.Collapsed;
            DescriptionFileNameTextBlock.Text = "";
        }
    }
}