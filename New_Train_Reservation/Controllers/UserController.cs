using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using New_Train_Reservation.Data;
using New_Train_Reservation.Models;
using System.Dynamic;

namespace New_Train_Reservation.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDBcontext db;
        public UserController(ApplicationDBcontext db)
        {
            this.db = db;
        }
        //Before Signin
        public IActionResult Index()
        {
            var Email = HttpContext.Session.GetString("Email");

            if (Email == null)
            {
                ViewBag.Error= "error";

            }
            else
            {
                var Name = HttpContext.Session.GetString("Name");
                TempData["Name"] = Name;
                ViewBag.Error = "";
            }
            
            return View();
        }
        
        [HttpGet]
        public IActionResult SignUp()
        {
            ViewBag.message = "";
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Users user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetString("Name", user.User_Fname);
                return RedirectToAction("Signin", "User");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Signin()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Signin(Users user)
            
        {
            
            var use = db.Users.FirstOrDefault(u => u.Email == user.Email);
                if (use != null && use.Password == user.Password)
                {
                    HttpContext.Session.SetString("Email", user.Email);
                    HttpContext.Session.SetString("Name", use.User_Fname);

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ViewBag.message = "Wrong Password or Email Address";
                    ModelState.AddModelError("", "Invalid Email or Password");
                }
                return View();
            

        }
        [HttpGet]
        public IActionResult User_Edit()
        {
            if (ModelState.IsValid) { 
            var Email = HttpContext.Session.GetString("Email");
            TempData["Name"] = HttpContext.Session.GetString("Name");
            var user = db.Users.FirstOrDefault(x => x.Email == Email);
            if (user == null)
            {
                return new NotFoundResult();
            }
            return View(user);
            }
            return View();
        }

        [HttpPost]
        public IActionResult User_Edit(Users user)
        {
           
                user.Email = HttpContext.Session.GetString("Email");
                
                HttpContext.Session.SetString("Name", user.User_Fname);
                TempData["Name"] = HttpContext.Session.GetString("Name");
                db.Users.Update(user);

                db.SaveChanges();
                return View(user);
            
        }

        public IActionResult User_Delete()
        {
            if (ModelState.IsValid)
            {
                var Email = HttpContext.Session.GetString("Email");
                List<Users> lst = db.Users.ToList();
                foreach (var item in lst)
                {
                    if (item.Email == Email)
                    {
                        db.Remove(item);
                        break;
                    }
                }
                db.SaveChanges();
                HttpContext.Session.Remove("Email");
                return RedirectToAction("Index", "User");
            }
            return View();
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                HttpContext.Session.Remove("Email");
            }
            return RedirectToAction("Index", "User");

        }
        //---------------------------------------------------------------------------
        //---------------------------------------------------------------------------
        [HttpGet]
        public IActionResult Search()
        {
            var Email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToAction("SignUp", "User");
            }
            TempData["Name"] = HttpContext.Session.GetString("Name");
            return View();
        }
        [HttpPost]
        public IActionResult Search(Admin_Tickets ad)
        {
            if(ModelState.IsValid)
            return RedirectToAction("Search_Result", ad);
            return View();
        }
        public IActionResult Search_Result(Admin_Tickets ad)
        {
            var Email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToAction("SignUp", "User");
            }

            TempData["Name"] = HttpContext.Session.GetString("Name");
            
            var t = db.Admin_Tickets.Where(c => c.Destination == ad.Destination && c.Pickup_Station == ad.Pickup_Station).ToList();
                if(t.Count > 0)
                {
                TempData["Ticket_message"] = "";
                    return View(t);
                }
                else
                {
                    TempData["Ticket_message"] = "No Available Tickets";
                     return View(t);
                }
            
        }

        [HttpPost]
      public IActionResult Purchased_Tickets_Review(int id)
       {
            var email = HttpContext.Session.GetString("Email");
            TempData["Name"] = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("SignUp", "User");
            }
            List<Users> lst = db.Users.ToList();
            foreach(var item in lst)
            {
                if (item.Email == email)
                {
                    var t = db.Admin_Tickets.Where(c => c.Id == id).FirstOrDefault();
                    if (t != null)
                    {
                        User_Tickets tk = new User_Tickets(); 
                        tk.Ticket_Money = t.Ticket_Money;
                        tk.Time_Pickup = t.Time_Pickup;
                        tk.Ticket_Class = Convert.ToString(t.Ticket_Classes);
                        tk.Pickup_Station = t.Pickup_Station;
                        tk.Destination = t.Destination;
                        tk.Seat_Number = t.Seat_Number;
                        tk.Train_Number = t.TrainID;
                        tk.Train_Coach_Number = t.Train_Coach_Number;
                        item.Number_of_purchased_tickets= Convert.ToInt32(item.Number_of_purchased_tickets) + 1;
                        db.User_Tickets.Add(tk);
                        db.Admin_Tickets.Remove(t);
                        tk.usersid =item.ID;
                        db.Users.Update(item);
                        db.SaveChanges();
                        List<User_Tickets> user = db.User_Tickets.ToList();
                        TempData["PTicket"] = "";
                        return View(user);
                    }
                    else
                    {
                        TempData["PTicket"] = "There is No Purchased Tickets!";
                        return View();
                    }
                }
            }
            return RedirectToAction("Logout","User");
        }

        public IActionResult Cancellation(int id)
        {
            var email = HttpContext.Session.GetString("Email");
            TempData["Name"] = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("SignUp", "User");
            }

            var lst = db.User_Tickets.Where(c=> c.Id == id).FirstOrDefault();
                if (lst!=null)
                {
                    
                    Admin_Tickets admin_Tickets = new Admin_Tickets();

                    //tk.Id = t.Id;
                    admin_Tickets.Ticket_Money = lst.Ticket_Money;
                    admin_Tickets.Time_Pickup = lst.Time_Pickup;
                    if(lst.Ticket_Class=="AC1")
                        admin_Tickets.Ticket_Classes = Ticket_Classes.AC1;
                    else
                        admin_Tickets.Ticket_Classes = Ticket_Classes.AC2;
                    admin_Tickets.Pickup_Station = lst.Pickup_Station;
                    admin_Tickets.Destination = lst.Destination;
                    admin_Tickets.Seat_Number = lst.Seat_Number;
                    admin_Tickets.TrainID = lst.Train_Number;
                    admin_Tickets.Train_Coach_Number = lst.Train_Coach_Number;
                    admin_Tickets.AdminID = 1;
                    //Users u = new Users();
                    var user = db.Users.Where(c => c.Email == email).FirstOrDefault();
                
                    user.Number_of_purchased_tickets=Convert.ToInt32(user.Number_of_purchased_tickets)- 1;

                    db.Users.Update(user);
                    db.Admin_Tickets.Add(admin_Tickets);
                    db.User_Tickets.Remove(lst);
                    db.SaveChanges();
                   
                    return RedirectToAction("Purchased_Tickets", "User");
                    
                }
            
            return RedirectToAction("Logout", "User");
        }
        [HttpGet]
        public IActionResult Purchased_Tickets()
        {
            var email = HttpContext.Session.GetString("Email");
            TempData["Name"] = HttpContext.Session.GetString("Name");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("SignUp", "User");
            }

              List<User_Tickets> ut = db.User_Tickets.ToList();
            if (ut.Count > 0)
            {
                TempData["PTicket"] = "";

                return View(ut);
            }
            else
            {
                TempData["PTicket"] = "There is No Purchased Tickets!";
                return View(ut);
            }
        }
        //-----------------------------------------------------------------------------------------------



    }
}
