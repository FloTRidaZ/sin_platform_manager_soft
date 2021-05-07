using System;
using System.Collections.ObjectModel;

namespace sin_manager_soft.net.pbt.sql.sqlessences
{
    public sealed class Artist
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SinFile Picture { get; set; }
        public SinFile Description { get; set; }
        public ObservableCollection<Album> Albums { get; set; }

        public Artist()
        {
            Albums = new ObservableCollection<Album>();
        }
    }
}