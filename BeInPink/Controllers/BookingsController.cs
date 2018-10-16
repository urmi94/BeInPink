using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BeInPink.Models;
using BeInPink.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BeInPink.Controllers
{

    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings
        public ActionResult Index()
        {
            List<Booking> myBookings = new List<Booking>();
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roles = userManager.GetRoles(User.Identity.GetUserId());
            string currentUsrId = User.Identity.GetUserId();

            if (roles.Contains("Admin"))
            {
                myBookings = db.Bookings.ToList();
            }
            else if(roles.Contains("Client"))
            {
                myBookings = db.Bookings.Where(m => m.BookingClientId == currentUsrId).ToList();
            }
            else if (roles.Contains("Coach"))
            {
                myBookings = db.Bookings.Where(m => m.BookingCoachId == currentUsrId).ToList();
            }


            return View(myBookings);
        }
       

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingID,BookingDateTime,BookingClientId,BookingCoachId,BookingStatus")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booking);
        }
        public ActionResult BookCoach()
        {
            return View();
        }

        // POST: Bookings/BookCoach
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult BookCoach(BookingViewModel bookingViewModel)
        {
            Booking booking = new Booking();
            booking.BookingDateTime = bookingViewModel.BookingDateTime;
            booking.BookingCoachId = (db.CoachUsers.First<Coach>(c => c.FirstName + " " + c.LastName == bookingViewModel.BookingCoach)).Id;
            booking.BookingClientId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                var result = db.SaveChanges();

                if (result > 0)
                {
                    EmailSender es = new EmailSender();
                    string coachEmail = (db.CoachUsers.First<Coach>(c => c.FirstName + " " + c.LastName == bookingViewModel.BookingCoach)).Email;

                    var userId = User.Identity.GetUserId();
                    Client client = db.ClientUsers.FirstOrDefault(u => u.Id == userId);

                    AccountController ac = new AccountController();

                    //Email notification to Coach
                   // var Coachcode = ac.UserManager.GenerateEmailConfirmationTokenAsync(booking.BookingCoachId);
                    var CoachcallbackUrl = Url.Action(
                       "Index", "Bookings",
                       new { userId = booking.BookingCoachId },//, code = Coachcode },
                       protocol: Request.Url.Scheme);
                    StringBuilder emailForCoach = new StringBuilder();
                    emailForCoach.AppendLine("Hi " + bookingViewModel.BookingCoach + ",\n\n");
                    emailForCoach.AppendLine("You have received a new appointment request from " + client.FirstName + " " + client.LastName + " on " + bookingViewModel.BookingDateTime);
                    emailForCoach.AppendLine("To view and update your booking requests, click <a href=\"" + CoachcallbackUrl + "\">here</a>");
                    es.Send(coachEmail,
                       "You have got a Client Appointment Request",
                       emailForCoach.ToString());

                    //Email notification to Client
                  //  var ClientCode = ac.UserManager.GenerateEmailConfirmationTokenAsync(booking.BookingCoachId);
                    var ClientCallbackUrl = Url.Action(
                       "Index", "Bookings",
                       new { userId = booking.BookingCoachId },//, code = ClientCode },
                       protocol: Request.Url.Scheme);
                    StringBuilder emailForClient = new StringBuilder();
                    emailForClient.AppendLine("Hi " + bookingViewModel.BookingClient + ",\n\n");
                    emailForClient.AppendLine("Your appointment request for " + bookingViewModel.BookingCoach + " on " + bookingViewModel.BookingDateTime + "has been sent to the coach. Your booking status (Approved/Rejected) will be notified to you.");
                    emailForClient.AppendLine("To view and update your booking requests, click <a href=\"" + ClientCallbackUrl + "\">here</a>");
                    es.Send(client.Email,
                       "Your Appointment Request has been Submitted",
                       emailForClient.ToString());

                    ViewBag.Result = "Your coach has been notified about this booking.";

                    return RedirectToAction("Index");
                }

            }

            return View(booking);
        }


        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingID,BookingDateTime,BookingClientId,BookingCoachId,BookingStatus")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
