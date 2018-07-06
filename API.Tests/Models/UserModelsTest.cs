using System;
using API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace API.Tests.Models
{
    [TestClass]
    public class UserModelsTest
    {
        string userId;

        [TestInitialize]
        public void Before()
        {
            userId = "a17996c0-1085-411a-a097-b6effb051ef3";
        }

        [TestMethod]
        public void CreateUserTest()
        {
            // Arrange
            User user = new User { Id = userId, Email = "isra@gmail.com", LastName = "perez", Name = "isra", Password = "123456" };
            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual("123456",user.Password);
            Assert.AreEqual("perez", user.LastName);
        }
    }
}
