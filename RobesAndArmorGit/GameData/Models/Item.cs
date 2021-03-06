﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameData.Models
{
    public class Item
    {
        
        public int Id { get; set; }
        public int Level { get; set; }        
        public int Health { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Def { get; set; }
        public int Atk { get; set; }       
        public string imgeUrl { get; set; }
        public string Rarity { get; set; }
        public int price { get; set; }


        public int typeId { get; set; }
        public Type Type { get; set; }

        public ICollection<Inventory_has_Item> Inventory_Has_Item { get; } = new List<Inventory_has_Item>();

        /*
        [ForeignKey("Type")]
        public int typeId { get; set; }
        */
    }
}
