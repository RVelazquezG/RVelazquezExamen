using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ClaseController : Controller
    {
        public IActionResult Add(ML.Clase clase)
        {
            ML.Result result = new ML.Result();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("http://localhost:5215/");
                var responseTask = cliente.PostAsJsonAsync("api/Clase/Add", clase);
                responseTask.Wait();

                var resultServicio = responseTask.Result;

                if (resultServicio.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se agrego el registro";
                }
                else
                {
                    ViewBag.Message = "No se agrego el registro";
                }

                return PartialView("Modal");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Clase clase = new ML.Clase();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("http://localhost:5215/");
                var responseTask = cliente.GetAsync("api/Clase/GetAll");
                responseTask.Wait();

                var resultServicio = responseTask.Result;
                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    result.Objects = new List<object>();
                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Clase resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Clase>(resultItem.ToString());
                        result.Objects.Add(resultItemList);

                    }
                }
                clase.Clases = result.Objects;
            }
            return View(clase);
        }

        [HttpGet]
        public ActionResult Form(int? IdClase)
        {

            ML.Clase clase = new ML.Clase();
            clase.Nivel = new ML.Nivel();
            clase.Horario = new ML.Horario();
            clase.Aula = new ML.Aula();

            ML.Result resultNivel = BL.Nivel.GetAll();
            ML.Result resultHorario = BL.Horario.GetAll();
            ML.Result resultAula = BL.Aula.GetAll();


            if (IdClase == null)
            {
                clase.Nivel.Niveles = resultNivel.Objects;
                clase.Horario.Horarios = resultHorario.Objects;
                clase.Aula.Aulas = resultAula.Objects;
                return View(clase);
            }
            else
            {
                ML.Result result = new ML.Result();
                try
                {
                    string urlAPI = ("http://localhost:5215/");
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(urlAPI);

                        var responseTask = client.GetAsync($"api/Clase/GetById/{IdClase}");

                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();
                            ML.Clase resultItemList = new ML.Clase();

                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Clase>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en la tabla Clase";
                        }

                    }
                }

                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;

                }
                if (result.Correct)
                {

                    clase = (ML.Clase)result.Object;
                    clase.Nivel.Niveles = resultNivel.Objects;
                    clase.Horario.Horarios = resultHorario.Objects;
                    clase.Aula.Aulas = resultAula.Objects;
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar la clase seleccionada";
                }

                return View(clase);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Clase clase)
        {

            if (clase.IdClase == 0)
            {
                ML.Result result = BL.Clase.Add(clase);
                if (result.Correct)
                {
                    clase = (ML.Clase)result.Object;
                    ViewBag.Message = "La clase ha sido agregado con exito";
                    return PartialView("Modal");
                }
                else
                {
                    clase.Nivel = new ML.Nivel();

                    ML.Result resultNivel = BL.Nivel.GetAll();
                    ML.Result resultHorario = BL.Horario.GetAll();
                    ML.Result resultAula = BL.Aula.GetAll();

                    clase.Nivel.Niveles = resultNivel.Objects;
                    clase.Horario.Horarios = resultHorario.Objects;
                    clase.Aula.Aulas = resultAula.Objects;
                    return View(clase);
                }

            }
            else
            {

                ML.Result result = BL.Clase.Update(clase);
                if (result.Correct)
                {
                    clase = (ML.Clase)result.Object;
                    ViewBag.Message = "La clase seleccionado ha sido actualizada con exito";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al actualizar la clase seleccionada";
                    return PartialView("Modal");
                }
            }

            return View(clase);
        }

        [HttpGet]
        public ActionResult Delete(int IdClase)
        {
            ML.Result reesult = new ML.Result();

            using (var client = new HttpClient())
            {
                //HttpPost
                client.BaseAddress = new Uri("http://localhost:5215/");
                var postTask = client.DeleteAsync($"api/clase/Delete/{IdClase}");
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    ViewBag.Message = "La clase ha sido eliminada";
                }
                else
                {
                    ViewBag.Message = "La clase no pudo ser eliminado";
                }
            }

            return PartialView("Modal");
        }
    }
}
