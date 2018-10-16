using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
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
                myBookings = db.Bookings.Include(b => b.Client).Include(b => b.Coach).ToList();
            }
            else if (roles.Contains("Client"))
            {
                myBookings = db.Bookings.Where(m => m.Client.Id == currentUsrId).ToList();
            }
            else if (roles.Contains("Coach"))
            {
                myBookings = db.Bookings.Where(m => m.Coach.Id == currentUsrId).ToList();
            }

            if (TempData.Count>0 )
                ViewBag.Result = TempData["msgFromApprove"].ToString();
            return View(myBookings);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.BookingClientId = new SelectList(db.ClientUsers, "Id", "FirstName");
            ViewBag.BookingCoachId = new SelectList(db.CoachUsers, "Id", "FirstName");
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
                List<Booking> myBookings = new List<Booking>();
                myBookings = db.Bookings.Where(m => m.Coach.Id == booking.BookingCoachId && m.BookingStatus == Booking._bookingStatus.Confirmed && DbFunctions.TruncateTime(m.BookingDateTime) == booking.BookingDateTime.Date).ToList();// && ((booking.BookingDateTime.Subtract(m.BookingDateTime).Minutes >30) || (booking.BookingDateTime.Subtract(m.BookingDateTime).Minutes < 30))).ToList();
                if (myBookings.Count > 0)
                {
                    foreach (Booking b in myBookings)
                    {
                        TimeSpan interval = b.BookingDateTime - booking.BookingDateTime;
                        var timeDiff = interval.Minutes;

                        if (timeDiff >= -30 && timeDiff <= 30)
                        {
                            ViewBag.Result = "Your coach has other appointments at this time.";
                            ViewBag.BookingClientId = new SelectList(db.Users, "Id", "FirstName", booking.BookingClientId);
                            ViewBag.BookingCoachId = new SelectList(db.Users, "Id", "FirstName", booking.BookingCoachId);
                            return View(booking);
                        }
                    }

                }
                else
                {
                    var userId = User.Identity.GetUserId();
                    booking.BookingClientId = userId;
                    db.Bookings.Add(booking);
                    var result = db.SaveChanges();

                    if (result > 0)
                    {
                        EmailSender es = new EmailSender();
                        booking.Coach = db.CoachUsers.FirstOrDefault(u => u.Id == booking.BookingCoachId);
                        booking.Client = db.ClientUsers.FirstOrDefault(u => u.Id == userId);

                        AccountController ac = new AccountController();

                        //Email notification to Coach
                        var CoachcallbackUrl = Url.Action(
                            "Index", "Bookings",
                            new { userId = booking.BookingCoachId },
                            protocol: Request.Url.Scheme);
                        StringBuilder emailForCoach = new StringBuilder();
                        emailForCoach.AppendLine("Hi " + booking.Coach.FirstName + ",\n\n");
                        emailForCoach.AppendLine("You have received a new appointment request from " + booking.Client.FirstName + " " + booking.Client.LastName + " on " + booking.BookingDateTime);
                        emailForCoach.AppendLine("To view and update your booking requests, click <a href=\"" + CoachcallbackUrl + "\">here</a>");
                        es.Send(booking.Coach.Email,
                           "You have got a Client Appointment Request",
                           emailForCoach.ToString());

                        //Email notification to Client
                        var ClientCallbackUrl = Url.Action(
                           "Index", "Bookings",
                           new { userId = booking.BookingCoachId },
                           protocol: Request.Url.Scheme);
                        StringBuilder emailForClient = new StringBuilder();
                        emailForClient.AppendLine("Hi " + booking.Client.FirstName + " " + booking.Client.LastName + ",\n\n");
                        emailForClient.AppendLine("Your appointment request for " + booking.Coach.FirstName + " " + booking.Coach.LastName + " on " + booking.BookingDateTime + "has been sent to the coach. Your booking status (Approved/Rejected) will be notified to you.");
                        emailForClient.AppendLine("To view and update your booking requests, click <a href=\"" + ClientCallbackUrl + "\">here</a>");
                        es.Send(booking.Client.Email,
                           "Your Appointment Request has been Submitted",
                           emailForClient.ToString());

                        ViewBag.Result = "Your coach has been notified about this booking.";

                        return RedirectToAction("Index");
                    }
                }
            }

            ViewBag.BookingClientId = new SelectList(db.Users, "Id", "FirstName", booking.BookingClientId);
            ViewBag.BookingCoachId = new SelectList(db.Users, "Id", "FirstName", booking.BookingCoachId);
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
        public ActionResult Edit(Booking booking)
        {
            booking.BookingStatus = Booking._bookingStatus.Requested;
            db.Entry(booking).State = EntityState.Modified;
            var result = db.SaveChanges();

            if (result > 0)
            {
                EmailSender es = new EmailSender();
                booking.Coach = db.CoachUsers.FirstOrDefault(u => u.Id == booking.BookingCoachId);
                booking.Client = db.ClientUsers.FirstOrDefault(u => u.Id == booking.BookingClientId);

                AccountController ac = new AccountController();

                //Email notification to Coach
                var CoachcallbackUrl = Url.Action(
                    "Index", "Bookings",
                    new { userId = booking.BookingCoachId },
                    protocol: Request.Url.Scheme);
                StringBuilder emailForCoach = new StringBuilder();
                emailForCoach.AppendLine("Hi " + booking.Coach.FirstName + ",\n\n");
                emailForCoach.AppendLine("Your appointment request from " + booking.Client.FirstName + " " + booking.Client.LastName + "has been updated.");
                emailForCoach.AppendLine("To view the changes of your booking requests, click <a href=\"" + CoachcallbackUrl + "\">here</a>");
                es.Send(booking.Coach.Email,
                   "Update on Appointment Request from " + booking.Client.FirstName,
                   emailForCoach.ToString());

                //Email notification to Client
                var ClientCallbackUrl = Url.Action(
                   "Index", "Bookings",
                   new { userId = booking.BookingCoachId },
                   protocol: Request.Url.Scheme);
                StringBuilder emailForClient = new StringBuilder();
                emailForClient.AppendLine("Hi " + booking.Client.FirstName + " " + booking.Client.LastName + ",\n\n");
                emailForClient.AppendLine("Your appointment request for " + booking.Coach.FirstName + " " + booking.Coach.LastName + " has been updated by the Administrator. This update has been sent to the coach. Your booking status (Approved/Rejected) will be notified to you.");
                emailForClient.AppendLine("To view and update your booking requests, click <a href=\"" + ClientCallbackUrl + "\">here</a>");
                es.Send(booking.Client.Email,
                   "Your Appointment Request has been Updated",
                   emailForClient.ToString());

                ViewBag.Result = "Your coach has been notified about this update.";

                return RedirectToAction("Index");
            }
            ViewBag.BookingClientId = new SelectList(db.Users, "Id", "FirstName", booking.BookingClientId);
            ViewBag.BookingCoachId = new SelectList(db.Users, "Id", "FirstName", booking.BookingCoachId);
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
            else
            {
                var userId = User.Identity.GetUserId();
                booking.BookingClientId = userId;
                db.Bookings.Remove(booking);
                var result = db.SaveChanges();

                if (result > 0)
                {
                    EmailSender es = new EmailSender();
                    booking.Coach = db.CoachUsers.FirstOrDefault(u => u.Id == booking.BookingCoachId);
                    booking.Client = db.ClientUsers.FirstOrDefault(u => u.Id == userId);

                    AccountController ac = new AccountController();

                    //Email notification to Coach
                    StringBuilder emailForCoach = new StringBuilder();
                    emailForCoach.AppendLine("Hi " + booking.Coach.FirstName + ",\n\n");
                    emailForCoach.AppendLine("Your appointment request from " + booking.Client.FirstName + " " + booking.Client.LastName + "has been deleted.");
                    es.Send(booking.Coach.Email,
                      booking.Client.FirstName + " deleted the Appointment Request",
                      emailForCoach.ToString());

                    //Email notification to Client
                    StringBuilder emailForClient = new StringBuilder();
                    emailForClient.AppendLine("Hi " + booking.Client.FirstName + " " + booking.Client.LastName + ",\n\n");
                    emailForClient.AppendLine("Your appointment request for " + booking.Coach.FirstName + " " + booking.Coach.LastName + " has been deleted and notified to the coach.");
                    es.Send(booking.Client.Email,
                       "Your Appointment Request has been deleted",
                       emailForClient.ToString());

                    ViewBag.Result = "Your coach has been notified about this update.";

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }

        }
        // GET: Bookings/Approve/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        public ActionResult Approve(int? id)
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
            else
            {
                if (ModelState.IsValid)
                {
                    List<Booking> myBookings = new List<Booking>();
                    myBookings = db.Bookings.Where(m => m.Coach.Id == booking.BookingCoachId && m.BookingStatus == Booking._bookingStatus.Confirmed && DbFunctions.TruncateTime(m.BookingDateTime) == booking.BookingDateTime.Date).ToList();
                    if (myBookings.Count > 0)
                    {
                        foreach (Booking b in myBookings)
                        {
                            TimeSpan interval = b.BookingDateTime - booking.BookingDateTime;
                            var timeDiff = interval.Minutes;

                            if (timeDiff >= -30 && timeDiff <= 30)
                            {
                                TempData["msgFromApprove"] = "You have other appointment at this time.";
                                 return RedirectToAction("Index");
                            }
                        }
                    }
                    else
                    {
                        booking.BookingStatus = Booking._bookingStatus.Confirmed;
                        db.Entry(booking).State = EntityState.Modified;
                        var result = db.SaveChanges();

                        if (result > 0)
                        {
                            EmailSender es = new EmailSender();

                            AccountController ac = new AccountController();

                            //Email notification to Client
                            StringBuilder emailForClient = new StringBuilder();
                            emailForClient.AppendLine("Hi " + booking.Client.FirstName + " " + booking.Client.LastName + ",\n\n");
                            emailForClient.AppendLine("Your appointment request for Coach " + booking.Coach.FirstName + " " + booking.Coach.LastName + " has been approved.");
                            es.Send(booking.Client.Email,
                               "Your Appointment Request has been approved",
                               emailForClient.ToString());

                            return RedirectToAction("Index");
                        }
                        return RedirectToAction("Index");

                    }
                    
                }
                ViewBag.BookingClientId = new SelectList(db.Users, "Id", "FirstName", booking.BookingClientId);
                ViewBag.BookingCoachId = new SelectList(db.Users, "Id", "FirstName", booking.BookingCoachId);
                return View(booking);
            }
        }
        // GET: Bookings/Reject/5
        public ActionResult Reject(int? id)
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

        // POST: Bookings/Reject/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reject([Bind(Include = "BookingID,BookingDateTime,BookingClientId,BookingCoachId,BookingStatus,RejectReason")] Booking booking)
        {
            booking.BookingStatus = Booking._bookingStatus.Rejected;
            db.Entry(booking).State = EntityState.Modified;
            //db.Entry(booking).State = EntityState.Modified;
            var result = db.SaveChanges();

            if (result > 0)
            {
                EmailSender es = new EmailSender();

                AccountController ac = new AccountController();

                //Email notification to Client
                var ClientcallbackUrl = Url.Action(
                "Edit", "Bookings",
                new { id = booking.BookingID },
                protocol: Request.Url.Scheme);

                booking.Coach = db.CoachUsers.FirstOrDefault(u => u.Id == booking.BookingCoachId);
                booking.Client = db.ClientUsers.FirstOrDefault(u => u.Id == booking.BookingClientId);

                StringBuilder emailForClient = new StringBuilder();
                emailForClient.AppendLine("Hi " + booking.Client.FirstName + " " + booking.Client.LastName + ",\n\n");
                emailForClient.AppendLine("Your appointment request for Coach " + booking.Coach.FirstName + " " + booking.Coach.LastName + " has been rejected. You can reschedule this request.");
                emailForClient.AppendLine("To reschedule, click <a href=\"" + ClientcallbackUrl + "\">here</a>");
                es.Send(booking.Client.Email,
                   "Your Appointment Request has been rejected",
                   emailForClient.ToString());

                return RedirectToAction("Index");
            }
            ViewBag.BookingClientId = new SelectList(db.Users, "Id", "FirstName", booking.BookingClientId);
            ViewBag.BookingCoachId = new SelectList(db.Users, "Id", "FirstName", booking.BookingCoachId);
            return View(booking);
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
