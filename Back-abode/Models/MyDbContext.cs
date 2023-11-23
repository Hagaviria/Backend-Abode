using Back_Abode.Models;
using MongoDB.Driver;

namespace Back_Abode.Models
{
    public class MyDbContext
    {
        private readonly IMongoDatabase _database;

        public MyDbContext(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoCollection<Users> Users => _database.GetCollection<Users>("Users");
    }
}
