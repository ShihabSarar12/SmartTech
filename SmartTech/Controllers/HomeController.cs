using SmartTech.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace SmartTech.Controllers
{
    public class HomeController : Controller
    {
        private SmartTechEntities db = new SmartTechEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Shop() 
        {
            return View();
        }

        public ActionResult Contact()
        {
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
            var IsValid = db.users.Where(usr => usr.Equals(user.email)).FirstOrDefault();
            if (IsValid == null) 
            {
                ViewBag.Error = "User already exists with this email!";
                return View();
            }
            if (user.password != user.confirm_password) 
            {
                ViewBag.Error = "Password doesn\'t match!!";
                return View();
            }
            user.remember_token = "Good";
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
            return RedirectToAction("Index");
        }

    }
}