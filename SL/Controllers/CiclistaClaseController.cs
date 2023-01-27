using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class CiclistaClaseController : Controller
    {

        [HttpGet]
        [Route("api/CiclistaClase/GetAll")]
        public IActionResult GetAll()
        {
            ML.CiclistaClase ciclistaClase = new ML.CiclistaClase();
            ciclistaClase.Clase = new ML.Clase();
            ciclistaClase.Ciclista = new ML.Ciclista();

            ML.Result result = BL.CiclistaClase.GetAll(ciclistaClase);

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
        [Route("api/CiclistaClase/Add")]
        public IActionResult Add(ML.CiclistaClase ciclistaClase)
        {

            ML.Result result = BL.CiclistaClase.Add(ciclistaClase);

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
        [Route("api/CiclistaClase/GetById/{IdRelacion}")]
        public IActionResult GetById(int IdRelacion)
        {
            ML.Ciclista ciclista = new ML.Ciclista();

            ciclista.Nivel = new ML.Nivel();
            ML.Result result = BL.CiclistaClase.GetById(IdRelacion);

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
        [Route("api/ciclistaClase/Delete/{IdRelacion}")]
        public IActionResult Delete(int IdRelacion)
        {
            ML.CiclistaClase ciclistaClase = new ML.CiclistaClase();
            ciclistaClase.IdRelacion = IdRelacion;
            var result = BL.CiclistaClase.Delete(IdRelacion);
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
