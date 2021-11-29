using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BondoraAssignment.Models;
using BondoraAssignment.Models.EntityFramework;

namespace BondoraAssignment.Controllers
{
    public class CartDetailController : Controller
    {
        // GET: CartDetail
        public async Task<ActionResult> Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await Task.Run(() => DBRequest.GetUserById(id));
            return View(user);
        }

        
        public ActionResult GetPrice(CartDetail cartDetail)
        {
            string tmp = CalculatorHelper.CalculatePriceOfProductWithDay(cartDetail.number_of_day, cartDetail.Product).ToString();

            return Content(tmp);
        }


        public ActionResult GetTotalPrice(User user)
        {
            return Content(CalculatorHelper.CalculateTotalPrice(user).ToString());
        }

        [HttpPost]
        public async Task<JsonResult> RemoveObjectFromCart(int productidincart, int userId)
        {
            bool removeItemFromCartSuccedded = await Task.Run(() => DBRequest.RemoveItemFromUserCart(productidincart, userId));
            if (removeItemFromCartSuccedded)
            {
                return Json("succedded");
            }
            else
            {
                return Json("error");
            }
        }
        [HttpPost]
        public async Task<JsonResult> ConfirmCommand(int user)
        {
            User userModel = await Task.Run(() => DBRequest.GetUserById(user));
            bool commandPassed = await Task.Run(() => DBRequest.ConfirmCommandForUser(userModel.id, CalculatorHelper.CalculateTotalPrice(userModel), CalculatorHelper.CalculateTotalFidelityPoint(userModel)));

            if (commandPassed)
            {
                DBRequest.AddFidelityPoint(userModel, CalculatorHelper.CalculateTotalFidelityPoint(userModel));
                DBRequest.RemoveAllItemFromUserCart(userModel);
                return Json("succedded");
            }
            else
            {

                return Json("error");
            }
        }
    }
}
