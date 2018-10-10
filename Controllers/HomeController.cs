using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharePass.Encryptors;
using SharePass.Helpers;
using SharePass.Models;

namespace SharePass.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetLink(IFormCollection data)
        {
            var pass = data["Password"];

            var enc = new AESEncryptor().Encrypt(pass);
            var link = new LinkGenerator().Generate();
            // write to db enc and link

            var actionLink = Url.Action("GetPass", new{url = link });
            return new JsonResult(actionLink);
        }

        [HttpGet]
        public IActionResult GetPass(string url)
        {
            // check if exists
            // check if expired;
            // read form db
            // delete salt

            //var dec = new AESEncryptor().Decrypt(enc, key, iV);

            var model = new PassViewModel();
            model.Password = url;
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Info()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
