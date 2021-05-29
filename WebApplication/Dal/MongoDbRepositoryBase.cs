using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace WebApplication.Dal
{
    public class MongoDbRepositoryBase<T> : IMongoDbRepositoryBase<T> where T : class, new()
    {
        private IMongoDatabase _database;
        protected readonly IMongoCollection<T> _collection;
        public MongoDbRepositoryBase()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("chat");
            _collection = _database.GetCollection<T>(typeof(T).Name);
        }


        public async Task Create(T obj)
        {
            await _collection.InsertOneAsync(obj);
        }

        public async Task<List<T>> Get()
        {
            var all = await _collection.FindAsync(Builders<T>.Filter.Empty);
            return await all.ToListAsync();
        }
    }
}