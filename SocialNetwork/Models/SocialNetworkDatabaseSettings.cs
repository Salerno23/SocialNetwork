using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Interfaces;

namespace SocialNetwork.Models
{
    public class SocialNetworkDatabaseSettings : ISocialNetworkDatabaseSettings
    {
        public string UserCollectionName { get; set; }
        public string BlockedCollectionName { get; set; }
        public string CircleCollectionName { get; set; }
        public string CommentCollectionName { get; set; }
        public string PostCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
