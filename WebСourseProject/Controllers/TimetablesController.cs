
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebСourseProject.Models;

namespace WebСourseProject.Controllers
{
    public class TimetablesController : Controller
    {
        private SiteContext db = new SiteContext();

        // GET: Timetables
        public ActionResult Index()
        {
            var timetables = db.Timetables.Include(t => t.Group)
                .Include(t => t.Period).OrderByDescending(t => t.Period.end);
            return View(timetables.ToList());
        }

        // GET: Timetables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timetable timetable = db.Timetables.Find(id);
            if (timetable == null)
            {
                return HttpNotFound();
            }
            return View(timetable);
        }

        // GET: Timetables/Create
        public ActionResult Create()
        {
                var stands = db.Groups.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.number + " " + s.letter
            });
            
            ViewBag.GroupId = new SelectList(stands, "Value", "Text");
            ViewBag.PeriodId = new SelectList(db.Periods, "Id", "name");
            return View();
        }

        // POST: Timetables/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GroupId,PeriodId")] Timetable timetable)
        {
            if (ModelState.IsValid)
            {
                db.Timetables.Add(timetable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var stands = db.Groups.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.number + " " + s.letter
            });

            ViewBag.GroupId = new SelectList(stands, "Value", "Text", timetable.GroupId);
            
            ViewBag.PeriodId = new SelectList(db.Periods, "Id", "name", timetable.PeriodId);
            return View(timetable);
        }

        // GET: Timetables/Edit/5
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timetable timetable = db.Timetables.Include(t => t.Group)
                .Include(t => t.Period).Where(s => s.Id == id).First();
            if (timetable == null)
            {
                return HttpNotFound();
            }
            
            Session["TimeTableId"] = timetable.Id;
            ViewBag.GroupId = new SelectList(db.Groups, "Id", "letter", timetable.GroupId);
            ViewBag.PeriodId = new SelectList(db.Periods, "Id", "name", timetable.PeriodId);
            var stands = db.Teachers.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.LastName + " " + s.FirstName + " " + s.FathersName
            });
            ViewBag.TeacherId = new SelectList(stands, "Value", "Text");
            return View(timetable);
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult getLessonInfo(int day, int lesson)
        {
            int TimeTableid = (int)Session["TimeTableId"];
            Lesson ls = db.Lessons.Where(l => l.TimetableId == TimeTableid).
                Where(l => l.day == day).Where(l => l.number == lesson).FirstOrDefault();
            if (ls == null)
            {
                return Json("no");
            }
            LessonRequest lr = new LessonRequest();
            lr.name = ls.name;
            lr.teacher = ls.TeachersName;
            return Json(lr);
        }

        [HttpPost]
        public JsonResult AddOrUpdateLesson(int day, int lesson, string subject, int TeacherId)
        {
            int TimetableId = (int)Session["TimeTableId"];
            Lesson find = db.Lessons.Where(s => s.TimetableId == TimetableId).
                Where(s => s.day == day).Where(s => s.number == lesson).FirstOrDefault();
            if (find == null)
            {
                Lesson ls = new Lesson();
                ls.day = day;
                ls.number = lesson;
                ls.name = subject;
                Teacher teacher = db.Teachers.Find(TeacherId);
                ls.TeachersName = teacher.LastName + " " + teacher.FirstName[0] + "." + teacher.FathersName[0] + ".";
                ls.TimetableId = (int)Session["TimeTableId"];
                db.Lessons.Add(ls);
            }
            else
            {
                Teacher teacher = db.Teachers.Find(TeacherId);
                find.TeachersName = teacher.LastName + " " + teacher.FirstName[0] + "." + teacher.FathersName[0] + ".";
                find.name = subject;
                db.Lessons.Attach(find);
                db.Entry(find).State = EntityState.Modified;
            }
            db.SaveChanges();
            return Json(true);
        }

        [HttpPost]
        public JsonResult getLessons()
        {
            int TimetableId = (int)Session["TimeTableId"];
            var data = db.Lessons.Where(s => s.TimetableId == TimetableId).ToList();
            return Json(data);
        }
    }
}
