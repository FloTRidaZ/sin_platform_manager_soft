using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using sin_manager_soft.net.pbt.sql.sqlessences;

namespace sin_manager_soft.net.pbt.sql.connector
{
    public sealed class Connector
    {
        private static Connector _instance;

        private static string _connectionStr =
            "Server = DESKTOP-HBEEL2G\\SQLEXPRESS; Database = sin_platform_db; User ID = {0}; Password = {1}";

        private readonly SqlConnection _connection;
        private readonly SinCollection _serverInstance;
        private readonly SinCollection _localInstance;

        public static void CreateInstance(string user, string password)
        {
            if (_instance != null)
            {
                return;
            }

            _instance = new Connector(user, password);
            _instance.CreateCollection();
        }

        public static Connector GetInstance()
        {
            return _instance;
        }

        private Connector(string user, string password)
        {
            _connectionStr = string.Format(_connectionStr, user, password);
            _connection = new SqlConnection(_connectionStr);
            SinCollection.CreateInstances();
            _serverInstance = SinCollection.GetServerCollection();
            _localInstance = SinCollection.GetLocalCollection();
        }

        private void CreateCollection()
        {
            InitProductList();
            InitArtistList();
            InitAlbumList();
            InitSongList();
            InitMailServiceList();
        }

        private void InitMailServiceList()
        {
            ObservableCollection<MailService> mailServices =
                new ObservableCollection<MailService>(_connection.Query<MailService>(Query.GET_MAIL_SERVICE_LIST)
                    .AsEnumerable());
            _serverInstance.MailServiceList = mailServices;
        }

        private void InitSongList()
        {
            ObservableCollection<Song> songs =
                new ObservableCollection<Song>(_connection.Query<Song>(Query.GET_SONG_LIST));
            foreach (Song song in songs)
            {
                var songIdParam = new
                {
                    id = song.Id
                };
                SinFile src =
                    _connection.QuerySingle<SinFile>(Query.GET_SONG_SRC, songIdParam);
                song.Src = src;
                Guid albumId = _connection.QuerySingle<Guid>(Query.GET_SONG_ALBUM, songIdParam);
                Album album = _serverInstance.AlbumList.AsList().Find(obj => obj.Id.Equals(albumId));
                album.Songs.Add(song);
            }
        }

        private void InitAlbumList()
        {
            ObservableCollection<Album> albums =
                new ObservableCollection<Album>(_connection.Query<Album>(Query.GET_ALBUM_LIST).AsEnumerable());
            foreach (Album album in albums)
            {
                var albumIdParam = new
                {
                    id = album.Id
                };
                SinFile description =
                    _connection.QuerySingle<SinFile>(Query.GET_ALBUM_DESCRIPTION, albumIdParam);
                SinFile picture =
                    _connection.QuerySingle<SinFile>(Query.GET_ALBUM_PICTURE, albumIdParam);
                album.Description = description;
                album.Picture = picture;
                Guid artistId = _connection.QuerySingle<Guid>(Query.GET_ALBUM_ARTIST_ID, albumIdParam);
                Artist artist = _serverInstance.ArtistList.AsList().Find(obj => obj.Id.Equals(artistId));
                artist.Albums.Add(album);
            }

            _serverInstance.AlbumList = albums;
        }

        private void InitArtistList()
        {
            ObservableCollection<Artist> artists =
                new ObservableCollection<Artist>(_connection.Query<Artist>(Query.GET_ARTIST_LIST).AsEnumerable());
            foreach (Artist artist in artists)
            {
                SinFile description =
                    _connection.QuerySingle<SinFile>(Query.GET_ARTIST_DESCRIPTION, new {id = artist.Id});
                SinFile picture =
                    _connection.QuerySingle<SinFile>(Query.GET_ARTIST_PICTURE, new {id = artist.Id});
                artist.Description = description;
                artist.Picture = picture;
            }

            _serverInstance.ArtistList = artists;
        }

