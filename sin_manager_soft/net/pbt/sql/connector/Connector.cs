using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using sin_manager_soft.net.pbt.sql.sqlessences;

namespace sin_manager_soft.net.pbt.sql.connector
{
    public sealed class Connector
    {
        private static Connector _instance;
        private static string CONNECTION_STR = "Server = DESKTOP-HBEEL2G\\SQLEXPRESS; Database = sin_platform_db; User ID = {0}; Password = {1}";
        private readonly SqlConnection _connection;
        private readonly SINCollection _serverInstance;
        private readonly SINCollection _localInstance;

        public static void CreateInstance(string user, string password)
        {
            if (_instance != null)
            {
                return;
            }
            _instance = new Connector(user, password);
            _instance.Connect();
            _instance.CreateCollection();
        }

        public static Connector GetInstance()
        {
            return _instance;
        }

        private Connector(string user, string password)
        {
            CONNECTION_STR = string.Format(CONNECTION_STR, user, password);
            _connection = new SqlConnection(CONNECTION_STR);
            SINCollection.CreateInstances();
            _serverInstance = SINCollection.GetServerCollection();
            _localInstance = SINCollection.GetLocalCollection();
        }

        private void Connect()
        {
            _connection.Open();
        }

        private void CreateCollection()
        {
            List<ProductType> productTypes = _connection.Query<ProductType>(Query.GET_FROM_PRODUCT_TYPE_VIEW).AsList<ProductType>();
            _serverInstance.ProductTypes.AddRange(productTypes);
            List<Product> products = _connection.Query<Product>(Query.GET_PRODUCT_LIST).AsList<Product>();
            foreach (Product product in products)
            {
                product.Description = _connection.QuerySingle<SINFile>(Query.GET_DESCRIPTION, new { id = product.Id });
                product.Pictures = _connection.Query<SINFile>(Query.GET_PRODUCT_PICTURES, new { id = product.Id }).AsList<SINFile>();
                List<int> typeIDList = _connection.Query<int>(Query.GET_PRODUCT_TYPE, new { id = product.Id }).AsList<int>();
                product.ProductTypes = new List<ProductType>();
                foreach (int id in typeIDList)
                {
                    product.ProductTypes.Add(productTypes.Find(type => type.Id == id));
                }
            }
            _serverInstance.ProductList.AddRange(products);
        }

        public void SendToServer()
        {
            List<Product> products = _localInstance.ProductList;
            foreach (Product product in products)
            {
                List<SINFile> pictures = product.Pictures;
                List<ProductType> productTypes = product.ProductTypes;
                var descriptionParam = new
                {
                    streamId = product.Description.StreamId,
                    name = product.Description.Name,
                    content = product.Description.FileStream
                };
                var prodParam = new
                {
                    id = product.Id,
                    name = product.Name,
                    price = product.Price,
                    count = product.Count,
                    description = product.Description.StreamId
                };
                _connection.Query(Query.ADD_DESCRIPTION, descriptionParam);
                _connection.Query(Query.ADD_PRODUCT, prodParam);
                foreach (SINFile picture in pictures)
                {
                    var pictureParam = new
                    {
                        streamId = picture.StreamId,
                        name = picture.Name,
                        content = picture.FileStream
                    };
                    _connection.Query(Query.ADD_PICTURE, pictureParam);
                    _connection.Query(Query.SET_PRODUCT_PICTURE, new { productId = product.Id, pictureId = picture.StreamId });
                }
                foreach (ProductType type in productTypes)
                {
                    _connection.Query(Query.SET_PRODUCT_TYPE, new { productId = product.Id, typeId = type.Id });
                }
            }
        }
    }
}
