using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cerebro.Services;

namespace Cerebro.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult StatusBoard()
        {
            var model = StatusBoardService.BuildStatusBoardViewModel();
            
            return View(model);
        }
    }
}
