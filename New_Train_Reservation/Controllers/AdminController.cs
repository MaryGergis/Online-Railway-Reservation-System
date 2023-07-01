using Microsoft.AspNetCore.Mvc;
using New_Train_Reservation.Data;
using New_Train_Reservation.Models;

namespace New_Train_Reservation.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDBcontext db;
        public AdminController(ApplicationDBcontext db)
        {
            this.db = db;
        }
        
        public IActionResult Admin_Home()
        {
            var email = HttpContext.Session.GetString("Email");
            if (email == null)
            {
                ViewBag.Error = "error";
            }
            else
            {
                ViewBag.Error = "";
                var Name = HttpContext.Session.GetString("Name");
                TempData["AdminName"] = Name;
            }
            TempData["Admin"] = email;

            return View();
        }

        //Tickets----------------------------------------------------------------------
        public IActionResult Index() //Tickets
        {
            var Email = HttpContext.Session.GetString("Email");
            TempData["AdminName"] = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToAction("Signin", "Admin");
            }
            //TICKETS & TRAIN EDITS
            List<Admin_Tickets> atk = db.Admin_Tickets.ToList();
            return View(atk);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var Email = HttpContext.Session.GetString("Email");
            TempData["AdminName"] = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToAction("Signin", "Admin");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(Admin_Tickets adt)
        {
            var Email = HttpContext.Session.GetString("Email");
            TempData["AdminName"] = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToAction("Signin", "Admin");
            }
            //TICKETS & TRAIN EDITS
            if (ModelState.IsValid)
            {
                var train = db.Trains.Where(c=> c.Train_Number == adt.TrainID).FirstOrDefault();
                var admin = db.Admin.Where(c => c.Email == Email).FirstOrDefault();

                if (train != null)
                {
                    adt.AdminID = admin.Id;
                    db.Add(adt);
                    db.SaveChanges();
                    TempData["TrainID"] = "";

                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    TempData["TrainID"] = "No such a Train";
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Email = HttpContext.Session.GetString("Email");
            TempData["AdminName"] = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToAction("Signin", "Admin");
            }
            var atk = db.Admin_Tickets.Where(x => x.Id == id).FirstOrDefault();
            if (atk == null)
            {
                return new NotFoundResult();
            }
            return View(atk);
        }

        [HttpPost]
        public IActionResult Edit(Admin_Tickets adk)
        {
            if (ModelState.IsValid)
            {
                var Email = HttpContext.Session.GetString("Email");
                var admin = db.Admin.Where(c => c.Email == Email).FirstOrDefault();
                TempData["AdminName"] = HttpContext.Session.GetString("Name");
                if (string.IsNullOrEmpty(Email))
                {
                    return RedirectToAction("Signin", "Admin");
                }
                adk.AdminID = admin.Id;

                db.Admin_Tickets.Update(adk);
                db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            return View(adk);

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var Email = HttpContext.Session.GetString("Email");
            TempData["AdminName"] = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToAction("Signin", "Admin");
            }
            var atk = db.Admin_Tickets.Where(x => x.Id == id).FirstOrDefault();
            if (atk == null)
            {
                return new NotFoundResult();
            }
            return View(atk);
        }
        [HttpPost]
        public IActionResult Delete(Admin_Tickets atk)
        {
            var Email = HttpContext.Session.GetString("Email");
            TempData["AdminName"] = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToAction("Signin", "Admin");
            }
            db.Admin_Tickets.Remove(db.Admin_Tickets.FirstOrDefault(c => c.Id == atk.Id));
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
            
        }

        //Trains------------------------------------------------------------------------------- 
        public IActionResult Train_Index()
        {
            var Email = HttpContext.Session.GetString("Email");
            TempData["AdminName"] = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToAction("Signin", "Admin");
            }
            //TICKETS & TRAIN EDITS
            List<Trains> atk = db.Trains.ToList();
            return View(atk);
        }
        [HttpGet]
        public IActionResult Train_Create()
        {
            var Email = HttpContext.Session.GetString("Email");
            TempData["AdminName"] = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToAction("Signin", "Admin");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Train_Create(Trains adt)
        {
            var Email = HttpContext.Session.GetString("Email");
            TempData["AdminName"] = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToAction("Signin", "Admin");
            }
            //TICKETS & TRAIN EDITS
            if (ModelState.IsValid)
            {
                db.Add(adt);
                db.SaveChanges();
                return RedirectToAction("Train_Index", "Admin");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Train_Edit(int id)
        {
            var Email = HttpContext.Session.GetString("Email");
            TempData["AdminName"] = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToAction("Signin", "Admin");
            }
            var atk = db.Trains.Where(x => x.ID == id).FirstOrDefault();
            if (atk == null)
            {
                return new NotFoundResult();
            }
            return View(atk);
        }

        [HttpPost]
        public IActionResult Train_Edit(Trains adk)
        {
            
            var Email = HttpContext.Session.GetString("Email");
            TempData["AdminName"] = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToAction("Signin", "Admin");
            }
            if (ModelState.IsValid)
            {
                db.Trains.Update(adk);
                db.SaveChanges();
                return RedirectToAction("Train_Index", "Admin");
            }
            return View(adk);

        }
        [HttpGet]
        public IActionResult Train_Delete(int id)
        {
            var Email = HttpContext.Session.GetString("Email");
            TempData["AdminName"] = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToAction("Signin", "Admin");
            }
            var atk = db.Trains.Where(x => x.ID == id).FirstOrDefault();
            if (atk == null)
            {
                return new NotFoundResult();
            }
            return View(atk);
        }
        [HttpPost]
        public IActionResult Train_Delete(Trains atk)
        {
            var Email = HttpContext.Session.GetString("Email");
            TempData["AdminName"] = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToAction("Signin", "Admin");
            }
            db.Trains.Remove(db.Trains.FirstOrDefault(c => c.ID == atk.ID));
            db.SaveChanges();
            return RedirectToAction("Train_Index", "Admin");

        }
        //Sign------------------------------------------------------------------------------- 

    

        [HttpGet]
        public IActionResult Signin()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Signin(Admin admin)
        {
           
                var adm = db.Admin.FirstOrDefault(a => a.Email == admin.Email);
                if (adm != null && adm.Password == admin.Password)
                {
                    HttpContext.Session.SetString("Email", admin.Email);
                    HttpContext.Session.SetString("Name", adm.Name);
                    return RedirectToAction("Admin_Home");
                }
                else
                {
                    ViewBag.message = "Wrong Password or Email Address";
                    ModelState.AddModelError("", "Invalid Email or Password");

                }
                return View();
          
        }
        [HttpGet]
        public IActionResult Admin_Edit()
        {
            var Email = HttpContext.Session.GetString("Email");
            TempData["AdminName"] = HttpContext.Session.GetString("Name");
            var admin = db.Admin.FirstOrDefault(x => x.Email == Email);
            if (admin == null)
            {
                return new NotFoundResult();
            }
            return View(admin);
        }

        [HttpPost]
        public IActionResult Admin_Edit(Admin admin)
        {
            
                admin.Email = HttpContext.Session.GetString("Email");
                db.Admin.Update(admin);
            TempData["AdminName"] = HttpContext.Session.GetString("Name");

            db.SaveChanges();
                return View(admin);
            
            
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                HttpContext.Session.Remove("Email");
            }
            return RedirectToAction("Admin_Home", "Admin");

        }
        //--------------------------------------------------------
        

    }
}
