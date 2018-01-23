using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebСourseProject.Models;

namespace WebСourseProject.Controllers
{
    public class TopicsController : Controller
    {
        private SiteContext db = new SiteContext();

        // GET: Topics
        public ActionResult Index()
        {
            return View(db.Topics.ToList());
        }

        // GET: Topics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Topics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Header,Content")] Topic topic, HttpPostedFileBase upload)
        {
            topic.published = DateTime.Now;
            
                if (upload != null && upload.ContentLength > 0)
                {
                    string fileExt = Path.GetExtension(upload.FileName);
                    if (fileExt == ".jpeg" || fileExt == ".jpg")
                    {
                        var fileName = upload.FileName;
                        var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        upload.SaveAs(path);
                        path = Path.Combine("\\Images\\", fileName);
                        topic.imgPath = path;
                    }
                    else
                    {
                        topic.imgPath = "none";
                    }
                }
                else
                {
                    topic.imgPath = "none";
                }
                topic.published = DateTime.Now;
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        // GET: Topics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Header,Content")] Topic topic, HttpPostedFileBase upload)
        {
            topic.published = DateTime.Now;
                
                if (upload != null && upload.ContentLength > 0)
                {
                    string fileExt = Path.GetExtension(upload.FileName);
                    if (fileExt == ".jpeg" || fileExt == ".jpg")
                    {
                        var fileName = upload.FileName;
                        var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                        upload.SaveAs(path);
                        path = Path.Combine("\\Images\\", fileName);
                        topic.imgPath = path;
                        db.Entry(topic).State = EntityState.Modified;
                        db.SaveChanges();
                }
                }
                    return RedirectToAction("Index");
            
        }

        // GET: Topics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
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
