using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using Dapper;
using sin_manager_soft.net.pbt.sql.connector;
using sin_manager_soft.net.pbt.sql.sqlessences;

namespace sin_platform_soft_unit_tests.net.pbt.sql.dao
{
    public sealed class AlbumDao : Dao
    {
        public override void CreateList(IDbConnection connection)
        {
            ObservableCollection<Album> albums =
                new ObservableCollection<Album>(connection.Query<Album>(Query.GET_ALBUM_LIST).AsEnumerable());
            foreach (Album album in albums)
            {
                var albumIdParam = new
                {
                    id = album.Id
                };
                SinFile description =
                    connection.QuerySingle<SinFile>(Query.GET_ALBUM_DESCRIPTION, albumIdParam);
                SinFile picture =
                    connection.QuerySingle<SinFile>(Query.GET_ALBUM_PICTURE, albumIdParam);
                album.Description = description;
                album.Picture = picture;
                Guid artistId = connection.QuerySingle<Guid>(Query.GET_ALBUM_ARTIST_ID, albumIdParam);
                Artist artist = serverInstance.ArtistList.AsList().Find(obj => obj.Id.Equals(artistId));
                artist.Albums.Add(album);
            }

            serverInstance.AlbumList = albums;
        }

        public override void SendList(IDbConnection connection)
        {
            ObservableCollection<Album> albums = localInstance.AlbumList;
            foreach (Album album in albums)
            {
                serverInstance.AlbumList.Add(album);
                SinFile description = album.Description;
                SinFile picture = album.Picture;
                connection.Query(Query.ADD_DESCRIPTION, description);
                connection.Query(Query.ADD_PICTURE, picture);
                var albumParam = new
                {
                    id = album.Id,
                    name = album.Name,
                    produceDate = album.ProduceDate,
                    artist = serverInstance.ArtistList.AsList().Find(obj => obj.Albums.Contains(album)).Id,
                    descriptionId = description.StreamId,
                    pictureId = picture.StreamId
                };
                connection.Query(Query.ADD_ALBUM, albumParam);
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
