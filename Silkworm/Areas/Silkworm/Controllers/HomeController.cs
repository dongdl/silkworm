using System;
using System.Web.Http;
using System.Web.Mvc;

namespace ATEC.Silkworm.Areas.Silkworm.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
    }
}