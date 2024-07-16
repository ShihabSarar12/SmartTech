using SmartTech.Models;
using System.Web.Mvc;

namespace SmartTech.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private SmartTechEntities db = new SmartTechEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Banner()
        {
            return View();
        }
        public ActionResult AddBanner()
        {

            return View();
        }
        public ActionResult Shipping()
        {
            return View();
        }
        public ActionResult AddCategories()
        {
            return View();
        }
        public ActionResult AddProducts()
        {
            return View();
        }

    }
}