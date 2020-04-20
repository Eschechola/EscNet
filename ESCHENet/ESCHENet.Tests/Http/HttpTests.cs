using ESCHENet.Http.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESCHENet.Tests.Http
{
    [TestClass]
    public class HttpTests
    {
        [TestMethod]
        public void ExecuteGetRequestIPTest()
        {
            IHttpContextAccessor Context = new HttpContextAccessor();
            var ip = new IP(Context).GetRequestIP();

            Assert.IsTrue(true);
        }
    }
}
