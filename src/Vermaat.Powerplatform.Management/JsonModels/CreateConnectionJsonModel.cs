using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vermaat.PowerPlatform.Management.Models;
using Environment = Vermaat.PowerPlatform.Management.Models.Environment;

namespace Vermaat.PowerPlatform.Management.JsonModels
{
    class CreateConnectionJsonModel
    {
        public CreateConnectionJsonModel(Environment environment)
        {
            Properties = new CreateConnectionJsonPropertiesModel
            {
                ConnectionParameters = new CreateConnectionJsonParametersModel
                {
                    
                },
                Environment = new CreateConnectionJsonEnvironmentModel
                {
                    Id = $"/providers/Microsoft.PowerApps/environments/{environment.PowerAppIdentifier}",
                    Name = environment.PowerAppIdentifier
                }
            };
        }

        public CreateConnectionJsonPropertiesModel Properties { get; set; }
    }

    class CreateConnectionJsonPropertiesModel
    {
        public CreateConnectionJsonEnvironmentModel Environment { get; set; }
        public CreateConnectionJsonParametersModel ConnectionParameters { get; set; }
    }

    class CreateConnectionJsonEnvironmentModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    class CreateConnectionJsonParametersModel
    {
        [JsonProperty("token:grantType")]
        public string GrantType { get; set; }
    }
}
