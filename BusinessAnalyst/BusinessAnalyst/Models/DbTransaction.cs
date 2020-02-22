using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BusinessAnalyst.Models
{
    public class DbTransaction
    {
        private static DbTransaction dbTransaction;
        private readonly SQLiteAsyncConnection _connection;
        private DbTransaction()
        {
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Database");

            Utils.MakeSureDirectoryExists(folderPath);

            string dbPath = Path.Combine(folderPath, "BusinessAnalystDB.db3");

            _connection = new SQLiteAsyncConnection(dbPath);

            CreateTables();
        }

        public static DbTransaction GetInstance()
        {
            if(dbTransaction == null)
            {
                dbTransaction = new DbTransaction();
            }

            return dbTransaction;
        }

        public async void CreateTables()
        {
            await _connection.CreateTablesAsync(CreateFlags.None, typeof(Party), typeof(Item), typeof(Transaction), typeof(Register), typeof(Document));
        }

        public async void InsertAsync(Entity entity)
        {
            await _connection.InsertAsync(entity);
        }

        public async void InsertAllAsync(List<Transaction> entities)
        {
            await _connection.InsertAllAsync(entities);
        }


        public async void UpdateAsync(Entity entity)
        {
            await _connection.UpdateAsync(entity);
        }

        public async void DeleteAsync(Entity entity)
        {
            await _connection.DeleteAsync(entity);
        }

        public async Task<List<Item>> GetItems()
        {
            return await _connection.Table<Item>().ToListAsync();
        }

        public async Task<List<Party>> GetParties()
        {
            return await _connection.Table<Party>().ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactions()
        {
            return await _connection.Table<Transaction>().ToListAsync();
        }


        public async Task<Document> GetDocument(string documentType)
        {
            List<Document> allDocuments = await _connection.Table<Document>().ToListAsync();
            Document document = allDocuments.Where(x => x.DocumentType == documentType).FirstOrDefault();

            if (document == null)
            {
                document = new Document
                {
                    DocumentNo = 1,
                    DocumentType = documentType,
                    CreatedDate = DateTime.Now
                };
                await _connection.InsertAsync(document);
            }
            else
            {
                document.DocumentNo++;
                await _connection.UpdateAsync(document);
            }
            return document;
        }

    }
}
