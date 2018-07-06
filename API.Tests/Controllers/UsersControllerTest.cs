using API._50.Domain.Core;
using API.Controllers;
using API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace API.Tests.Controllers
{


    [TestClass]
    public class UsersControllerTest
    {

        private UsersController usersController;
        private string userId;

        [TestInitialize]
        public void Before()
        {
            usersController = new UsersController();
            userId = "a18996c0-1085-411a-a097-b6effb051ef3";
        }

        [TestMethod()]
        public void ACreateUserTest()
        {
            // Arrange
            User user = new User {Id= userId, Email = "isra@gmail.com", LastName = "perez", Name = "isra", Password = "123456" };
            // Act
            var result = usersController.PostUser(user);
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void BGetUserTest()
        {
            // Arrange
            var mockRepositoryUser = new Mock<IRepositoryUser>();
            mockRepositoryUser.Setup(sp => sp.FindById(us => us.Name == "isra@gmail.com")).Returns(new User { Id = userId,Email = "isra@gmail.com", LastName = "perez", Name = "isra", Password = "123456" });
            UsersController _usersController = new UsersController(mockRepositoryUser.Object);
            // Act
            IHttpActionResult actionResult = _usersController.GetUser(userId);
            var contentResult = actionResult as OkNegotiatedContentResult<User>;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("isra@gmail.com",contentResult.Content.Email);
        }


        [TestMethod()]
        public void CGetUsersAllTest()
        {
            // Arrange
            UsersController _usersController = new UsersController();
            // Act
            var actionResult = _usersController.GetUsers();
            var contentResult = actionResult as OkNegotiatedContentResult<User>;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(contentResult);
        }
        [TestMethod()]
        public void DPutUsersTest()
        {
            // Arrange
            User user = new User { Id = userId, Email = "isra@gmail.com", LastName = "perez", Name = "isra", Password = "123666" };
            UsersController _usersController = new UsersController();
            // Act
            var actionResult = _usersController.PutUser(userId, user);
            // Assert
            Assert.IsNotNull(actionResult);
        }

        [TestMethod()]
        public void EDeleteUserTest()
        {
            // Act
            var result = usersController.DeleteUser(userId);
            // Assert
            Assert.IsNotNull(result);
        }
    }


      
}
