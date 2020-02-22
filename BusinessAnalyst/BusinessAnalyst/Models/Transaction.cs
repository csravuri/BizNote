using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using static BusinessAnalyst.Models.Enumarations;

namespace BusinessAnalyst.Models
{
    public class Transaction : Entity
    {
        [PrimaryKey, AutoIncrement]
        public int TransactionID { get; set; }
        public string BillNo { get; set; }
        public TransactionType? TransactionType { get; set; }
        public string PartyName { get; set; }
        public PartyType? PartyType { get; set; }
        public string City { get; set; }
        public string ItemName { get; set; }
        public string Qty { get; set; }
        public string MRP { get; set; }
        public string Amount { get; set; }

    }
}
