using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMS.Models;

namespace IMS.Controllers
{
    public class InventoryTransactionController : Controller
    {
        private readonly InventoryContext _context = new InventoryContext();
        // GET: InventoryTransaction
        public ActionResult Index()
        {
            var transactions = _context.InventoryTransactions.Include("Product").ToList();
            return View(transactions);
        }
        // GET: InventoryTransaction/Details/5
        public ActionResult Details(int id)
        {
            var transaction = _context.InventoryTransactions.Include("Product").SingleOrDefault(t => t.TransactionId == id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: InventoryTransaction/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(_context.Products, "ProductId", "Name");
            return View();
        }

        // POST: InventoryTransaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryTransaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.CreatedAt = DateTime.Now;
                transaction.UpdatedAt = DateTime.Now;
                _context.InventoryTransactions.Add(transaction);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(_context.Products, "ProductId", "Name", transaction.ProductId);
            return View(transaction);
        }

        // GET: InventoryTransaction/Edit/5
        public ActionResult Edit(int id)
        {
            var transaction = _context.InventoryTransactions.SingleOrDefault(t => t.TransactionId == id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(_context.Products, "ProductId", "Name", transaction.ProductId);
            return View(transaction);
        }

        // POST: InventoryTransaction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InventoryTransaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.UpdatedAt = DateTime.Now;
                _context.Entry(transaction).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(_context.Products, "ProductId", "Name", transaction.ProductId);
            return View(transaction);
        }

        // GET: InventoryTransaction/Delete/5
        public ActionResult Delete(int id)
        {
            var transaction = _context.InventoryTransactions.SingleOrDefault(t => t.TransactionId == id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: InventoryTransaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var transaction = _context.InventoryTransactions.SingleOrDefault(t => t.TransactionId == id);
            _context.InventoryTransactions.Remove(transaction);
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