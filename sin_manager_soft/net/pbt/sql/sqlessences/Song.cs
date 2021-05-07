using System;

namespace sin_manager_soft.net.pbt.sql.sqlessences
{
    public sealed class Song
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SinFile Src { get; set; }
    }
}