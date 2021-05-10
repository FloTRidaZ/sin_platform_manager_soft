using System;
using System.Collections.ObjectModel;
using System.Data;
using Dapper;
using sin_manager_soft.net.pbt.sql.connector;
using sin_manager_soft.net.pbt.sql.sqlessences;

namespace sin_platform_soft_unit_tests.net.pbt.sql.dao
{
    public sealed class SongDao : Dao
    {
        public override void CreateList(IDbConnection connection)
        {
            ObservableCollection<Song> songs =
                new ObservableCollection<Song>(connection.Query<Song>(Query.GET_SONG_LIST));
            foreach (Song song in songs)
            {
                var songIdParam = new
                {
                    id = song.Id
                };
                SinFile src =
                    connection.QuerySingle<SinFile>(Query.GET_SONG_SRC, songIdParam);
                song.Src = src;
                Guid albumId = connection.QuerySingle<Guid>(Query.GET_SONG_ALBUM, songIdParam);
                Album album = serverInstance.AlbumList.AsList().Find(obj => obj.Id.Equals(albumId));
                album.Songs.Add(song);
            }

            serverInstance.SongList = songs;
        }

        public override void SendList(IDbConnection connection)
        {
            ObservableCollection<Song> songs = localInstance.SongList;
            foreach (Song song in songs)
            {
                serverInstance.SongList.Add(song);
                connection.Query(Query.ADD_AUDIO, song.Src);
                var songParam = new
                {
                    id = song.Id,
                    name = song.Name,
                    srcId = song.Src.StreamId,
                    album = serverInstance.AlbumList.AsList().Find(obj => obj.Songs.Contains(song)).Id
                };
                connection.Query(Query.ADD_SONG, songParam);
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