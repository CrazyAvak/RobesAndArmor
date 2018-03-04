﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameData.Models
{
    public class Enemy_has_Item
    {
        /// <summary>
        /// many to many table with Item and Enemy
        /// </summary>
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int EnemyId { get; set; }
        public Enemy Enemy { get; set; }
    }
}