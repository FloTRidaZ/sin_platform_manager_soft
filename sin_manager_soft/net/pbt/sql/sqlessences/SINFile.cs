using System;

namespace sin_manager_soft.net.pbt.sql.sqlessences
{
    public sealed class SINFile
    {
        public string Name { get; set; }
        public byte[] FileStream { get; set; }
        public Guid StreamId { get; set; }
    }
}
