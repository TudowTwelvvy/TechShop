
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Data
{
    public class DatabaseContext : IAsyncDisposable
    {
        private const string DbName = "techShop.db3";
        private static string DbPath() {
            return Path.Combine(FileSystem.AppDataDirectory, DbName);
        }

        private SQLiteAsyncConnection? _connection;

        //perform asynchronous operations such as reading and writing to the database without blocking the user interface
        private SQLiteAsyncConnection Database()
        {
            if (_connection == null)
            {
                _connection = new SQLiteAsyncConnection(
                    DbPath(), 
                    SQLiteOpenFlags.Create |
                    SQLiteOpenFlags.ReadWrite |
                    SQLiteOpenFlags.SharedCache
                );
            }

            return _connection;
        }

        private async Task CreateTableIfNotExists<TTable>() where
            TTable : class, new()
        {
            await Database().CreateTableAsync<TTable>();
        }

        //obtain a table from the database, and if it is not found, create it using CreateTableIfNotExists
        private async Task<AsyncTableQuery<TTable>> GetTableAsync<TTable>() 
            where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return Database().Table<TTable>();
        }

        //get all data from the database
        // create the tables if they do not exist
        public async Task<IEnumerable<TTable>> GetAllAsync<TTable>() 
            where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.ToListAsync();
        }


        //get filtered data from the database based on a predicate
        public async Task<IEnumerable<TTable>>
            GetFileteredAsync<TTable>(Expression<Func<TTable, bool>> predicate)
            where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.Where(predicate).ToListAsync();
        }

        //helper to perform CRUD operations asynchronously
        private async Task<TResult> Execute<TTable, TResult>(Func<Task<TResult>> action)
            where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await action();
        }

        public async Task<TTable> GetItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
        {
            try
            {
                await CreateTableIfNotExists<TTable>();
                //return await Database.GetAsync<TTable>(primaryKey);
                return await Execute<TTable, TTable>(async () => await Database().GetAsync<TTable>(primaryKey));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving item by key: {ex.Message}");
            }
            return null;
        }

        public async Task<bool> AddItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            //return await Database.InsertAsync(item) > 0;
            return await Execute<TTable, bool>(async () => await Database().InsertAsync(item) > 0);
        }

        public async Task<bool> UpdateItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Database().UpdateAsync(item) > 0;
        }

        public async Task<bool> DeleteItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Database().DeleteAsync(item) > 0;
        }

        public async Task<bool> DeleteItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Database().DeleteAsync<TTable>(primaryKey) > 0;
        }

        public async Task<bool> DeleteAllAsync<TTable>() where TTable : class, new()
        {
            await CreateTableIfNotExists<TTable>();
            return await Database().DeleteAllAsync<TTable>() > 0;
        }
        
        //close connection to the database when it is no longer needed
        public async ValueTask DisposeAsync()
        {
            await _connection?.CloseAsync();
        }
    }
}
