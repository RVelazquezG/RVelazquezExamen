using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class ClaseController : Controller
    {


        [HttpGet]
        [Route("api/Clase/GetAll")]
        public IActionResult GetAll()
        {

            ML.Result result = BL.Clase.GetAll();



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
        [Route("api/Clase/Add")]
        public IActionResult Add(ML.Clase clase)
        {

            ML.Result result = BL.Clase.Add(clase);

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
        [Route("api/Clase/GetById/{IdClase}")]
        public IActionResult GetById(int IdClase)
        {
            ML.Clase clase = new ML.Clase();

            clase.Nivel = new ML.Nivel();
            ML.Result result = BL.Clase.GetById(IdClase);

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
        [Route("api/clase/Delete/{IdClase}")]
        public IActionResult Delete(int IdClase)
        {
            ML.Clase clase = new ML.Clase();
            clase.IdClase = IdClase;
            var result = BL.Ciclista.Delete(IdClase);
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
