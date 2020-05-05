using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SocialNetwork.Interfaces;
using SocialNetwork.Models;

namespace SocialNetwork.Services
{
    public class CircleService
    {
        private readonly IMongoCollection<Circle> _circle;

        public CircleService(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _circle = database.GetCollection<Circle>(settings.CircleCollectionName);
        }

        public List<Circle> Get() =>
            _circle.Find(circle => true).ToList();

        public Circle Get(string id) =>
            _circle.Find<Circle>(circle => circle.Id == id).FirstOrDefault();

        public Circle GetForCircleId(string circleId) =>
            _circle.Find<Circle>(circle => circle.CircleId == circleId).FirstOrDefault();

        public Circle Create(Circle circle)
        {
            _circle.InsertOne(circle);
            return circle;
        }

        public void Update(string id, Circle circleIn) =>
            _circle.ReplaceOne(circle => circle.Id == id, circleIn);

        public void Remove(Circle circleIn) =>
            _circle.DeleteOne(circle => circle.Id == circleIn.Id);

        public void Remove(string id) =>
            _circle.DeleteOne(circle => circle.Id == id);
    }
}

