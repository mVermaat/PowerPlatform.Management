using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vermaat.PowerPlatform.Management.Models;

namespace Vermaat.PowerPlatform.Management.Test
{
    [TestClass]
    public class ConnectionsTest : BaseTest
    {
        [TestMethod]
        public async Task TestGetConnections()
        {
            var manager = new AdminPowerPlatformManager(new UsernamePasswordTokenManager(EndpointInfo.Prod, Username, Password));

        }
    }
}
