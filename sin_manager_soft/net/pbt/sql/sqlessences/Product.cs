﻿using System;
using System.Collections.Generic;

namespace sin_manager_soft.net.pbt.sql.sqlessences
{
    public sealed class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public byte[] Description { get; set; }
        public List<Picture> Pictures { get; set; }
        public List<ProductType> ProductTypes { get; set; }
    }
}
