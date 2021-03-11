using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vermaat.PowerPlatform.Management.Models;

namespace Vermaat.PowerPlatform.Management.JsonModels
{
    internal class PowerAutomateRunCollectionJsonModel
    {
        [JsonProperty("value")]
        public PowerAutomateRunJsonModel[] PowerAutomates { get; set; }

        public PowerAutomateRun[] ToPowerAutomateCollection()
        {
            return PowerAutomates.Select(e => e.ToPowerAutomateRun()).ToArray();
        }
    }

    internal class PowerAutomateRunJsonModel
    {
        public string Name { get; set; }
        public PowerAutomateRunPropertyJsonModel Properties { get; set; }

        public PowerAutomateRun ToPowerAutomateRun()
            => new PowerAutomateRun()
            {
                EndTime = Properties.EndTime,
                Name = Name,
                StartTime = Properties.StartTime,
                Status = (PowerAutomateRunStatus)Enum.Parse(typeof(PowerAutomateRunStatus), Properties.Status)
            };
    }

    internal class PowerAutomateRunPropertyJsonModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
    }
}
