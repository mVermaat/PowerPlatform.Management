using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vermaat.PowerPlatform.Management.Test
{
    public class BaseTest
    {
        public TestContext TestContext { get; set; }

        protected Guid TenantId => Guid.Parse((string)TestContext.Properties["TenantId"]);
        protected string ClientId => (string)TestContext.Properties["ClientId"];
        protected string ClientSecret => (string)TestContext.Properties["ClientSecret"];
        protected string Username => (string)TestContext.Properties["Username"];
        protected string Password => (string)TestContext.Properties["Password"];

        [TestInitialize]
        public void Initialize()
        {
            PreInitialize();
            PostInitialize();
        }

        protected virtual void PreInitialize() { }
        protected virtual void PostInitialize() { }
    }
}
