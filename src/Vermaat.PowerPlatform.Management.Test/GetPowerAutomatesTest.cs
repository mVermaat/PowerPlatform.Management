using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vermaat.PowerPlatform.Management.Test
{
    [TestClass]
    public class GetPowerAutomatesTest : BaseTest
    {
        [TestMethod]
        public async Task TestGetAdminEnvironments()
        {
            var manager = new AdminPowerPlatformManager(new UsernamePasswordTokenManager(EndpointInfo.Prod, Username, Password));
            var environment = (await manager.GetEnvironments()).FirstOrDefault(e => e.Name.Equals("vermaat2-dev", StringComparison.OrdinalIgnoreCase));

            var flows = await manager.GetPowerAutomates(environment);


        }
    }
}
