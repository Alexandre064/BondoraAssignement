using BondoraAssignment.Controllers;
using BondoraAssignment.Models.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;

namespace BondoraAssignment.Tests.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
        CreateDataHelper dataHelper = new CreateDataHelper();
        [TestMethod]
        public void GetPrice()
        {
            ProductController controller = new ProductController();

            CartDetail cart = dataHelper.CreateCartDetail(7);

            var result = controller.GetPrice(cart.Product) as ContentResult;

            Assert.AreEqual("Unique paiment of 100€ and then 60€ for the first 4 days and then 40€ for each day.", result.Content);
        }
    }
}
