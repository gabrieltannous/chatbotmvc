using ChatBot_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatBot_MVC.Controllers
{
    public class HomeController : Controller
    {
        private DefaultConnectionEntities db = new DefaultConnectionEntities();
        static List<Transcript> transcripts = new List<Transcript>();
        Rules rules = new Rules();
        public ActionResult Index()
        {
            return View(transcripts.ToList());
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

        [HttpPost]
        public ActionResult Message(string msg)
        {
            var response = rules.RespondToInput(msg, db.Rules.ToList()) ?? "Sorry, I couldn't understand that.";
            transcripts.Add(new Transcript { Name = "You: ", Message = msg });
            transcripts.Add(new Transcript { Name = "Bot: ", Message = response });
            return RedirectToAction("Index");
        }
    }
}