using sin_manager_soft.net.pbt.sql.sqlessences;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class ProductListPage : Page
    {
        private readonly List<Product> _products;
        private readonly SINCollection _serverInstance;
        private readonly ResourceLoader _resourceLoader;

        public ProductListPage()
        {
            this.InitializeComponent();
            _serverInstance = SINCollection.GetServerCollection();
            _products = _serverInstance.ProductList;
            _resourceLoader = ResourceLoader.GetForCurrentView();
        }

        private void ProductListContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 0)
            {
                return;
            }
            args.RegisterUpdateCallback(BindNameToProduct);
        }

        private void BindNameToProduct(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 1)
            {
                return;
            }
            Product product = args.Item as Product;
            StackPanel parent = args.ItemContainer.ContentTemplateRoot as StackPanel;
            TextBlock textBlock = parent.Children[1] as TextBlock;
            textBlock.Text = product.Name;
            textBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindPriceToProduct);
        }

        private void BindPriceToProduct(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 2)
            {
                return;
            }
            Product product = args.Item as Product;
            StackPanel parent = args.ItemContainer.ContentTemplateRoot as StackPanel;
            TextBlock textBlock = parent.Children[2] as TextBlock;
            textBlock.Text = (product.Price / 100).ToString();
            textBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindCountToProduct);
        }

        private void BindCountToProduct(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 3)
            {
                return;
            }
            Product product = args.Item as Product;
            StackPanel parent = args.ItemContainer.ContentTemplateRoot as StackPanel;
            TextBlock textBlock = parent.Children[3] as TextBlock;
            textBlock.Text = product.Count.ToString();
            textBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindProductEnumToProduct);
        }

        private void BindProductEnumToProduct(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 4)
            {
                return;
            }
            Product product = args.Item as Product;
            StackPanel parent = args.ItemContainer.ContentTemplateRoot as StackPanel;
            TextBlock textBlock = parent.Children[4] as TextBlock;
            StringBuilder strBuilder = new StringBuilder();
            foreach (ProductType type in product.ProductTypes)
            {
                strBuilder.Append(type.Name).Append("; ");
            }
            textBlock.Text = strBuilder.ToString();
            textBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindProductDescriptionProduct);
        }

        private void BindProductDescriptionProduct(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 5)
            {
                return;
            }
            Product product = args.Item as Product;
            StackPanel parent = args.ItemContainer.ContentTemplateRoot as StackPanel;
            TextBlock textBlock = parent.Children[5] as TextBlock;
            textBlock.Text = Encoding.ASCII.GetString(product.Description.FileStream);
            textBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindPictureToproduct);
        }

        private async void BindPictureToproduct(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 6)
            {
                return;
            }
            Product product = args.Item as Product;
            StackPanel parent = args.ItemContainer.ContentTemplateRoot as StackPanel;
            Image img = parent.Children[0] as Image;
            BitmapImage bitmap = new BitmapImage();
            await bitmap.SetSourceAsync(WindowsRuntimeStreamExtensions.AsRandomAccessStream(new MemoryStream(product.Pictures[0].FileStream)));
            img.Source = bitmap;
            img.Opacity = 1;
        }
    }
}
