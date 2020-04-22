using ESCHENet.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESCHENet.Tests.Configuration
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void ExecuteConfigurationInjectionTest()
        {
            /*
            *
            * Exemplo: Acessar arquivo de configuração
            *
            */


            var Configuration = SettingsInjection.Configuration;

            var text = Configuration["Test"];

            Assert.IsTrue(text == "Test value");
        }
    }
}
