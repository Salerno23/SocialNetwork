using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SocialNetwork.Interfaces;
using SocialNetwork.Models;

namespace SocialNetwork.Services
{
    public class WallService
    {
        private readonly IMongoCollection<Wall> _wall;

        public WallService(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _wall = database.GetCollection<Wall>(settings.WallCollectionName);
        }

        public List<Wall> Get() =>
            _wall.Find(wall => true).ToList();

        public Wall Get(string id) =>
            _wall.Find<Wall>(wall => wall.Id == id).FirstOrDefault();

        public Wall Create(Wall wall)
        {
            _wall.InsertOne(wall);
            return wall;
        }

        public void Update(string id, Wall wallIn) =>
            _wall.ReplaceOne(wall => wall.Id == id, wallIn);

        public void Remove(Wall wallIn) =>
            _wall.DeleteOne(wall => wall.Id == wallIn.Id);

        public void Remove(string id) =>
            _wall.DeleteOne(wall => wall.Id == id);
    }
}
