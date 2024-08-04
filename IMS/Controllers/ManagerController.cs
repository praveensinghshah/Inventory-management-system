using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace IMS.Controllers
{
    public class ManagerController : Controller
    {
        private readonly InventoryContext _context = new InventoryContext();
        // GET: Manager
        public ActionResult Index()
        {
            var viewModel = new ManagerDashboardViewModel
            {
                RecentTransactions = _context.InventoryTransactions.OrderByDescending(t => t.TransactionDate).Take(10).ToList(),
                SupplierOrders = _context.Orders.Where(o => o.OrderStatus == "Pending").ToList()
            };

            return View(viewModel);
        }
    }
}