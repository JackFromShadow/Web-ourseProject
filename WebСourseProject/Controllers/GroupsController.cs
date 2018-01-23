using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebСourseProject.Models;

namespace WebСourseProject.Controllers
{
    public class GroupsController : Controller
    {
        private SiteContext db = new SiteContext();

        // GET: Groups
        public ActionResult Index()
        {
            var Groups = db.Groups.Include(d => d.Teacher).
                Where(d => d.number != 91).OrderBy(d => d.number);
            return View(Groups.ToList());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group @class = db.Groups.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            var stands = db.Teachers.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.LastName + " " + s.FirstName + " " + s.FathersName
            });
            ViewBag.TeacherId = new SelectList(stands, "Value", "Text");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,number,letter,profile,TeacherId,photoPath")] Group @class)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(@class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var stands = db.Teachers.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.LastName + " " + s.FirstName + " " + s.FathersName
            });
            ViewBag.TeacherId = new SelectList(stands, "Value", "Text", @class.TeacherId);
            return View(@class);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group @class = db.Groups.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            var stands = db.Teachers.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.LastName + " " + s.FirstName + " " + s.FathersName
            });
            ViewBag.TeacherId = new SelectList(stands, "Value", "Text", @class.TeacherId);
            return View(@class);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,number,letter,profile,TeacherId,photoPath")] Group @class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var stands = db.Teachers.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.LastName + " " + s.FirstName + " " + s.FathersName
            });
            ViewBag.TeacherId = new SelectList(stands, "Value", "Text", @class.TeacherId);
            return View(@class);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group @class = db.Groups.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group @class = db.Groups.Find(id);
            db.Groups.Remove(@class);
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
