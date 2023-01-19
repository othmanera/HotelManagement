using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hotel_Management_Project.Models;

namespace Hotel_Management_Project.Controllers
{
    public class ReviewsController : Controller
    {
        private hotelsManagementProjectEntities1 db = new hotelsManagementProjectEntities1();

        // GET: Reviews
        public ActionResult Index()
        {

            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }

            var reviews = db.Reviews.Include(r => r.HOTEL).Include(r => r.User);
            return View(reviews.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }

            ViewBag.IdHotel = new SelectList(db.HOTELs, "idHotel", "nomHotel");
            ViewBag.IdUser = new SelectList(db.Users, "Id", "username");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdReview,TitleReview,DescReview,IdHotel,IdUser")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdHotel = new SelectList(db.HOTELs, "idHotel", "nomHotel", review.IdHotel);
            ViewBag.IdUser = new SelectList(db.Users, "Id", "username", review.IdUser);
            return View(review);
        }

        // GET: Reviews/Edit/5
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
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdHotel = new SelectList(db.HOTELs, "idHotel", "nomHotel", review.IdHotel);
            ViewBag.IdUser = new SelectList(db.Users, "Id", "username", review.IdUser);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdReview,TitleReview,DescReview,IdHotel,IdUser")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdHotel = new SelectList(db.HOTELs, "idHotel", "nomHotel", review.IdHotel);
            ViewBag.IdUser = new SelectList(db.Users, "Id", "username", review.IdUser);
            return View(review);
        }

        // GET: Reviews/Delete/5
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
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
    }
}
