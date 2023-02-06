using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace APRERP.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View("Error");
        }
        public ViewResult NotFound()
        {
            Response.StatusCode = 404;  //you may want to set this to 200
            return View("NotFound");
        }
    }
}
