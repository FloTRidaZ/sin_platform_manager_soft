using System;
using Windows.ApplicationModel.Resources;
using sin_manager_soft.net.pbt.strings;

namespace sin_manager_soft.net.pbt.util
{
    public static class CategoryHeaderSelector
    {
        public static string GetHeader(Category category, ResourceLoader resourceLoader)
        {
            string header;
            switch (category)
            {
                case Category.ALBUM:
                    header = resourceLoader.GetString(ResourceKey.ALBUM_VIEW_KEY);
                    break;
                case Category.ARTIST:
                    header = resourceLoader.GetString(ResourceKey.ARTIST_VIEW_KEY);
                    break;
                case Category.SONG:
                    header = resourceLoader.GetString(ResourceKey.SONG_VIEW_KEY);
                    break;
                case Category.MAIL_SERVICE:
                    header = resourceLoader.GetString(ResourceKey.MAIL_VIEW_KEY);
                    break;
                case Category.PRODUCT:
                    header = resourceLoader.GetString(ResourceKey.PROD_VIEW_KEY);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(category), category, null);
            }

            return header;
        }
    }
}