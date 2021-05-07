namespace sin_manager_soft.net.pbt.sql.connector
{
    public static class Query
    {
        public const string GET_FROM_PRODUCT_TYPE_VIEW = @"SELECT * FROM product_type_view";
        public const string ADD_PICTURE = @"EXEC add_picture @StreamId, @Name, @FileStream";
        public const string ADD_DESCRIPTION = @"EXEC add_description @StreamId, @Name, @FileStream";
        public const string ADD_PRODUCT = @"EXEC add_product @id, @name, @price, @count, @description";
        public const string SET_PRODUCT_PICTURE = @"EXEC set_product_picture @productId, @pictureId";
        public const string SET_PRODUCT_TYPE = @"EXEC set_product_type @productId, @typeId";
        public const string GET_PRODUCT_LIST = @"SELECT id, name, price, count FROM product_view";
        public const string GET_DESCRIPTION = @"SELECT file_stream AS fileStream FROM product_view WHERE id LIKE @id";

        public const string GET_PRODUCT_PICTURES =
            @"SELECT file_stream AS fileStream FROM product_picture_view WHERE product LIKE @id";

        public const string GET_PRODUCT_TYPE = @"SELECT type FROM product_enum_view WHERE id LIKE @id";
        public const string ADD_ARTIST = @"EXEC add_artist @id, @name, @descriptionId, @pictureId";
        public const string GET_ARTIST_LIST = @"SELECT id, name FROM artist_view";
        public const string GET_ARTIST_PICTURE = @"SELECT picture AS fileStream FROM artist_view WHERE id LIKE @id";

        public const string GET_ARTIST_DESCRIPTION =
            @"SELECT description AS fileStream FROM artist_view WHERE id LIKE @id";

        public const string GET_ALBUM_LIST = @"SELECT id, name, produce_date AS produceDate FROM album_view";
        public const string GET_ALBUM_PICTURE = @"SELECT picture AS fileStream FROM album_view WHERE id LIKE @id";
        public const string GET_ALBUM_DESCRIPTION = @"SELECT description AS fileStream FROM album_view WHERE id LIKE @id";
        public const string GET_ALBUM_ARTIST_ID = @"SELECT artist FROM album_view WHERE id LIKE @id";
        public const string GET_SONG_LIST = @"SELECT id, name FROM song_view";
        public const string GET_SONG_ALBUM = @"SELECT album FROM song_view WHERE id LIKE @id";
        public const string GET_SONG_SRC = @"SELECT src FROM song_view WHERE id LIKE @id";
        public const string ADD_ALBUM = @"EXEC add_album @id, @name, @produceDate, @artist, @descriptionId, @pictureId";
        public const string ADD_AUDIO = @"EXEC add_audio @StreamId, @Name, @FileStream";
        public const string ADD_SONG = @"EXEC add_song @id, @name, @album, @srcId";
        public const string ADD_MAIL_SERVICE = @"EXEC add_mail_service @id, @name";
        public const string GET_MAIL_SERVICE_LIST = @"SELECT * FROM mail_service_view";
    }
}