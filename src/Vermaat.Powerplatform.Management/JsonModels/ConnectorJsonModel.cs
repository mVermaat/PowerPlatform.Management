using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vermaat.PowerPlatform.Management.Models;

namespace Vermaat.PowerPlatform.Management.JsonModels
{
    class ConnectorsCollectionJsonModel
    {
        [JsonProperty("value")]
        public ConnectorsJsonModel[] Connectors { get; set; }

        public Connector[] ToConnectorCollection()
        {
            return Connectors.Select(e => e.ToConnector()).ToArray();
        }
    }

    class ConnectorsJsonModel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }

        public ConnectorsJsonPropertiesModel Properties { get; set; }

        public Connector ToConnector()
            => new Connector()
            {
                Description = Properties.Description,
                DisplayName = Properties.DisplayName,
                Id = Id,
                IsCustomApi = Properties.IsCustomApi,
                Name = Name,
                Type = Type
            };
    }

    class ConnectorsJsonPropertiesModel
    {
        public string DisplayName { get; set; }
        public bool IsCustomApi { get; set; }
        public string Description { get; set; }
    }
}
