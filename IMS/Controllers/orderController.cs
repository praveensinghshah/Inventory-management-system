using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMS.Models;

namespace IMS.Controllers
{
    public class orderController : Controller
    {
        private readonly InventoryContext _context = new InventoryContext();
        // GET: order
        public ActionResult Index()
        {
            var orders = _context.Orders.Include("Customer").ToList();
            return View(orders);
        }
        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            var order = _context.Orders.Include("OrderDetails.Product").SingleOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(_context.Customers, "CustomerId", "Name");
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                order.CreatedAt = DateTime.Now;
                order.UpdatedAt = DateTime.Now;
                _context.Orders.Add(order);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(_context.Customers, "CustomerId", "Name", order.CustomerId);
            return View(order);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            var order = _context.Orders.SingleOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(_context.Customers, "CustomerId", "Name", order.CustomerId);
            return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                order.UpdatedAt = DateTime.Now;
                _context.Entry(order).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(_context.Customers, "CustomerId", "Name", order.CustomerId);
            return View(order);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            var order = _context.Orders.SingleOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var order = _context.Orders.SingleOrDefault(o => o.OrderId == id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("Index");
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