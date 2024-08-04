using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using IMS.Models;

namespace IMS.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly InventoryContext _context = new InventoryContext();

        // GET: UserRole
        public ActionResult Index()
        {
            var userRoles = _context.UserRoles.Include(ur => ur.User).ToList();
            return View(userRoles);
        }

        // GET: UserRole/Details/5
        public ActionResult Details(int id)
        {
            var userRole = _context.UserRoles.Include(ur => ur.User).SingleOrDefault(ur => ur.UserRoleId == id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // GET: UserRole/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(_context.Users, "UserId", "Username");
            return View();
        }

        // POST: UserRole/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                userRole.CreatedAt = DateTime.Now;
                userRole.UpdatedAt = DateTime.Now;
                _context.UserRoles.Add(userRole);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(_context.Users, "UserId", "Username", userRole.UserId);
            return View(userRole);
        }

        // GET: UserRole/Edit/5
        public ActionResult Edit(int id)
        {
            var userRole = _context.UserRoles.SingleOrDefault(ur => ur.UserRoleId == id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(_context.Users, "UserId", "Username", userRole.UserId);
            return View(userRole);
        }

        // POST: UserRole/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                userRole.UpdatedAt = DateTime.Now;
                _context.Entry(userRole).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(_context.Users, "UserId", "Username", userRole.UserId);
            return View(userRole);
        }

        // GET: UserRole/Delete/5
        public ActionResult Delete(int id)
        {
            var userRole = _context.UserRoles.SingleOrDefault(ur => ur.UserRoleId == id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // POST: UserRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userRole = _context.UserRoles.SingleOrDefault(ur => ur.UserRoleId == id);
            _context.UserRoles.Remove(userRole);
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
