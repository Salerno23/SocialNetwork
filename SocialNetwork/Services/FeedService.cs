using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SocialNetwork.Interfaces;
using SocialNetwork.Models;

namespace SocialNetwork.Services
{
    public class FeedService
    {
        private readonly IMongoCollection<Feed> _feed;

        public FeedService(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _feed = database.GetCollection<Feed>(settings.FeedCollectionName);
        }

        public List<Feed> Get() =>
            _feed.Find(feed => true).ToList();

        public Feed Get(string id) =>
            _feed.Find<Feed>(feed => feed.Id == id).FirstOrDefault();

        public List<Feed> GetForUser(string userId) =>
            _feed.Find<Feed>(feed => feed.UserId == userId).ToList();

        public Feed Create(Feed feed)
        {
            _feed.InsertOne(feed);
            return feed;
        }

        public void Update(string id, Feed feedIn) =>
            _feed.ReplaceOne(feed => feed.Id == id, feedIn);

        public void Remove(Feed feedIn) =>
            _feed.DeleteOne(feed => feed.Id == feedIn.Id);

        public void Remove(string id) =>
            _feed.DeleteOne(feed => feed.Id == id);
    }
}
