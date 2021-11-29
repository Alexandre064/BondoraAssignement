using BondoraAssignment.Controllers;
using BondoraAssignment.Models.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;


namespace BondoraAssignment.Tests.Controllers
{
    /// <summary>
    /// Description résumée pour CartDetailController
    /// </summary>
    [TestClass]
    public class CartDetailControllerTest
    {
        CreateDataHelper dataHelper = new CreateDataHelper();

        #region GetPrice
        [TestMethod]
        public void GetPriceFor7Days()
        {
            CartDetailController controller = new CartDetailController();
            /**
             * test product for 7 days : 
             * 1 payment of 100
             * 3 payment of 60
             * 4 payment of 40
             * total = 440
             */
            CartDetail cart = dataHelper.CreateCartDetail(7);

            var result = controller.GetPrice(cart) as ContentResult;

            Assert.AreEqual("440", result.Content);
        }

        [TestMethod]
        public void GetPriceFor1Day()
        {
            CartDetailController controller = new CartDetailController();
            /**
             * test product for 1 days : 
             * 1 payment of 100
             * 1 payment of 60
             * total = 160
             */
            CartDetail cart = dataHelper.CreateCartDetail(1);

            var result = controller.GetPrice(cart) as ContentResult;

            Assert.AreEqual("160", result.Content);
        }

        [TestMethod]
        public void GetPriceFor3Day()
        {
            CartDetailController controller = new CartDetailController();
            /**
             * test product for 3 days : 
             * 1 payment of 100
             * 3 payment of 60
             * total = 280
             */
            CartDetail cart = dataHelper.CreateCartDetail(3);

            var result = controller.GetPrice(cart) as ContentResult;

            Assert.AreEqual("280", result.Content);
        }

        [TestMethod]
        public void GetPriceFor100Day()
        {
            CartDetailController controller = new CartDetailController();
            /**
             * test product for 100 days : 
             * 1 payment of 100
             * 3 payment of 60
             * 97 payment of 40
             * total = 4160
             */
            CartDetail cart = dataHelper.CreateCartDetail(100);

            var result = controller.GetPrice(cart) as ContentResult;

            Assert.AreEqual("4160", result.Content);
        }
        #endregion

        #region GetTotalPrice
        [TestMethod]
        public void GetTotalPrice10Days2Products()
        {
            CartDetailController controller = new CartDetailController();
            /**
             * We rent the same product twice.
             * Test product for 10 days : 
             * 1 payment of 100
             * 3 payment of 60
             * 7 payment of 40
             * total = 560
             * 
             * With two products that 1120.
             */
            User user = dataHelper.CreateUser(10);
            user.CartDetails.Add(dataHelper.CreateCartDetail(10));
            var result = controller.GetTotalPrice(user) as ContentResult;

            Assert.AreEqual("1120", result.Content);
        }
        [TestMethod]
        public void GetTotalPrice10Days3Products()
        {
            CartDetailController controller = new CartDetailController();
            /**
             * We rent the same product twice.
             * Test product for 10 days : 
             * 1 payment of 100
             * 3 payment of 60
             * 7 payment of 40
             * total = 560
             * 
             * With three products that 1680.
             */
            User user = dataHelper.CreateUser(10);
            user.CartDetails.Add(dataHelper.CreateCartDetail(10));
            user.CartDetails.Add(dataHelper.CreateCartDetail(10));
            var result = controller.GetTotalPrice(user) as ContentResult;

            Assert.AreEqual("1680", result.Content);
        }
        [TestMethod]
        public void GetTotalPrice10Days1Product7Days1Product()
        {
            CartDetailController controller = new CartDetailController();
            /**
             * We rent the same product twice.
             * Test product for 10 days : 
             * 1 payment of 100
             * 3 payment of 60
             * 7 payment of 40
             * total = 560
             * 
             * Test product for 7 days : 
             * 1 payment of 100
             * 3 payment of 60
             * 7 payment of 40
             * total = 440
             * 
             */
            User user = dataHelper.CreateUser(10);
            user.CartDetails.Add(dataHelper.CreateCartDetail(7));
            var result = controller.GetTotalPrice(user) as ContentResult;

            Assert.AreEqual("1000", result.Content);
        }
        #endregion
    }
}
