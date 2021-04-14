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

        private Query()
        {

        }
    }
}
