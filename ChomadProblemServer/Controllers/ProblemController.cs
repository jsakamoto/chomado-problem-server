using System;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;

namespace ChomadProblemServer
{
    [EnableCors("Any")]
    public class ProblemController : Controller
    {

        [HttpGet, Route("/")]
        public ActionResult Index()
        {
            if (this.Request.Scheme == "http")
                return RedirectPermanent($"https://{this.Request.Host.Host}/");

            this.ViewBag.SiteBase = this.Request.GetDisplayUrl().TrimEnd('/');
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
