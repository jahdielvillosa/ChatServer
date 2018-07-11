using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChatServer.Models;

namespace ChatServer.Controllers
{
    public class NotificationsController : Controller
    {
        private ChatServerModelContainer db = new ChatServerModelContainer();

        // GET: Notifications
        public ActionResult Index()
        {
            return View(db.Notifications.ToList());
        }

        // GET: Notifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notifications notifications = db.Notifications.Find(id);
            if (notifications == null)
            {
                return HttpNotFound();
            }
            return View(notifications);
        }

        // GET: Notifications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ServiceId,JobId,AppId,Recipient,Message,SendDT,Status")] Notifications notifications)
        {
            if (ModelState.IsValid)
            {
                db.Notifications.Add(notifications);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(notifications);
        }

        // GET: Notifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notifications notifications = db.Notifications.Find(id);
            if (notifications == null)
            {
                return HttpNotFound();
            }
            return View(notifications);
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ServiceId,JobId,AppId,Recipient,Message,SendDT,Status")] Notifications notifications)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notifications).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(notifications);
        }

        // GET: Notifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notifications notifications = db.Notifications.Find(id);
            if (notifications == null)
            {
                return HttpNotFound();
            }
            return View(notifications);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notifications notifications = db.Notifications.Find(id);
            db.Notifications.Remove(notifications);
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
