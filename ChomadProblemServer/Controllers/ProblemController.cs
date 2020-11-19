using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ChomadProblemServer
{
    [ApiController, EnableCors("Any")]
    public class ProblemController : ControllerBase
    {
        [HttpPost("answer")]
        public int Post([FromBody] int[] answers)
        {
            var correct = new[] { 1, 1, 4, 3, 3, 4, 2, 1, 3, 2 };
            return correct
                .Zip(answers, (c, a) => c == a)
                .Count(right => right);
        }
    }
}
