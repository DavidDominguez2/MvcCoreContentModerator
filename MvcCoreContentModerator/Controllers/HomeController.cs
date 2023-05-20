using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.ContentModerator.Models;
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
            Screen result = this.contentModerator.ModerateText(text);
            bool curse = (bool)result.Classification.ReviewRecommended;
			string correct = result.AutoCorrectedText;

            if (!curse) {
				ViewData["result"] = correct;
            } else {
                ViewData["MENSAJE"] = "No se puede enviar el mensaje, se ha detectado una palabra ilícita.";
            }

			return View();
        }
    }
}