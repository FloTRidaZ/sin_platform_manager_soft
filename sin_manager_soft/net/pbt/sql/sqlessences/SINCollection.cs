using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace sin_manager_soft.net.pbt.sql.sqlessences
{
    public sealed class SinCollection
    {
        private static Dictionary<SinCollectionInstances, SinCollection> _instances;

        public ObservableCollection<Product> ProductList { get; set; }
        public ObservableCollection<ProductType> ProductTypes { get; set; }
        public ObservableCollection<Artist> ArtistList { get; set; }
        public ObservableCollection<Album> AlbumList { get; set; }
        public ObservableCollection<Song> SongList { get; set; }
        public ObservableCollection<MailService> MailServiceList { get; set; }

        public static void CreateInstances()
        {
            if (_instances != null)
            {
                return;
            }

            _instances = new Dictionary<SinCollectionInstances, SinCollection>
            {
                {SinCollectionInstances.LOCAL, new SinCollection()},
                {SinCollectionInstances.SERVER, new SinCollection()}
            };
        }

        public static SinCollection GetLocalCollection()
        {
            return _instances[SinCollectionInstances.LOCAL];
        }

        public static SinCollection GetServerCollection()
        {
            return _instances[SinCollectionInstances.SERVER];
        }

        private SinCollection()
        {
            ProductList = new ObservableCollection<Product>();
            ProductTypes = new ObservableCollection<ProductType>();
            ArtistList = new ObservableCollection<Artist>();
            AlbumList = new ObservableCollection<Album>();
            SongList = new ObservableCollection<Song>();
            MailServiceList = new ObservableCollection<MailService>();
        }
    }
}