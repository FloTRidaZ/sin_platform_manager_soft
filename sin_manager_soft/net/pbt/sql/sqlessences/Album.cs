using System;
using System.Collections.ObjectModel;

namespace sin_manager_soft.net.pbt.sql.sqlessences
{
    public sealed class Album
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        public SinFile Picture { get; set; }
        public SinFile Description { get; set; }
        public ObservableCollection<Song> Songs { get; set; }

        public Album()
        {
            Songs = new ObservableCollection<Song>();
        }
    }
}