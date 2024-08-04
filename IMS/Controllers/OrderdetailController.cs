using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMS.Models;

namespace IMS.Controllers
{
    public class OrderdetailController : Controller
    {
        private readonly InventoryContext _context = new InventoryContext();
        // GET: Orderdetail
        public ActionResult Index()
        {
            var orderDetails = _context.OrderDetails.Include("Order").Include("Product").ToList();
            return View(orderDetails);
        }
        // GET: OrderDetail/Details/5
        public ActionResult Details(int id)
        {
            var orderDetail = _context.OrderDetails.Include("Order").Include("Product").SingleOrDefault(od => od.OrderDetailId == id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: OrderDetail/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(_context.Orders, "OrderId", "OrderDate");
            ViewBag.ProductId = new SelectList(_context.Products, "ProductId", "Name");
            return View();
        }

        // POST: OrderDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                orderDetail.CreatedAt = DateTime.Now;
                orderDetail.UpdatedAt = DateTime.Now;
                _context.OrderDetails.Add(orderDetail);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(_context.Orders, "OrderId", "OrderDate", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(_context.Products, "ProductId", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetail/Edit/5
        public ActionResult Edit(int id)
        {
            var orderDetail = _context.OrderDetails.SingleOrDefault(od => od.OrderDetailId == id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(_context.Orders, "OrderId", "OrderDate", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(_context.Products, "ProductId", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // POST: OrderDetail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                orderDetail.UpdatedAt = DateTime.Now;
                _context.Entry(orderDetail).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(_context.Orders, "OrderId", "OrderDate", orderDetail.OrderId);
            ViewBag.ProductId = new SelectList(_context.Products, "ProductId", "Name", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetail/Delete/5
        public ActionResult Delete(int id)
        {
            var orderDetail = _context.OrderDetails.SingleOrDefault(od => od.OrderDetailId == id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: OrderDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var orderDetail = _context.OrderDetails.SingleOrDefault(od => od.OrderDetailId == id);
            _context.OrderDetails.Remove(orderDetail);
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