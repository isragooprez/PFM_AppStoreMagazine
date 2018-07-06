using System;
using API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace API.Tests.Controllers
{
    [TestClass]
    public class NsoupControllerTest
    {


        private NsoupController nsoupController;
        private string filter;
        private string url;

        [TestInitialize]
        public void Before()
        {
            //Arrage
            nsoupController = new NsoupController();
            filter = "Kaunas";
            url = "145393,tip=sid,clean=0";
        }

        [TestMethod]
        public void GetFilter()
        {
            //Act
            var result = nsoupController.GetFilter(filter);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Lithuania", result[0].Country);
        }

        [TestMethod]
        public void GetDataMagazine()
        {
            //Act
            var result = nsoupController.GetDataMagazine(url);
            //Assert
            Assert.IsNotNull(result);
        }
    }
}
