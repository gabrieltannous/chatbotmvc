using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChatBot_MVC.Models;

namespace ChatBot_MVC.Controllers
{
    [Authorize]
    public class ReactionsController : Controller
    {
        private DefaultConnectionEntities db = new DefaultConnectionEntities();

        // GET: Reactions
        public ActionResult Index()
        {
            return View(db.Reactions.ToList());
        }

        // GET: Reactions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Reaction Maintainer, Editor")]
        public ActionResult Create([Bind(Include = "Id,Object,Action,Mood,Emoji")] Reaction reaction)
        {
            if (ModelState.IsValid)
            {
                db.Reactions.Add(reaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reaction);
        }

        // GET: Reactions/Edit/5
        [Authorize(Roles = "Reaction Maintainer, Editor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reaction reaction = db.Reactions.Find(id);
            if (reaction == null)
            {
                return HttpNotFound();
            }
            return View(reaction);
        }

        // POST: Reactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Reaction Maintainer, Editor")]
        public ActionResult Edit([Bind(Include = "Id,Object,Action,Mood,Emoji")] Reaction reaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reaction);
        }

        // GET: Reactions/Delete/5
        [Authorize(Roles = "Reaction Maintainer, Editor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reaction reaction = db.Reactions.Find(id);
            if (reaction == null)
            {
                return HttpNotFound();
            }
            return View(reaction);
        }

        // POST: Reactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Reaction Maintainer, Editor")]
        public ActionResult DeleteConfirmed(int id)
        {
            Reaction reaction = db.Reactions.Find(id);
            db.Reactions.Remove(reaction);
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
