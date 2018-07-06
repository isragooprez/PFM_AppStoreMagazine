using API._30.DistributedServices;
using API.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests.Models
{
    [TestClass]
    public class MagazineModelTest
    {

        private Magazine magazine;
        private string filter;
        private string url;
        string userId;

        [TestInitialize]
        public void Before()
        {
            userId = "a17996c0-1085-411a-a097-b6effb051ef3";
            filter = "Kaunas";
            url = "145393,tip=sid,clean=0";
        }
        [TestMethod]
        public void CreateMagazine()
        {
            //Arrage
            NsoupServices nsoupServices = new NsoupServices();
            //Act
            Magazine mgzn = nsoupServices.GedDataMagazine(url);
            mgzn.DateIn = DateTime.Now;
            mgzn.UserId = userId;
            //Assert
            Assert.IsNotNull(mgzn);
            Assert.IsNotNull(mgzn.Name);
            Assert.AreEqual("Mechanika", mgzn.Name);
            Assert.AreEqual("13921207", mgzn.ISSN);
            
        }

    }
}
