using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SocialNetwork.Interfaces;
using SocialNetwork.Models;

namespace SocialNetwork.Services
{
    public class SocialNetworkService
    {
        private readonly IMongoCollection<Users> _user;

        public SocialNetworkService(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _user = database.GetCollection<Users>(settings.SocialNetworkCollectionName);
        }

        public List<Users> Get() =>
            _user.Find(user => true).ToList();

        public Users Get(string id) =>
            _user.Find<Users>(user => user.Id == id).FirstOrDefault();

        public Users Create(Users user)
        {
            _user.InsertOne(user);
            return user;
        }

        public void Update(string id, Users userIn) =>
            _user.ReplaceOne(user => user.Id == id, userIn);

        public void Remove(Users userIn) =>
            _user.DeleteOne(user => user.Id == userIn.Id);

        public void Remove(string id) =>
            _user.DeleteOne(user => user.Id == id);
    }

}
