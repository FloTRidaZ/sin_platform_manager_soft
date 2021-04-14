using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sin_manager_soft.net.pbt.sql.connector
{
    public sealed class Query
    {
        public const string GET_FROM_PRODUCT_TYPE_VIEW = @"SELECT * FROM product_type_view";
        public const string ADD_PICTURE = @"EXEC add_picture @streamId, @name, @content";
        public const string ADD_DESCRIPTION = @"EXEC add_description @streamId, @name, @content";
        public const string ADD_PRODUCT = @"EXEC add_product @id, @name, @price, @count, @description";
        public const string SET_PRODUCT_PICTURE = @"EXEC set_product_picture @productId, @pictureId";
        public const string SET_PRODUCT_TYPE = @"EXEC set_product_type @productId, @typeId";
        public const string GET_PRODUCT_LIST = @"SELECT id, name, price, count FROM product_view";
        public const string GET_DESCRIPTION = @"SELECT file_stream AS fileStream FROM product_view WHERE id LIKE @id";
        public const string GET_PRODUCT_PICTURES = @"SELECT file_stream AS fileStream FROM product_picture_view WHERE product LIKE @id";
        public const string GET_PRODUCT_TYPE = @"SELECT type FROM product_enum_view WHERE id LIKE @id";

        private Query()
        {

        }
    }
}
