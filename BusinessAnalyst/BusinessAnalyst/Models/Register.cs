using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using static BusinessAnalyst.Models.Enumarations;

namespace BusinessAnalyst.Models
{
    public class Register : Entity
    {
        [PrimaryKey, AutoIncrement]
        public int RegisterID { get; set; }
        public string BillNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PartyName { get; set; }
        public string City { get; set; }
        public TransactionType? TransactionType { get; set; }
        public string Amount { get; set; }
    }
}
