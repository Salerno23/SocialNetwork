using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.Interfaces;

namespace SocialNetwork.Models
{
    public class SocialNetworkDatabaseSettings : ISocialNetworkDatabaseSettings
    {
        public string SocialNetworkCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}
