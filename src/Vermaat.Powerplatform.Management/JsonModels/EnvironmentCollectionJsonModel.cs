using Newtonsoft.Json;
using System;
using System.Linq;

namespace Vermaat.PowerPlatform.Management.JsonModels
{
    internal class EnvironmentCollectionJsonModel
    {
        [JsonProperty("value")]
        public EnvironmentJsonModel[] Environments { get; set; }

        public Models.Environment[] ToEnvironmentCollection()
        {
            return Environments.Select(e => e.ToEnvironment()).ToArray();
        }
    }

    internal class EnvironmentJsonModel
    {
        public string Name { get; set; }
        public EnvironmentPropertiesJsonModel Properties { get; set; }

        internal Models.Environment ToEnvironment()
        {
            var result = new Models.Environment
            {
                PowerAppIdentifier = Name,
                IsDefault = Properties.IsDefault,
                Name = Properties.DisplayName
            };

            if (!result.IsDefault)
            {
                result.DataverseIdentifier = Properties.LinkedEnvironmentMetadata.ResourceId;
                result.DataverseUrl = Properties.LinkedEnvironmentMetadata.InstanceUrl;
                result.Name = Properties.LinkedEnvironmentMetadata.FriendlyName;
            }

            return result;
        }
    }

    internal class EnvironmentPropertiesJsonModel
    {
        public string DisplayName { get; set; }
        public bool IsDefault { get; set; }
        public EnvironmentMetadataJsonModel LinkedEnvironmentMetadata { get; set; }
    }

    internal class EnvironmentMetadataJsonModel
    {
        public Guid ResourceId { get; set; }
        public string InstanceUrl { get; set; }
        public string FriendlyName { get; set; }

    }
}
