using Microsoft.AspNetCore.Mvc;
using New_Train_Reservation.Data;
using New_Train_Reservation.Models;

namespace New_Train_Reservation.Controllers
{
    public class SettingsController : Controller
    {
        //Suggestions
        //Contacts US
        private ApplicationDBcontext db;
        public SettingsController(ApplicationDBcontext db) { 
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Complaints_Suggestions()
        {
            TempData["sc_success"] = "";
            TempData["Name"] = HttpContext.Session.GetString("Name");
            return View();
        }
        [HttpPost]
        public IActionResult Complaints_Suggestions(Suggestions_Complaints sc)
        {
            var user = db.Users.Where(c=> c.Email== sc.Email).FirstOrDefault();
            sc.UsersID = user.ID;

            if (sc !=null && ModelState.IsValid)
            {
                TempData["sc_success"] = "true";
                db.Add(sc);
                db.SaveChanges();
                return View();
            }
            return new NotFoundResult();
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            ViewBag.submit = "";
            TempData["Name"] = HttpContext.Session.GetString("Name");
            return View();
        }
        [HttpPost]
        public IActionResult ContactUs(IFormCollection req)
        {
            ViewBag.submit = req["submit"];
            return View();
        }


    }
}
