using System;
using System.Linq;
using System.Web.Mvc;
using IMS.Models;

namespace IMS.Controllers
{
   
    public class UserController : Controller
    {
        private readonly InventoryContext _context = new InventoryContext();

        // GET: User
        public ActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                user.UpdatedAt = DateTime.Now;
                _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId == id);
            _context.Users.Remove(user);
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
