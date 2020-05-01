using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SocialNetwork.Interfaces;
using SocialNetwork.Models;

namespace SocialNetwork.Services
{
    public class FollowsService
    {
        private readonly IMongoCollection<Follows> _follows;

        public FollowsService(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _follows = database.GetCollection<Follows>(settings.FollowsCollectionName);
        }

        public List<Follows> Get() =>
            _follows.Find(follows => true).ToList();

        public Follows Get(string id) =>
            _follows.Find<Follows>(follows => follows.Id == id).FirstOrDefault();

        public Follows Create(Follows follows)
        {
            _follows.InsertOne(follows);
            return follows;
        }

        public void Update(string id, Follows followsIn) =>
            _follows.ReplaceOne(follows => follows.Id == id, followsIn);

        public void Remove(Follows followsIn) =>
            _follows.DeleteOne(follows => follows.Id == followsIn.Id);

        public void Remove(string id) =>
            _follows.DeleteOne(follows => follows.Id == id);
    }
}
