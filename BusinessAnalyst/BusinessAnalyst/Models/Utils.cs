using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static BusinessAnalyst.Models.Enumarations;

namespace BusinessAnalyst.Models
{
    public class Utils
    {
        public static void MakeSureDirectoryExists(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

       
        public static string GetDateTimeFileName(string extension = "")
        {
            return $"{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss_fff")}{extension}";
        }

        public static decimal ToDecimal(string inputString)
        {
            decimal result = 0;
            decimal.TryParse(inputString, out result);
            return result;
        }

        public static TransactionType? GetTransactionType(PartyType? partyType)
        {
            switch (partyType)
            {
                case PartyType.Supplier:
                    return TransactionType.Purchase;
                case PartyType.Customer:
                    return TransactionType.Sale;
                default:
                    return TransactionType.Misc;
            }
        }

        public async static Task<string> GetBillNo(PartyType? partyType)
        {
            DbTransaction dbTransaction = DbTransaction.GetInstance();
            Document document;
            switch (partyType)
            {
                case PartyType.Supplier:
                    document = await dbTransaction.GetDocument("PO");
                    return $"{document.DocumentType}-{document.DocumentNo}";
                case PartyType.Customer:
                    document = await dbTransaction.GetDocument("SO");
                    return $"{document.DocumentType}-{document.DocumentNo}";
                default:
                    document = await dbTransaction.GetDocument("MI");
                    return $"{document.DocumentType}-{document.DocumentNo}";
            }
        }
    }
}
