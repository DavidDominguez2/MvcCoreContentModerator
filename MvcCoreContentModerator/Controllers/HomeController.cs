using Microsoft.AspNetCore.Mvc;
using MvcCoreContentModerator.Models;
using MvcCoreContentModerator.Services;
using System.Diagnostics;

namespace MvcCoreContentModerator.Controllers {
    public class HomeController : Controller {

        private ServiceContentModerator contentModerator;

        public HomeController(ServiceContentModerator contentModerator) {
            this.contentModerator = contentModerator;
        }

        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string text) {
            var result = this.contentModerator.ModerateText(text);
            ViewData["result"] = result;
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}