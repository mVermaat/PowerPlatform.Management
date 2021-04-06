using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vermaat.PowerPlatform.Management.Models;

namespace Vermaat.PowerPlatform.Management.JsonModels
{
    class ConnectionsCollectionJsonModel
    {
        [JsonProperty("value")]
        public ConnectionsJsonModel[] Connections { get; set; }

        public Connection[] ToConnectionCollection()
        {
            return Connections.Select(e => e.ToConnection()).ToArray();
        }
    }

    class ConnectionsJsonModel
    {
        public string Name { get; set; }

        public ConnectionsJsonPropertiesModel Properties { get; set; }

        public Connection ToConnection()
            => new Connection()
            {
                AccountName = Properties.AccountName,
                DisplayName = Properties.DisplayName,
                Id = Name,
                Type = !string.IsNullOrEmpty(Properties.ApiId) ? Properties.ApiId.Split('/').Last() : null
            };

    }

    class ConnectionsJsonPropertiesModel
    {
        public string ApiId { get; set; }
        public string DisplayName { get; set; }
        public string AccountName { get; set; }
    }
}
