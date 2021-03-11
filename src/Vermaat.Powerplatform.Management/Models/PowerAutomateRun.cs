using System;

namespace Vermaat.PowerPlatform.Management.Models
{
    public class PowerAutomateRun
    {
        public string Name { get; set; }
        public PowerAutomateRunStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public enum PowerAutomateRunStatus
    {
        Succeeded, Failed, Cancelled
    }
}
