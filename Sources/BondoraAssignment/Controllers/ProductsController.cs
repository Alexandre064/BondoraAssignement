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
    public class ProductsController : Controller
    {
        public async Task<ActionResult> Index()
        {
            IEnumerable<Product> products = await Task.Run(() => DBRequest.GetAllProducts());
            return View(products.OrderBy(s => s.id).ToPagedList(1,products.Count()));
        }
    }
}
