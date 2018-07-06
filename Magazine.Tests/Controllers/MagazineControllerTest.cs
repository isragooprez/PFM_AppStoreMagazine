using Magazine.Controllers;
using Magazine.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Magazine.Tests.Controllers
{
    [TestClass]
    public class MagazineControllerTest
    {

        private MagazinesVirtualModels _magazineStoreVirtualModel;

        [TestInitialize]
        public void Before()
        {
            _magazineStoreVirtualModel = new MagazinesVirtualModels();
        }
    



        [TestMethod]
        public void Create()
        {
            // Arrange
            MagazineController controller = new MagazineController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            MagazineController controller = new MagazineController();

            // Act
            ViewResult result = controller.Delete() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            // Arrange
            MagazineController controller = new MagazineController();

            // Act
            ViewResult result = controller.Details() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit()
        {
            // Arrange
            MagazineController controller = new MagazineController();

            // Act
            ViewResult result = controller.Edit() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
