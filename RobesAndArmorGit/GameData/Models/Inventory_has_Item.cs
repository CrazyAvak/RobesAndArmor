﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameData.Models
{
    /// <summary>
    /// A many to many relationshop table
    /// </summary>
    public class Inventory_has_Item
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
    }
}
