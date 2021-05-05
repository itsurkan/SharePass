using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharePass.Encryptors;
using SharePass.Helpers;
using SharePass.Models;

namespace SharePass.Controllers
{
    public class PassController : Controller
    {
        private readonly SharePassContext _context;
        private readonly ISaltGenerator saltGenerator;
        private readonly ILinkGenerator linkGenerator;
        private readonly IEncryptor encryptor;
        public PassController(SharePassContext context, ISaltGenerator saltGenerator, ILinkGenerator linkGenerator, IEncryptor encryptor)
        {
            this._context = context;
            this.encryptor = encryptor;
            this.saltGenerator = saltGenerator;
            this.linkGenerator = linkGenerator;
        }

        [HttpPost]
        public JsonResult GetLink(IFormCollection  data)
        {
            var model = new PassModel(saltGenerator, linkGenerator, encryptor).New(data["Password"]);

            _context.Passwords.Add(model);
            _context.SaveChanges();

            var actionLink =$"{Request.Scheme}://{Request.Host.Value}{Url.Action("GetPass", new { url = model.Link })}";
            return new JsonResult(actionLink);
        }

        [HttpGet]
        public JsonResult GetPass(string url)
        {
            var passModel = _context.Passwords.Include(s => s.Salt).FirstOrDefault(p => p.Link == url);

            string recoveryResult = passModel == null ? "Wrong link" : passModel.GetRecoveredPass();

            _context.Passwords.Remove(passModel);
            _context.SaveChanges();

            return new JsonResult(new { Result = recoveryResult });
        }
    }
}
