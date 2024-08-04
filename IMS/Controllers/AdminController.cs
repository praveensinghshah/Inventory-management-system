using IMS.Models;
using System.Linq;
using System.Web.Mvc;


namespace IMS.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly InventoryContext _context = new InventoryContext();
        // GET: Admin
        public ActionResult Index()
        {
            var model = new AdminDashboardViewModel
            {
                TotalUsers = _context.Users.Count(),
                TotalProducts = _context.Products.Count(),
                TotalTransactions = _context.InventoryTransactions.Count(),
                TotalRevenue = _context.InventoryTransactions
                              .Where(t => t.TransactionType == "Sale")
                              .Sum(t => (decimal?)(t.Quantity * t.Product.Price)) ?? 0,
                LastTransactionDate = _context.InventoryTransactions
                                        .OrderByDescending(t => t.TransactionDate)
                                        .Select(t => t.TransactionDate)
                                        .FirstOrDefault()
            };

            return View(model);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}