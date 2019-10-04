using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExapleWebApp.Models;
using Microsoft.AspNet.Identity;

namespace ExapleWebApp.Controllers
{
    [Authorize]
    public class CatsController : Controller
    {
        private Entities db = new Entities();

        // GET: Cats
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            return View(db.Cat.Where(x=>x.IdUser==userId).ToList());
        }

        // GET: Cats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            Cat cat = db.Cat.FirstOrDefault(x=> x.Id == id && x.IdUser == userId);
            if (cat == null)
            {
                return HttpNotFound();
            }
            return View(cat);
        }

        // GET: Cats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,BirthDate")] Cat cat)
        {
            cat.IdUser = User.Identity.GetUserId();
            if (db.Cat.Any(x=>x.Name == cat.Name))
            {
                ModelState.AddModelError("Name", "That name already exists");
            }
            if (ModelState.IsValid)
            {
                    db.Cat.Add(cat);
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }

            return View(cat);
        }

        // GET: Cats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            Cat cat = db.Cat.FirstOrDefault(x => x.Id == id && x.IdUser == userId);
            if (cat == null)
            {
                return HttpNotFound();
            }
            return View(cat);
        }

        // POST: Cats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,BirthDate")] Cat cat)
        {
            var userId = User.Identity.GetUserId();
            Cat editCat = db.Cat.FirstOrDefault(x => x.Id == cat.Id && x.IdUser == userId);
            if (ModelState.IsValid  && editCat != null)
            {
                if (db.Cat.Any(x => x.Name == cat.Name && cat.Name!=editCat.Name))
                {
                    ModelState.AddModelError("Name", "That name already exists");
                    return View(cat);
                }
                editCat.Name = cat.Name;
                editCat.BirthDate = cat.BirthDate;
                db.Entry(editCat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cat);
        }

        // GET: Cats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = User.Identity.GetUserId();
            Cat cat = db.Cat.FirstOrDefault(x => x.Id == id && x.IdUser == userId);
            if (cat == null)
            {
                return HttpNotFound();
            }
            return View(cat);
        }

        // POST: Cats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userId = User.Identity.GetUserId();
            Cat cat = db.Cat.FirstOrDefault(x => x.Id == id && x.IdUser == userId);
            db.Cat.Remove(cat);
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
