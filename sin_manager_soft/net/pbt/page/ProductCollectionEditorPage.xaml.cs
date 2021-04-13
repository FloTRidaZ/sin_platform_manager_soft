using sin_manager_soft.net.pbt.sql.sqlessences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class ProductCollectionEditorPage : Page
    {
        private readonly List<Picture> _pictures;
        private readonly List<ProductType> _productTypes;
        private byte[] _descriptionFile;
        private string _name;
        private int _count;
        private int _price;

        public ProductCollectionEditorPage()
        {
            this.InitializeComponent();
            _pictures = new List<Picture>();
            _productTypes = new List<ProductType>();
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
                    Picture picture = new Picture
                    {
                        Name = file.Name,
                        Content = await GetBytes(file)
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
            _descriptionFile = await GetBytes(file);
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
        }
    }
}
