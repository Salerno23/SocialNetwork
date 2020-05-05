using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SocialNetwork.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public List<string> CreatedCircles { get; set; }

        public List<string> Posts { get; set; }

        public string Blocked { get; set; }
        public string Follows { get; set; }
    }
}

