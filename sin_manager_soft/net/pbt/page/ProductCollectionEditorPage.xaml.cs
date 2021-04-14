using sin_manager_soft.net.pbt.sql.connector;
using sin_manager_soft.net.pbt.sql.sqlessences;
using sin_manager_soft.net.pbt.strings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class ProductCollectionEditorPage : Page
    {
        private readonly List<SINFile> _pictures;
        private readonly List<ProductType> _productTypes;
        private readonly ResourceLoader _resourceLoader;
        private readonly SINCollection _localInstance;
        private readonly SINCollection _serverInstance;
        private SINFile _descriptionFile;
        private string _name;
        private int _count;
        private int _price;

        public ProductCollectionEditorPage()
        {
            this.InitializeComponent();
            _pictures = new List<SINFile>();
            _productTypes = new List<ProductType>();
            _localInstance = SINCollection.GetLocalCollection();
            _serverInstance = SINCollection.GetServerCollection();
            _resourceLoader = ResourceLoader.GetForCurrentView();
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
            if (files != null)
            {
                StringBuilder fileNames = new StringBuilder();
                foreach (StorageFile file in files)
                {
                    fileNames.Append(file.Name + ", ");
                    SINFile picture = new SINFile
                    {
                        Name = file.Name,
                        Content = await GetBytes(file),
                        StreamId = Guid.NewGuid()
                    };
                    _pictures.Add(picture);
                }
                _imagesTextBlock.Text = fileNames.ToString();
            }
        }

        private async Task<byte[]> GetBytes(StorageFile file)
        {
            IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
            DataReader dataReader = new DataReader(stream.GetInputStreamAt(0));
            byte[] bytes = new byte[stream.Size];
            await dataReader.LoadAsync((uint)stream.Size);
            dataReader.ReadBytes(bytes);
            return bytes;
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

            _descriptionFile = new SINFile
            {
                Name = file.Name,
                Content = await GetBytes(file),
                StreamId = Guid.NewGuid()
            };
            _descriptionInput.Text = file.Name;
        }

        private void ProductNameInputTextChanged(object sender, TextChangedEventArgs e)
        {
            _name = _productNameInput.Text;
        }

        private void ProductPriceInputTextChanged(object sender, TextChangedEventArgs e)
        {
            _price = int.Parse(_productPriceInput.Text);
        }

        private void ProductCountInputTextChanged(object sender, TextChangedEventArgs e)
        {
            _count = int.Parse(_productCountInput.Text);
        }

        private void CreateNewProductButtonClick(object sender, RoutedEventArgs e)
        {
            Product product = new Product
            {
                Id = Guid.NewGuid(),
                Name = _name,
                Price = _price,
                Count = _count,
                Pictures = _pictures,
                Description = _descriptionFile,
                ProductTypes = _productTypes
            };
            _localInstance.ProductList.Add(product);
            Connector.GetInstance().SendToServer();
        }

        private void ProductNameTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            SetContent(sender as TextBlock, ResourceKey.PRODUCT_NAME_KEY);
        }

        private void ProductPriceTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            SetContent(sender as TextBlock, ResourceKey.PRODUCT_PRICE_KEY);
        }

        private void ProductCountTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            SetContent(sender as TextBlock, ResourceKey.PRODUCT_COUNT_KEY);
        }

        private void ProductPicturesTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            SetContent(sender as TextBlock, ResourceKey.PRODUCT_PICTURES_KEY);
        }

        private void ProductTypesTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            SetContent(sender as TextBlock, ResourceKey.PRODUCT_TYPES_KEY);
        }

        private void ProductDescriptionTextBlockLoaded(object sender, RoutedEventArgs e)
        {
            SetContent(sender as TextBlock, ResourceKey.PRODUCT_DESCRIPTION_KEY);
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
            List<ProductType> temp = _serverInstance.ProductTypes;
            foreach (ProductType type in temp)
            {
                CheckBox checkBox = new CheckBox
                {
                    Content = type.Name,
                    DataContext = type
                };
                checkBox.Click += ProductTypeCheckBoxClick;
                _checkBoxGroup.Children.Add(checkBox);
            }
        }

        private void ProductTypeCheckBoxClick(object sender, RoutedEventArgs e)
        {
            CheckBox instance = sender as CheckBox;
            if (instance.IsChecked == true)
            {
                _productTypes.Add(instance.DataContext as ProductType);
                return;
            }
            _productTypes.Remove(instance.DataContext as ProductType);
        }
    }
}
