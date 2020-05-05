using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace SocialNetwork.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        public string PostId { get; set; }
        public string Post_ { get; set; }
        public DateTime Date { get; set; }
        public string ContentType { get; set; }
        public bool IsPublic { get; set; }

        public List<string> Comments { get; set; }
        public List<string> CircleRef { get; set; }
    }
}
