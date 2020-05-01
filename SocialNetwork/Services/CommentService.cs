using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SocialNetwork.Interfaces;
using SocialNetwork.Models;

namespace SocialNetwork.Services
{
    public class CommentService
    {
        private readonly IMongoCollection<Comment> _comment;

        public CommentService(ISocialNetworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _comment = database.GetCollection<Comment>(settings.CommentCollectionName);
        }

        public List<Comment> Get() =>
            _comment.Find(comment => true).ToList();

        public Comment Get(string id) =>
            _comment.Find<Comment>(comment => comment.Id == id).FirstOrDefault();

        public Comment Create(Comment comment)
        {
            _comment.InsertOne(comment);
            return comment;
        }

        public void Update(string id, Comment commentIn) =>
            _comment.ReplaceOne(comment => comment.Id == id, commentIn);

        public void Remove(Comment commentIn) =>
            _comment.DeleteOne(comment => comment.Id == commentIn.Id);

        public void Remove(string id) =>
            _comment.DeleteOne(comment => comment.Id == id);
    }
}
