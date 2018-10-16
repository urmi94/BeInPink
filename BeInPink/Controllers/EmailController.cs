using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeInPink.Models;
using BeInPink.Utils;

namespace BeInPink.Controllers
{
    public class EmailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Email
        public ActionResult Index()
        {
            return View(db.SendEmailViewModels.ToList());
        }

        // GET: Email/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendEmailViewModel sendEmailViewModel = db.SendEmailViewModels.Find(id);
            if (sendEmailViewModel == null)
            {
                return HttpNotFound();
            }
            return View(sendEmailViewModel);
        }

        // GET: Email/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Email/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ToEmail,Subject,Contents")] SendEmailViewModel sendEmailViewModel)
        {
            if (ModelState.IsValid)
            {
                db.SendEmailViewModels.Add(sendEmailViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sendEmailViewModel);
        }

        // GET: Email/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendEmailViewModel sendEmailViewModel = db.SendEmailViewModels.Find(id);
            if (sendEmailViewModel == null)
            {
                return HttpNotFound();
            }
            return View(sendEmailViewModel);
        }

        // POST: Email/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ToEmail,Subject,Contents")] SendEmailViewModel sendEmailViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sendEmailViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sendEmailViewModel);
        }

        // GET: Email/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendEmailViewModel sendEmailViewModel = db.SendEmailViewModels.Find(id);
            if (sendEmailViewModel == null)
            {
                return HttpNotFound();
            }
            return View(sendEmailViewModel);
        }

        // POST: Email/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SendEmailViewModel sendEmailViewModel = db.SendEmailViewModels.Find(id);
            db.SendEmailViewModels.Remove(sendEmailViewModel);
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
        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }

        [HttpPost]
        public ActionResult Send_Email(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }
    }
}
