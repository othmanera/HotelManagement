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
    public class HOTELsController : Controller
    {
        private hotelsManagementProjectEntities1 db = new hotelsManagementProjectEntities1();

        // GET: HOTELs
        public ActionResult Index()
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }

            return View(db.HOTELs.ToList());
        }

        //Indivudual hotel page
        public ActionResult Details(int? id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOTEL hOTEL = db.HOTELs.Find(id);
            if (hOTEL == null)
            {
                return HttpNotFound();
            }
            return View(hOTEL);
        }

        // GET: HOTELs/Create
        public ActionResult Create()
        {
            if ((String)Session["Username"] != "Admin")
            {
                return RedirectToAction("Index", "Home");

            }

            return View();
        }

        // POST: HOTELs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idHotel,nomHotel,ville,nbEtoiles,Rating,prix,descriptionHotel")] HOTEL hOTEL)
        {
            if (ModelState.IsValid)
            {
                db.HOTELs.Add(hOTEL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hOTEL);
        }

        // GET: HOTELs/Edit/5
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
            HOTEL hOTEL = db.HOTELs.Find(id);
            if (hOTEL == null)
            {
                return HttpNotFound();
            }
            return View(hOTEL);
        }

        // POST: HOTELs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idHotel,nomHotel,ville,nbEtoiles,Rating,prix,descriptionHotel")] HOTEL hOTEL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOTEL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hOTEL);
        }

        // GET: HOTELs/Delete/5
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
            HOTEL hOTEL = db.HOTELs.Find(id);
            if (hOTEL == null)
            {
                return HttpNotFound();
            }
            return View(hOTEL);
        }

        // POST: HOTELs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HOTEL hOTEL = db.HOTELs.Find(id);
            db.HOTELs.Remove(hOTEL);
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

        public ActionResult List()
        {
            return View(db.HOTELs.ToList());
        }

        //search function
        public ActionResult Search(string q)
        {
            
            var pquery1 = db.HOTELs.Where(elt => elt.ville.Contains(q)).ToList();
            if (pquery1.Count !=0)
            {
                
                return View(pquery1);
            }

            else { 
            var pquery2 = db.HOTELs.Where(elt => elt.nomHotel.Contains(q)).ToList();
            return View(pquery2);
            }

        }

    





    }
}
