using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace Covid.Controllers.Tests
{
    [TestClass]
    public class ProvincesControllerTest
    {
        [TestMethod]
        public async void Provinces()
        {
            ProvincesController controller = new ProvincesController();

            ViewResult result = await controller.Provinces("US") as ViewResult;

            Assert.IsNotNull(result);

        }
    }
}