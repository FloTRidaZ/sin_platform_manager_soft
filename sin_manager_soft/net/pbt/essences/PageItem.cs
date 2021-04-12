using System;

namespace sin_manager_soft.net.pbt.essences
{
    public sealed class PageItem
    {
        public string Header { get; set; }
        public Type Page { get; set; }
        public bool IsEnabled { get; set; }
    }
}