        private void InitProductList()
        {
            ObservableCollection<ProductType> productTypes =
                new ObservableCollection<ProductType>(_connection.Query<ProductType>(Query.GET_FROM_PRODUCT_TYPE_VIEW)
                    .AsEnumerable());
            _serverInstance.ProductTypes = productTypes;
            ObservableCollection<Product> products =
                new ObservableCollection<Product>(_connection.Query<Product>(Query.GET_PRODUCT_LIST).AsEnumerable());
            foreach (Product product in products)
            {
                product.Description = _connection.QuerySingle<SinFile>(Query.GET_DESCRIPTION, new {id = product.Id});
                product.Pictures = _connection.Query<SinFile>(Query.GET_PRODUCT_PICTURES, new {id = product.Id})
                    .AsList();
                List<int> typeIdList =
                    _connection.Query<int>(Query.GET_PRODUCT_TYPE, new {id = product.Id}).AsList();
                product.ProductTypes = new List<ProductType>();
                foreach (int id in typeIdList)
                {
                    product.ProductTypes.Add(productTypes.AsList().Find(type => type.Id == id));
                }
            }

            _serverInstance.ProductList = products;
        }

        public void SendProductList()
        {
            ObservableCollection<Product> products = _localInstance.ProductList;
            foreach (Product product in products)
            {
                _serverInstance.ProductList.Add(product);
                List<SinFile> pictures = product.Pictures;
                List<ProductType> productTypes = product.ProductTypes;
                SinFile descriptionParam = product.Description;
                _connection.Query(Query.ADD_DESCRIPTION, descriptionParam);
                var prodParam = new
                {
                    id = product.Id,
                    name = product.Name,
                    price = product.Price,
                    count = product.Count,
                    description = product.Description.StreamId
                };
                _connection.Query(Query.ADD_PRODUCT, prodParam);
                foreach (SinFile picture in pictures)
                {
                    _connection.Query(Query.ADD_PICTURE, picture);
                    _connection.Query(Query.SET_PRODUCT_PICTURE,
                        new {productId = product.Id, pictureId = picture.StreamId});
                }

                foreach (ProductType type in productTypes)
                {
                    _connection.Query(Query.SET_PRODUCT_TYPE, new {productId = product.Id, typeId = type.Id});
                }
            }
        }

        public void SendArtistList()
        {
            ObservableCollection<Artist> artists = _localInstance.ArtistList;
            foreach (Artist artist in artists)
            {
                _serverInstance.ArtistList.Add(artist);
                SinFile description = artist.Description;
                SinFile picture = artist.Picture;
                _connection.Query(Query.ADD_DESCRIPTION, description);
                _connection.Query(Query.ADD_PICTURE, picture);
                var artistParam = new
                {
                    id = artist.Id,
                    name = artist.Name,
                    descriptionId = description.StreamId,
                    pictureId = picture.StreamId
                };
                _connection.Query(Query.ADD_ARTIST, artistParam);
            }
        }

        public void SendAlbumList()
        {
            ObservableCollection<Album> albums = _localInstance.AlbumList;
            foreach (Album album in albums)
            {
                _serverInstance.AlbumList.Add(album);
                SinFile description = album.Description;
                SinFile picture = album.Picture;
                _connection.Query(Query.ADD_DESCRIPTION, description);
                _connection.Query(Query.ADD_PICTURE, picture);
                var albumParam = new
                {
                    id = album.Id,
                    name = album.Name,
                    produceDate = album.ProduceDate,
                    artist = _serverInstance.ArtistList.AsList().Find(obj => obj.Albums.Contains(album)).Id,
                    descriptionId = description.StreamId,
                    pictureId = picture.StreamId
                };
                _connection.Query(Query.ADD_ALBUM, albumParam);
            }
        }

        public void SendSongList()
        {
            ObservableCollection<Song> songs = _localInstance.SongList;
            foreach (Song song in songs)
            {
                _serverInstance.SongList.Add(song);
                _connection.Query(Query.ADD_AUDIO, song.Src);
                var songParam = new
                {
                    id = song.Id,
                    name = song.Name,
                    srcId = song.Src.StreamId,
                    album = _serverInstance.AlbumList.AsList().Find(obj => obj.Songs.Contains(song)).Id
                };
                _connection.Query(Query.ADD_SONG, songParam);
            }
        }

        public void SendMailServiceList()
        {
            ObservableCollection<MailService> mailServices = _localInstance.MailServiceList;
            foreach (MailService mailService in mailServices)
            {
                _serverInstance.MailServiceList.Add(mailService);
                _connection.Query(Query.ADD_MAIL_SERVICE, mailService);
            }
        }
    }
}