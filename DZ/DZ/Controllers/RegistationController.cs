using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DZ.Models;

namespace DZ.Controllers
{
    public class RegistationController : Controller
    {
        private Web_DZEntities db = new Web_DZEntities();

        // GET: Registation
        public ActionResult Index()
        {
            var registations = db.registations.Include(r => r.batch).Include(r => r.course);
            return View(registations.ToList());
        }

        // GET: Registation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registation registation = db.registations.Find(id);
            if (registation == null)
            {
                return HttpNotFound();
            }
            return View(registation);
        }

        // GET: Registation/Create
        public ActionResult Create()
        {
            ViewBag.id_batch = new SelectList(db.batches, "id_batch", "batch1");
            ViewBag.id_course = new SelectList(db.courses, "id_course", "course1");
            return View();
        }

        // POST: Registation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,firstname,lastname,nic,id_course,id_batch,telno")] registation registation)
        {
            if (ModelState.IsValid)
            {
                db.registations.Add(registation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_batch = new SelectList(db.batches, "id_batch", "batch1", registation.id_batch);
            ViewBag.id_course = new SelectList(db.courses, "id_course", "course1", registation.id_course);
            return View(registation);
        }

        // GET: Registation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registation registation = db.registations.Find(id);
            if (registation == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_batch = new SelectList(db.batches, "id_batch", "batch1", registation.id_batch);
            ViewBag.id_course = new SelectList(db.courses, "id_course", "course1", registation.id_course);
            return View(registation);
        }

        // POST: Registation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,firstname,lastname,nic,id_course,id_batch,telno")] registation registation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_batch = new SelectList(db.batches, "id_batch", "batch1", registation.id_batch);
            ViewBag.id_course = new SelectList(db.courses, "id_course", "course1", registation.id_course);
            return View(registation);
        }

        // GET: Registation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registation registation = db.registations.Find(id);
            if (registation == null)
            {
                return HttpNotFound();
            }
            return View(registation);
        }

        // POST: Registation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            registation registation = db.registations.Find(id);
            db.registations.Remove(registation);
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
