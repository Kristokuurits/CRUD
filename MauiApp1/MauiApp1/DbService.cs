using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiApp1
{
    public class DbService
    {
        private const string DB_Name = "myDatabase";
        private readonly SQLiteAsyncConnection _connection;

        public DbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(".", DB_Name));
            _connection.CreateTableAsync<Item>();
        }

        public async Task<List<Item>> GetItems()
        {
            return await _connection.Table<Item>().ToListAsync();
        }

        public async Task<Item> GetById(int id)
        {
            return await _connection.Table<Item>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Item Item)
        {
            await _connection.InsertAsync(Item);
        }

        public async Task Update(Item Item)
        {
            await _connection.UpdateAsync(Item);
        }

        public async Task Delete(Item Item)
        {
            await _connection.DeleteAsync(Item);
        }
    }
}
