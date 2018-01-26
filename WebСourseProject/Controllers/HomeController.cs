using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Collections;
using System.Web.Security;
using WebСourseProject.Filters;
using WebСourseProject.Models;

namespace WebСourseProject.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        private SiteContext db = new SiteContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult _PartialIndexArticles()
        {
            //var News = work.AllArticle3();
            //return PartialView(News);
            return PartialView();
        }

        public IQueryable<Topic> AllNews2()
        {
            return db.Set<Topic>().Where(u => u.Type == "Новость").Take(2);//Ура, корректный вывод новостей!
        }
    
        public ActionResult _PartialLayoutNews()
        {
             var News = db.Topics.ToList(); 
            //var News = work.AllNews2();

            //return PartialView(News);
            return PartialView();
        }

        public ActionResult TimeTables()
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

        public ActionResult News()
        {
            var topics = db.Topics.ToList();

            return View(topics);
        }
        public ActionResult Teachers()
        {
            var teachers = db.Teachers.ToList();

            return View(teachers);
        }

        [HttpPost]
        public JsonResult getTimetable(int GroupId, int PeriodId)
        {
            var data = db.Lessons.Where(s => s.timetable.GroupId == GroupId).
                Where(s => s.timetable.PeriodId == PeriodId).ToList();
            return Json(data);
        }

        public ActionResult TopicContent(int id)
        {
            var topic = db.Topics.Find(id);
            ViewBag.content = topic.Content;
            return View(topic);
        }























        public ActionResult ChangeCulture(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsolutePath;
            // Список культур
            List<string> cultures = new List<string>() { "ru", "en"};
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }
            // Сохраняем выбранную культуру в куки
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;   // если куки уже установлено, то обновляем значение
            else
            {

                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }
    }
}