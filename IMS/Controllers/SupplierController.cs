using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMS.Models;

namespace IMS.Controllers
{
    public class SupplierController : Controller
    {
        private readonly InventoryContext _context = new InventoryContext();
        // GET: Supplier
        public ActionResult Index()
        {
            var suppliers = _context.Suppliers.ToList();
            return View(suppliers);
        }
        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            var supplier = _context.Suppliers.SingleOrDefault(s => s.SupplierId == id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.CreatedAt = DateTime.Now;
                supplier.UpdatedAt = DateTime.Now;
                _context.Suppliers.Add(supplier);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            var supplier = _context.Suppliers.SingleOrDefault(s => s.SupplierId == id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.UpdatedAt = DateTime.Now;
                _context.Entry(supplier).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            var supplier = _context.Suppliers.SingleOrDefault(s => s.SupplierId == id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var supplier = _context.Suppliers.SingleOrDefault(s => s.SupplierId == id);
            _context.Suppliers.Remove(supplier);
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