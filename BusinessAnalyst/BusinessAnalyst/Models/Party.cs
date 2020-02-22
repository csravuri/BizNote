using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using static BusinessAnalyst.Models.Enumarations;

namespace BusinessAnalyst.Models
{
    public class Party : Entity
    {
        [PrimaryKey, AutoIncrement]
        public int PartyID { get; set; }
        public string PartyName { get; set; }
        public PartyType? PartyType { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public bool? isCreditor { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }


}
