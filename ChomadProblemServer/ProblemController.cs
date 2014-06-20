using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ChomadProblemServer
{
    [EnableCors("*", "*", "*")]
    public class ProblemController : ApiController
    {
        [HttpPost, Route("answer")]
        public int Post([FromBody]int[] answers)
        {
            var correct = new[] { 4, 3, 1, 2 };
            return correct
                .Zip(answers, (c, a) => c == a)
                .Count(right => right);
        }
    }
}
