using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using Dapper;
using sin_manager_soft.net.pbt.sql.connector;
using sin_manager_soft.net.pbt.sql.sqlessences;

namespace sin_platform_soft_unit_tests.net.pbt.sql.dao
{
    public sealed class ProductDao : Dao
    {
        public override void CreateList(IDbConnection connection)
        {
            ObservableCollection<ProductType> productTypes =
                new ObservableCollection<ProductType>(connection.Query<ProductType>(Query.GET_FROM_PRODUCT_TYPE_VIEW)
                    .AsEnumerable());
            serverInstance.ProductTypes = productTypes;
            ObservableCollection<Product> products =
                new ObservableCollection<Product>(connection.Query<Product>(Query.GET_PRODUCT_LIST).AsEnumerable());
            foreach (Product product in products)
            {
                product.Description = connection.QuerySingle<SinFile>(Query.GET_DESCRIPTION, new {id = product.Id});
                product.Pictures = connection.Query<SinFile>(Query.GET_PRODUCT_PICTURES, new {id = product.Id})
                    .AsList();
                List<int> typeIdList =
                    connection.Query<int>(Query.GET_PRODUCT_TYPE, new {id = product.Id}).AsList();
                product.ProductTypes = new List<ProductType>();
                foreach (int id in typeIdList)
                {
                    product.ProductTypes.Add(productTypes.AsList().Find(type => type.Id == id));
                }
            }

            serverInstance.ProductList = products;
        }

        public override void SendList(IDbConnection connection)
        {
            ObservableCollection<Product> products = localInstance.ProductList;
            foreach (Product product in products)
            {
                serverInstance.ProductList.Add(product);
                List<SinFile> pictures = product.Pictures;
                List<ProductType> productTypes = product.ProductTypes;
                SinFile descriptionParam = product.Description;
                connection.Query(Query.ADD_DESCRIPTION, descriptionParam);
                var prodParam = new
                {
                    id = product.Id,
                    name = product.Name,
                    price = product.Price,
                    count = product.Count,
                    description = product.Description.StreamId
                };
                connection.Query(Query.ADD_PRODUCT, prodParam);
                foreach (SinFile picture in pictures)
                {
                    connection.Query(Query.ADD_PICTURE, picture);
                    connection.Query(Query.SET_PRODUCT_PICTURE,
                        new { productId = product.Id, pictureId = picture.StreamId });
                }

                foreach (ProductType type in productTypes)
                {
                    connection.Query(Query.SET_PRODUCT_TYPE, new { productId = product.Id, typeId = type.Id });
                }
            }
        }

        public override void Update(IDbConnection connection, object obj)
        {
        }

        public override void Delete(IDbConnection connection, object obj)
        {
        }
    }
}