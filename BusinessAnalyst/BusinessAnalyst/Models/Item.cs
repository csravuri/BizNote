using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BusinessAnalyst.Models
{
    public class Item : Entity
    {
        [PrimaryKey, AutoIncrement]
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string MRP { get; set; }
    }
}
