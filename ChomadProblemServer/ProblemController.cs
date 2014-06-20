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
            var correct = new[] { 1, 1, 4, 3, 3, 4, 2, 1, 3, 2 };
            return correct
                .Zip(answers, (c, a) => c == a)
                .Count(right => right);
        }
    }
}
