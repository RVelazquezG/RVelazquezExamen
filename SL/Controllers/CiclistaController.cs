using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class CiclistaController : ControllerBase
    {
        [HttpPost]
        [Route("api/Ciclista/Add")]
        public IActionResult Add(ML.Ciclista ciclista)
        {

            ML.Result result = BL.Ciclista.Add(ciclista);



            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }


        }
    }
}
