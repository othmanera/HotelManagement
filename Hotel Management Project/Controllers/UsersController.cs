using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Hotel_Management_Project.Models;

namespace Hotel_Management_Project.Controllers
{
    public class UsersController : Controller
    {
        private hotelsManagementProjectEntities1 db = new hotelsManagementProjectEntities1();

        // GET: Users
        
        public ActionResult Index()
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }
            var users = db.Users.Include(u => u.UserRole);
                return View(users.ToList());
            

            }

         
        
        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        
        // GET: Users/Create
        public ActionResult Create()
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }
            ViewBag.RoleId = new SelectList(db.UserRoles, "RoleId", "RoleName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,username,email,passwordUser,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.UserRoles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.UserRoles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,username,email,passwordUser,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.UserRoles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //login 
        public ActionResult Login() {
            if (Session["Username"] !=null)
            {
                return RedirectToAction("Index", "Home");

            }


            return View();

        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            

                if (ModelState.IsValid)
            {
                var LoggedUser = db.Users.FirstOrDefault(u => u.username == user.username && u.passwordUser == user.passwordUser);

                if (LoggedUser != null)
                {
                    //Store user information in session
                    Session["Username"] = user.username;
                    Session["IsAuthenticated"] = true;
                    return RedirectToAction("Index", "Home");
                }
            }

            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View(user);
        }











        //Registration

        public ActionResult Register()
        {
            if (Session["Username"] != null)
            {
                return RedirectToAction("Index", "Home");

            }

            ViewBag.RoleId = new SelectList(db.UserRoles, "RoleId", "RoleName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,username,email,passwordUser,RoleId")] User user)
        {
            if (Request.IsAuthenticated)
            {
                return View("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.RoleId = new SelectList(db.UserRoles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }


        


        
       





    }




}
