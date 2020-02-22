using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAnalyst.Models
{
    public class Enumarations
    {
        public enum PartyType
        {
            Other,
            Supplier,
            Customer,
            Loan,
            OwnBankAccount
        }

        public enum TransactionType
        {
            Purchase,
            Sale,
            Misc
        }
    }
}
