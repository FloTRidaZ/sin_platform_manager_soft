using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using sin_manager_soft.net.pbt.sql.connector;
using sin_manager_soft.net.pbt.sql.sqlessences;
using sin_manager_soft.net.pbt.strings;


namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class ProductWrapperPage
    {
        private readonly ObservableCollection<Product> _products;
        private readonly ResourceLoader _resourceLoader;

        public ProductWrapperPage()
        {
            this.InitializeComponent();
            _products = SinCollection.GetLocalCollection().ProductList;
            _resourceLoader = ResourceLoader.GetForCurrentView();
        }

        private void WrapperContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
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

            string rawStr = _resourceLoader.GetString(ResourceKey.RAW_STR_KEY);
            string rawProdName = _resourceLoader.GetString(ResourceKey.PROD_NAME_KEY);
            Product product = args.Item as Product;
            string prodName = string.Format(rawStr, rawProdName, product.Name);
            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            TextBlock textBlock = parent.Children[1] as TextBlock;
            textBlock.Text = prodName;
            textBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindPriceToProduct);
        }

        private void BindPriceToProduct(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 2)
            {
                return;
            }

            string rawStr = _resourceLoader.GetString(ResourceKey.RAW_PRICE_STR_KEY);
            string rawProdPrice = _resourceLoader.GetString(ResourceKey.PROD_PRICE_KEY);
            Product product = args.Item as Product;
            string prodPrice = string.Format(rawStr, rawProdPrice, (product.Price / 100));
            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            TextBlock textBlock = parent.Children[2] as TextBlock;
            textBlock.Text = prodPrice;
            textBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindCountToProduct);
        }

        private void BindCountToProduct(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 3)
            {
                return;
            }

            string rawStr = _resourceLoader.GetString(ResourceKey.RAW_STR_KEY);
            string rawProdCount = _resourceLoader.GetString(ResourceKey.PROD_COUNT_KEY);
            Product product = args.Item as Product;
            string prodCount = string.Format(rawStr, rawProdCount, product.Count);
            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            TextBlock textBlock = parent.Children[3] as TextBlock;
            textBlock.Text = prodCount;
            textBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindProductEnumToProduct);
        }

        private void BindProductEnumToProduct(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 4)
            {
                return;
            }

            string rawStr = _resourceLoader.GetString(ResourceKey.RAW_STR_KEY);
            string rawProdTypes = _resourceLoader.GetString(ResourceKey.PROD_TYPE_KEY);
            Product product = args.Item as Product;
            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            TextBlock textBlock = parent.Children[4] as TextBlock;
            StringBuilder strBuilder = new StringBuilder();
            foreach (ProductType type in product.ProductTypes)
            {
                strBuilder.Append(type.Name).Append("; ");
            }

            string prodTypes = string.Format(rawStr, rawProdTypes, strBuilder);
            textBlock.Text = prodTypes;
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
            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            TextBlock textBlock = parent.Children[5] as TextBlock;
            textBlock.Text = Encoding.UTF8.GetString(product.Description.FileStream);
            textBlock.Opacity = 1;
            args.RegisterUpdateCallback(BindPictureToProduct);
        }

        private async void BindPictureToProduct(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 6)
            {
                return;
            }

            Product product = args.Item as Product;
            RelativePanel parent = args.ItemContainer.ContentTemplateRoot as RelativePanel;
            Image img = parent.Children[0] as Image;
            BitmapImage bitmap = new BitmapImage();
            await bitmap.SetSourceAsync(
                new MemoryStream(product.Pictures[0].FileStream).AsRandomAccessStream());
            img.Source = bitmap;
            img.Opacity = 1;
        }

        private void SendBtnOnLoaded(object sender, RoutedEventArgs e)
        {
            Button instance = sender as Button;
            instance.Content = _resourceLoader.GetString(ResourceKey.SEND_BTN_KEY);
        }

        private void SendBtnOnClick(object sender, RoutedEventArgs e)
        {
            Connector.GetInstance().SendProductList();
        }
    }
}