using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BondoraAssignment.Models.EntityFramework;

namespace BondoraAssignment.Controllers
{
    public class OrdersController : Controller
    {
        private BondoraAssignementDBEntities db = new BondoraAssignementDBEntities();

        // GET: Orders
        public async Task<ActionResult> Index(int? id)
        {
            var orders = db.Orders.Include(o => o.Payment);
            return View(await orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
    }
}
