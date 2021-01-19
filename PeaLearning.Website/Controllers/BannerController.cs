using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PeaLearning.Website.Controllers
{
    public class BannerController : BaseController
    {
        public IActionResult Index()
        {
            // ReSharper disable once Mvc.ViewNotResolved
            return View();
        }
    }
}
