﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vermaat.PowerPlatform.Management.Test
{
    [TestClass]
    public class EnvironmentTests : BaseTest
    {
        [TestMethod]
        public void TestGetAdminEnvironments()
        {
            var manager = new PowerPlatformManager(new UsernamePasswordTokenManager(EndpointInfo.Prod, Username, Password));
            var environments = manager.GetAdminEnvironments().Result;

            environments.Should().NotBeNullOrEmpty();
        }
    }
}