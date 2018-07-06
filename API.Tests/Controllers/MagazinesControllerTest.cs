using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using API.Models;
using System.Web.Http;
using API._30.DistributedServices;
using System.Configuration;
using System.Web.Http.Results;
using Moq;
using API._50.Domain.Core;

namespace API.Tests.Controllers
{
    [TestClass()]
    public class MagazinesControllerTest
    {
        private MagazinesController magazinesController;
        private int code_mgz;
        string userId;
        string url;
        [TestInitialize]
        public void Before()
        {
            magazinesController = new MagazinesController();
            code_mgz = 1;
            userId = "a17996c0-1085-411a-a097-b6effb051ef3";
            url = "19309,tip=sid,clean=0";
        }

        [TestMethod()]
        public void CreateMagazineTest()
        {
            // Arrange
            NsoupServices nsoupServices = new NsoupServices();
            //Act
            var mgzn = nsoupServices.GedDataMagazine(url);
            mgzn.DateIn = DateTime.Now;
            mgzn.UserId = userId;
            var result = magazinesController.PostMagazine(mgzn);
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void GetMagazinesTest()
        {
            // Arrange
            IQueryable<Magazine> result = magazinesController.GetMagazines();

            // Assert
            Assert.IsNotNull(magazinesController.GetMagazines().Where(m => m.Id >= 0));
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void GetMagazinesByUrlTest()
        {
            // Arrange
            var mockRepositoryMagazine = new Mock<IRepositoryMagazine>();
            mockRepositoryMagazine.Setup(sp => sp.FindById(mg => mg.Url == url)).Returns(new Magazine { Id = code_mgz, Name = "Magazine1" });

            MagazinesController _magazinesController = new MagazinesController(mockRepositoryMagazine.Object);

            //Act
            IHttpActionResult actionResult = _magazinesController.GetMagazinesByUrl(url, userId);
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Magazine>>;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(contentResult);

        }

        [TestMethod()]
        public void GetMagazineTest()
        {

            IHttpActionResult result = magazinesController.GetMagazine(code_mgz);
            var s = result.ToString();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetMagazinesByIdUserTest()
        {

            var result = magazinesController.GetMagazinesByIdUser(userId);
            Assert.IsNotNull(magazinesController.GetMagazines().Where(m => m.UserId == userId));
            Assert.IsNotNull(result);
        }


        [TestMethod()]
        public void DeleteMagazineTest()
        {
            // Arrange
            Magazine magazine = new Magazine();
            var mockRepositoryMagazine = new Mock<IRepositoryMagazine>();
            mockRepositoryMagazine.Setup(sp => sp.Delete(magazine)).Returns(new Magazine { Id = code_mgz, Name = "Magazine1" });

            MagazinesController _magazinesController = new MagazinesController(mockRepositoryMagazine.Object);

            //Act
            IHttpActionResult actionResult = _magazinesController.DeleteMagazine(71);
            var contentResult = actionResult as OkNegotiatedContentResult<Magazine>;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(contentResult);
        }

        [TestMethod()]
        public void PutMagazineTest()
        {
            // Arrange
            Magazine magazine = new Magazine();
            var mockRepositoryMagazine = new Mock<IRepositoryMagazine>();
            mockRepositoryMagazine.Setup(sp => sp.FindById(mg => mg.Url == url)).Returns(new Magazine { Id = code_mgz, Name = "Magazine1" });

            MagazinesController _magazinesController = new MagazinesController(mockRepositoryMagazine.Object);

            //Act
            IHttpActionResult actionResult = _magazinesController.PutMagazine(code_mgz, magazine);
            var contentResult = actionResult as OkNegotiatedContentResult<Magazine>;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(contentResult);
        }
    }
}
