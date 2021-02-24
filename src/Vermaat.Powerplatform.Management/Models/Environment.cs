using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vermaat.PowerPlatform.Management.Models
{
    public class Environment
    {
        public string PowerAppIdentifier { get; set; }
        public Guid? DataverseIdentifier { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public string DataverseUrl { get; set; }
    }
}
