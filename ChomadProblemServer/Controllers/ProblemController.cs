using System;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ChomadProblemServer
{
    [EnableCors("Any")]
    public class ProblemController : Controller
    {
        private IConfiguration Config { get; set; }

        public ProblemController(IConfiguration config)
        {
            this.Config = config;
        }

        [HttpGet, Route("/")]
        public ActionResult Index()
        {
            var enforceHTTPS = this.Config.GetValue<bool>("EnforceHTTPS", defaultValue: false);
            var scheme = this.Request.Headers.TryGetValue("X-Forwarded-Proto", out var value) ? value.ToString() : this.Request.Scheme;
            if (enforceHTTPS && scheme == "http")
                return RedirectPermanent($"https://{this.Request.Host.Host}/");

            this.ViewBag.SiteBase = $"{scheme}://{this.Request.Host.Host}";
            return View();
        }

        [HttpPost, Route("answer")]
        public int Post([FromBody]int[] answers)
        {
            var correct = new[] { 1, 1, 4, 3, 3, 4, 2, 1, 3, 2 };
            return correct
                .Zip(answers, (c, a) => c == a)
                .Count(right => right);
        }
    }
}
