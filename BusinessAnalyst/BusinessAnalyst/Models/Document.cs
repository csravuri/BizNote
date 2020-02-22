using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BusinessAnalyst.Models
{
    public class Document : Entity
    {
        [PrimaryKey, AutoIncrement]
        public int DocumentID { get; set; }
        [Unique]
        public string DocumentType { get; set; }
        public int DocumentNo { get; set; }
    }
}
