using System;

namespace Vermaat.PowerPlatform.Management.Models
{
    public class PowerAutomate
    {
        public Guid PowerAppIdentifier { get; set; }
        public Guid DataverseIdentifier { get; set; }
        public string DisplayName { get; set; }
        public PowerAutomateState State { get; set; }

    }

    public enum PowerAutomateState
    {
        Started, Stopped
    }

}
