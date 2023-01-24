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
            ML.Result result = new ML.Result();

            foreach (string Nombre in ciclista.Ciclistas)
            {
                {
                    ML.Ciclista ciclistaItem = new ML.Ciclista();

                    ciclistaItem.IdCiclista = ciclista.IdCiclista;


                    ML.Result resultCiclista = BL.Ciclista.Add(ciclista);

                }
            }

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
