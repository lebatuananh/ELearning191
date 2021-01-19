using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeaLearning.Common;
using PeaLearning.Common.Utils;
using PeaLearning.Infrastructure;
using PeaLearning.Website.Models;
using System.Diagnostics;
using System.Linq;
using static PeaLearning.Common.Constants;

namespace PeaLearning.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(PeaDbContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.TitlePage = SEO.AddTitle(Meta.HomeTitle);
            ViewBag.MetaDescription = SEO.AddMeta("description", Meta.HomeDesc);
            //ViewBag.MetaFacebook = SEO.AddMetaImages("photo/2020/03/03/banner1.jpg",);
            ViewBag.MetaCanonical = SEO.MetaCanonical(StaticVariable.BaseUrl);
            return View();
        }

        [Route("lien-he")]
        public IActionResult Contact()
        {
            ViewBag.TitlePage = SEO.AddTitle(Meta.ContactTitle);
            ViewBag.MetaDescription = SEO.AddMeta("description", Meta.ContactDesc);
            return View();
        }

        [Route("gioi-thieu")]
        public IActionResult Introduce()
        {
            ViewBag.TitlePage = SEO.AddTitle(Meta.AboutTitle);
            ViewBag.MetaDescription = SEO.AddMeta("description", Meta.AboutDesc);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
