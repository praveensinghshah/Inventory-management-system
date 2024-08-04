using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using IMS.Models;

namespace IMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly InventoryContext _context = new InventoryContext();

        public ActionResult Login()
        {
            ViewBag.Roles = new List<SelectListItem>
            {
                new SelectListItem { Text = "Admin", Value = "Admin" },
                new SelectListItem { Text = "Manager", Value = "Manager" },
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginviewModel model)
        {
            ViewBag.Roles = new List<SelectListItem>
            {
                new SelectListItem { Text = "Admin", Value = "Admin" },
                new SelectListItem { Text = "Manager", Value = "Manager" },
            };

            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(u => u.Username == model.Username);

                if (user != null && VerifyPassword(user.PasswordHash, model.Password))
                {
                    if (user.Role == model.Role)
                    {
                        Session["Username"] = user.Username;
                        Session["Role"] = user.Role;

                        if (model.Role == "Admin")
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (model.Role == "Manager")
                        {
                            return RedirectToAction("Index", "Manager");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect role selected.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        private bool VerifyPassword(string storedHash, string enteredPassword)
        {
            return storedHash == enteredPassword;
        }
    }
}
