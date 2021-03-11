using Newtonsoft.Json;
using System;
using System.Linq;
using Vermaat.PowerPlatform.Management.Models;

namespace Vermaat.PowerPlatform.Management.JsonModels
{
    internal class PowerAutomateCollectionJsonModel
    {
        [JsonProperty("value")]
        public PowerAutomateJsonModel[] PowerAutomates { get; set; }

        public PowerAutomate[] ToPowerAutomateCollection()
        {
            return PowerAutomates.Select(e => e.ToPowerAutomate()).ToArray();
        }
    }

    internal class PowerAutomateJsonModel
    {
        public Guid Name { get; set; }

        public PowerAutomateJsonModelProperty Properties { get; set; }

        public PowerAutomate ToPowerAutomate()
            => new PowerAutomate()
            {
                DataverseIdentifier = Properties.WorkflowEntityId,
                DisplayName = Properties.DisplayName,
                PowerAppIdentifier = Name,
                State = (PowerAutomateState)Enum.Parse(typeof(PowerAutomateState), Properties.State)
            };
    }

    internal class PowerAutomateJsonModelProperty
    {
        public string DisplayName { get; set; }
        public string State { get; set; }
        public Guid WorkflowEntityId { get; set; }
    }
}
