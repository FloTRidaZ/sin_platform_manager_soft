using System.Data;
using System.Data.SqlClient;
using sin_platform_soft_unit_tests.net.pbt.sql.connector;
using sin_platform_soft_unit_tests.net.pbt.sql.dao;

namespace sin_manager_soft.net.pbt.sql.connector
{
    public sealed class Connector : IDBConnector
    {
        private static Connector _instance;

        private static string _connectionStr =
            "Server = DESKTOP-HBEEL2G\\SQLEXPRESS; Database = sin_platform_db; User ID = {0}; Password = {1}";

        private readonly IDbConnection _connection;

        private readonly Dao _productDao;
        private readonly Dao _artistDao;
        private readonly Dao _albumDao;
        private readonly Dao _songDao;
        private readonly Dao _mailServiceDao;

        public static void CreateInstance(string user, string password)
        {
            if (_instance != null)
            {
                return;
            }

            _instance = new Connector(user, password);
        }

        public static Connector GetInstance()
        {
            return _instance;
        }

        private Connector(string user, string password)
        {
            _connectionStr = string.Format(_connectionStr, user, password);
            _connection = new SqlConnection(_connectionStr);
            _productDao = new ProductDao();
            _artistDao = new ArtistDao();
            _albumDao = new AlbumDao();
            _songDao = new SongDao();
            _mailServiceDao = new MailServiceDao();
        }

        public void Connect()
        {
            _productDao.CreateList(_connection);
            _artistDao.CreateList(_connection);
            _albumDao.CreateList(_connection);
            _songDao.CreateList(_connection);
            _mailServiceDao.CreateList(_connection);
        }

        public void SendProductList()
        {
            _productDao.SendList(_connection);
        }

        public void SendArtistList()
        {
            _artistDao.SendList(_connection);
        }

        public void SendAlbumList()
        {
            _albumDao.SendList(_connection);
        }

        public void SendSongList()
        {
            _songDao.SendList(_connection);
        }

        public void SendMailServiceList()
        {
            _mailServiceDao.SendList(_connection);
        }
    }
}