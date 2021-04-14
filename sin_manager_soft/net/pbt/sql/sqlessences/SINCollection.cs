using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sin_manager_soft.net.pbt.sql.sqlessences
{
    public sealed class SINCollection
    {
        private static Dictionary<SINCollectionInstances, SINCollection> _instances;

        public List<Product> ProductList { get; }
        public List<ProductType> ProductTypes { get; }

        public static void CreateInstances()
        {
            if (_instances != null)
            {
                return;
            }
            _instances = new Dictionary<SINCollectionInstances, SINCollection>
            {
                { SINCollectionInstances.LOCAL, new SINCollection() },
                { SINCollectionInstances.SERVER, new SINCollection() }
            };
        }

        public static SINCollection GetLocalCollection()
        {
            return _instances[SINCollectionInstances.LOCAL];
        }

        public static SINCollection GetServerCollection()
        {
            return _instances[SINCollectionInstances.SERVER];
        }

        private SINCollection()
        {
            ProductList = new List<Product>();
            ProductTypes = new List<ProductType>();
        }
    }
}
