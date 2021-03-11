using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Vermaat.PowerPlatform.Management.Models;
using Environment = Vermaat.PowerPlatform.Management.Models.Environment;

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

        protected List<Environment> Environments { get; private set; }
        protected List<PowerAutomate> PowerAutomates { get; private set; }

        protected Environment TestEnvironment { get; private set; }
        protected PowerAutomate TestPowerAutomate { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            PreInitialize();

            SetupAssertionEnvironments();
            SetupAssertionPowerAutomates();

            PostInitialize();
        }

        private void SetupAssertionPowerAutomates()
        {
            PowerAutomates = new List<PowerAutomate>();

            TestPowerAutomate = new PowerAutomate()
            {
                DataverseIdentifier = Guid.Parse("{9a45f242-7f82-eb11-a812-000d3a460c81}"),
                DisplayName = "Http PA",
                PowerAppIdentifier = Guid.Parse("{97ed793b-d3c4-e558-7ae6-07305d7b403e}"),
                State = PowerAutomateState.Started
            };
            PowerAutomates.Add(TestPowerAutomate);

            var first = new PowerAutomate()
            {
                DataverseIdentifier = Guid.Parse("{c14d8409-7f82-eb11-a812-000d3a460c81}"),
                DisplayName = "FirstPA",
                PowerAppIdentifier = Guid.Parse("{5ce3f511-0d28-12f5-1ebf-f4906c0bf66e}"),
                State = PowerAutomateState.Started
            };
            PowerAutomates.Add(first);
        }

        private void SetupAssertionEnvironments()
        {
            Environments = new List<Environment>();
            var defaultEnv = new Environment()
            {
                IsDefault = true,
                Name = "Vermaat (default)",
                PowerAppIdentifier = "Default-7aaeb5f4-d3ad-4511-9f5e-d73ced67ed45"
            };
            Environments.Add(defaultEnv);

            var dev = new Environment()
            {
                IsDefault = false,
                Name = "vermaat2-dev",
                DataverseUrl = "https://vermaat2dev.crm4.dynamics.com/",
                PowerAppIdentifier = "30ed1806-fce3-4d29-8670-f2a38a8c2e0b",
                DataverseIdentifier = Guid.Parse("c370e5f1-07af-4aca-a78e-1277ca05431c")
            };
            Environments.Add(dev);

            var clean = new Environment()
            {
                IsDefault = false,
                Name = "Empty",
                DataverseUrl = "https://vermaat2-clean.crm4.dynamics.com/",
                PowerAppIdentifier = "fb626086-0a4b-47de-a3e9-20ef8ba1de76",
                DataverseIdentifier = Guid.Parse("225f39a1-62b0-4355-bd64-6139e79c8dc0")
            };
            Environments.Add(clean);

            TestEnvironment = new Environment()
            {
                IsDefault = false,
                Name = "vermaat2",
                DataverseUrl = "https://vermaat2.crm4.dynamics.com/",
                PowerAppIdentifier = "b38bc7e3-1283-4f88-b1e8-2127fe753048",
                DataverseIdentifier = Guid.Parse("a6ba380d-b048-4353-b1b4-94e9e0471899")
            };
            Environments.Add(TestEnvironment);
        }

        protected virtual void PreInitialize() { }
        protected virtual void PostInitialize() { }
    }
}
