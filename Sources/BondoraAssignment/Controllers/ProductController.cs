using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using BondoraAssignment.Models.EntityFramework;
using System.Threading.Tasks;
using BondoraAssignment.Models;
using System.Text;

namespace BondoraAssignment.Controllers
{
    public class ProductController : Controller
    {
        private string currency = "€";

        public async Task<ActionResult> Index(int id)
        {
            Product product = await Task.Run(() => DBRequest.GetProductById(id));
            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        /// <summary>
        /// Return a description on how the price calculation works for a product
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public ActionResult GetPrice(Product p)
        {
            string tmp = CalculatePriceForProduct(p);
            return Content(tmp);
        }

        /// <summary>
        /// Return a description on how the price calculation works for a product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private string CalculatePriceForProduct(Product product)
        {
            StringBuilder str = new StringBuilder("");
            ICollection<FromToCalculation> listOfCalculation = product.Category.FromToCalculations;
            foreach (FromToCalculation rateInfo in listOfCalculation)
            {
                if (!string.IsNullOrEmpty(str.ToString()))
                {
                    str.Append(" and then ");
                }

                if (rateInfo.from == rateInfo.to) //We rent it for the same day
                {
                    if (rateInfo.from == 0) //unique rate
                    {
                        str.Append("Unique paiment of " + rateInfo.Rate.price + currency);
                    }
                }
                else if (rateInfo.to == null)
                {
                    str.Append(rateInfo.Rate.price + currency + " for each day");
                }
                else
                {
                    int? tmp = rateInfo.from + rateInfo.to; //Magic number 1 because we start at day 0
                    str.Append(rateInfo.Rate.price + currency + " for the first " + tmp + " days");
                }
            }
            str.Append('.');
            return str.ToString();
        }

        /// <summary>
        /// Add a product with the number of day into the user's cart
        /// </summary>
        /// <param name="productid"></param>
        /// <param name="daysToRent"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> ConfirmRentObject(string productid, string daysToRent, string userid)
        {
            bool addItemToCartSucceded = await Task.Run(() => DBRequest.AddItemToUserCart(int.Parse(productid), int.Parse(daysToRent), int.Parse(userid)));
            if (addItemToCartSucceded)
            {
                return Json("succedded");
            }
            else
            {
                return Json("error");
            }
        }

    }
}