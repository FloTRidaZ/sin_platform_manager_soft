using sin_manager_soft.net.pbt.sql.sqlessences;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace sin_manager_soft.net.pbt.page
{
    public sealed partial class ProductListPage : Page
    {
        private readonly List<Product> _products;
        private readonly SINCollection _serverInstance;

        public ProductListPage()
        {
            this.InitializeComponent();
            _serverInstance = SINCollection.GetServerCollection();
            _products = _serverInstance.ProductList;
        }
    }
}
