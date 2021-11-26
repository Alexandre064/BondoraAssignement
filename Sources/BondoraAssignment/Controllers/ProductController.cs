using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using BondoraAssignment.Models.EntityFramework;

namespace BondoraAssignment.Controllers
{
    public class ProductController : Controller
    {
        private readonly BondoraAssignementDBEntities db = new BondoraAssignementDBEntities();

        // GET: Product
        public ActionResult Index()
        {
            IEnumerable<Product> products = from p in db.Products select p;
            return View(products.OrderBy(s => s.id).ToPagedList(1,products.Count()));
        }
    }
}
