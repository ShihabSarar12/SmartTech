using SmartTech.Models;
using System.Linq;
using System.Web.Mvc;

namespace SmartTech.Controllers
{
    public class HomeController : Controller
    {
        private readonly SmartTechEntities db = new SmartTechEntities();
        public ActionResult Index()
        {
            ViewBag.User = Session["user"];
            Session["prev_page"] = "Index";
            return View();
        }

        public ActionResult About()
        {
            Session["prev_page"] = "About";
            return View();
        }

        public ActionResult Shop()
        {
            Session["prev_page"] = "Shop";
            return View();
        }

        public ActionResult Contact()
        {
            Session["prev_page"] = "Contact";
            return View();
        }

        [HttpGet]
        public ActionResult Signin_Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Signin_Register(user user)
        {
            if (user.password != user.confirm_password) 
            {
                ViewBag.Error = "Password doesn\'t match!!";
                return View();
            }
            db.users.Add(user);
            db.SaveChanges();
            ViewBag.Error = "Signed up successfully";
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var IsValidUser = db.users.Where(usr => usr.email.Equals(email)
                    && usr.password.Equals(password)).FirstOrDefault();
            if (IsValidUser == null)
            {
                ViewBag.IsValid = "Credentials doesn\'t match!!";
                return View();
            }
            Session["user"] = IsValidUser;
            if (Session["prev_page"] == null)
                return View();
            return RedirectToAction(Session["prev_page"].ToString());
        }

    }
}