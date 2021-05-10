using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using Dapper;
using sin_manager_soft.net.pbt.sql.connector;
using sin_manager_soft.net.pbt.sql.sqlessences;

namespace sin_platform_soft_unit_tests.net.pbt.sql.dao
{
    public sealed class ArtistDao : Dao
    {
        public override void CreateList(IDbConnection connection)
        {
            ObservableCollection<Artist> artists =
                new ObservableCollection<Artist>(connection.Query<Artist>(Query.GET_ARTIST_LIST).AsEnumerable());
            foreach (Artist artist in artists)
            {
                SinFile description =
                    connection.QuerySingle<SinFile>(Query.GET_ARTIST_DESCRIPTION, new { id = artist.Id });
                SinFile picture =
                    connection.QuerySingle<SinFile>(Query.GET_ARTIST_PICTURE, new { id = artist.Id });
                artist.Description = description;
                artist.Picture = picture;
            }

            serverInstance.ArtistList = artists;
        }

        public override void SendList(IDbConnection connection)
        {
            ObservableCollection<Artist> artists = localInstance.ArtistList;
            foreach (Artist artist in artists)
            {
                serverInstance.ArtistList.Add(artist);
                SinFile description = artist.Description;
                SinFile picture = artist.Picture;
                connection.Query(Query.ADD_DESCRIPTION, description);
                connection.Query(Query.ADD_PICTURE, picture);
                var artistParam = new
                {
                    id = artist.Id,
                    name = artist.Name,
                    descriptionId = description.StreamId,
                    pictureId = picture.StreamId
                };
                connection.Query(Query.ADD_ARTIST, artistParam);
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
