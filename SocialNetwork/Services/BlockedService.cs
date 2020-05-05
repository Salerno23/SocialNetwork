using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SocialNetwork.Interfaces;
using SocialNetwork.Models;


namespace SocialNetwork.Services
{
    public class BlockedService
    {
        private readonly IMongoCollection<Blocked> _blocked;

        public BlockedService(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _blocked = database.GetCollection<Blocked>(settings.BlockedCollectionName);
        }

        public List<Blocked> Get() =>
            _blocked.Find(blocked => true).ToList();

        public Blocked Get(string id) =>
            _blocked.Find<Blocked>(blocked => blocked.Id == id).FirstOrDefault();
        
        public Blocked GetForUser(string userId) =>
            _blocked.Find<Blocked>(blocked => blocked.UserId == userId).FirstOrDefault();

        public Blocked Create(Blocked blocked)
        {
            _blocked.InsertOne(blocked);
            return blocked;
        }

        public void Update(string id, Blocked blockedIn) =>
            _blocked.ReplaceOne(blocked => blocked.Id == id, blockedIn);

        public void Remove(Blocked blockedIn) =>
            _blocked.DeleteOne(blocked => blocked.Id == blockedIn.Id);

        public void Remove(string id) =>
            _blocked.DeleteOne(blocked => blocked.Id == id);
    }
}
