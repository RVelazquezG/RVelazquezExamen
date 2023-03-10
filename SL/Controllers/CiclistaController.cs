using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SL.Controllers
{
    public class CiclistaController : ControllerBase
    {

        [HttpGet]
        [Route("api/Ciclista/GetAll")]
        public IActionResult GetAll()
        {


            ML.Result result = BL.Ciclista.GetAll();



            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/Ciclista/Add")]
        public IActionResult Add([FromBody]ML.Ciclista ciclista)
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

        [HttpGet]
        [Route("api/Ciclista/GetById/{IdCiclista}")]
        public IActionResult GetById(int IdCiclista)
        {
            ML.Ciclista ciclista = new ML.Ciclista();

            ciclista.Nivel = new ML.Nivel();
            ML.Result result = BL.Ciclista.GetById(IdCiclista);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("api/ciclista/Delete/{IdCiclista}")]
        public IActionResult Delete(int IdCiclista)
        {
            ML.Ciclista ciclista = new ML.Ciclista();
            ciclista.IdCiclista = IdCiclista;
            var result = BL.Ciclista.Delete(IdCiclista);
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
