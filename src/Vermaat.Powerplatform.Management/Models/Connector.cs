﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vermaat.PowerPlatform.Management.Models
{
    public class Connector
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool IsCustomApi { get; set; }
    }
}
