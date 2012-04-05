using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cerebro.Services;
using Cerebro.DataFactories;

namespace Cerebro.Controllers
{
    public class HomeController : Controller
    {
        private StatusBoardService _statusBoardService;

        public HomeController()
        {
            _statusBoardService = new StatusBoardService(new TargetProcessFactory());
        }

        public ActionResult StatusBoard()
        {
            var model = _statusBoardService.BuildStatusBoardViewModel();
            
            return View(model);
        }
    }
}
