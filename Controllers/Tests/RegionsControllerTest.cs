using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace Covid.Controllers.Tests
{
    [TestClass]
    public class RegionsControllerTest
    {
        [TestMethod]
        public async void Regions()
        {
            RegionsController controller = new RegionsController();

            ViewResult result = await controller.Regions() as ViewResult;

            Assert.IsNotNull(result);

        }
    }
}