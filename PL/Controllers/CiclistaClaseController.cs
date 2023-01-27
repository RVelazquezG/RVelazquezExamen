using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
{
    public class CiclistaClaseController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.CiclistaClase ciclistaClase = new ML.CiclistaClase();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("http://localhost:5215/");
                var responseTask = cliente.GetAsync("api/CiclistaClase/GetAll");
                responseTask.Wait();

                var resultServicio = responseTask.Result;
                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    result.Objects = new List<object>();
                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.CiclistaClase resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.CiclistaClase>(resultItem.ToString());
                        result.Objects.Add(resultItemList);

                    }
                }
                ciclistaClase.CiclistaClases = result.Objects;
            }
            return View(ciclistaClase);
        }

        [HttpPost]
        public ActionResult GetAll(ML.CiclistaClase ciclistaClase)
        {

            ML.Result result = new ML.Result();
            ML.Result resultClase = BL.Clase.GetAll();


            result = BL.CiclistaClase.GetAll(ciclistaClase);

            if (result.Correct)
            {
                ciclistaClase.CiclistaClases = result.Objects;
                ciclistaClase.Clase.Clases = resultClase.Objects;
                return View(ciclistaClase);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Form(int? IdRelacion)
        {

            ML.CiclistaClase ciclistaClase = new ML.CiclistaClase();
            ciclistaClase.Clase = new ML.Clase();
            ciclistaClase.Ciclista = new ML.Ciclista();

            ML.Result resultClase = BL.Clase.GetAll();
            ML.Result resultCiclista = BL.Ciclista.GetAll();

            if (IdRelacion == null)
            {
                ciclistaClase.Clase.Clases = resultClase.Objects;
                //ciclistaClase.Ciclista.Ciclistas = new List<ML.Ciclista>((IEnumerable<ML.Ciclista>)(resultCiclista.Objects));
                ciclistaClase.Ciclista.CiclistasBase = resultCiclista.Objects.ToList();
                return View(ciclistaClase);
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

                        var responseTask = client.GetAsync($"api/CiclistaClase/GetById/{IdRelacion}");

                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();
                            ML.CiclistaClase resultItemList = new ML.CiclistaClase();

                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.CiclistaClase>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en la tabla CiclistaClase";
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

                    ciclistaClase = (ML.CiclistaClase)result.Object; 
                    ciclistaClase.Clase.Clases = resultClase.Objects;
                    ciclistaClase.Ciclista.Ciclistas = new List<ML.Ciclista>((IEnumerable<ML.Ciclista>)(result.Objects));
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar el ciclistaClase seleccionado";
                }

                return View(ciclistaClase);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.CiclistaClase ciclistaClase)
        {

            if (ciclistaClase.IdRelacion == 0)
            {
                ML.Result result = BL.CiclistaClase.Add(ciclistaClase);
                if (result.Correct)
                {
                    ciclistaClase = (ML.CiclistaClase)result.Object;
                    ViewBag.Message = " El ciclista ha sido asignado a la clase con exito";
                    return PartialView("Modal");
                }
                else
                {
                    ciclistaClase.Clase = new ML.Clase();
                    ciclistaClase.Ciclista = new ML.Ciclista();

                    ML.Result resultClase = BL.Clase.GetAll();
                    ML.Result resultCiclista = BL.Ciclista.GetAll();

                    ciclistaClase.Clase.Clases = resultClase.Objects;
                    ciclistaClase.Ciclista.Ciclistas = new List<ML.Ciclista>((IEnumerable<ML.Ciclista>)(result.Objects));
                    return View(ciclistaClase);
                }

            }
            else
            {

                ML.Result result = BL.CiclistaClase.Update(ciclistaClase);
                if (result.Correct)
                {
                    ciclistaClase = (ML.CiclistaClase)result.Object;
                    ViewBag.Mensaje = "El registro seleccionado ha sido actualizado con exito";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al actualizar el registro seleccionado";
                    return PartialView("Modal");
                }
            }

            return View(ciclistaClase);
        }

        [HttpGet]
        public ActionResult Delete(int IdRelacion)
        {

            using (var client = new HttpClient())
            {
                //HttpPost
                client.BaseAddress = new Uri("http://localhost:5215/");
                var postTask = client.DeleteAsync($"api/ciclistaClase/Delete/{IdRelacion}");
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {

                    ViewBag.Message = "El registro ha sido eliminado";
                }
                else
                {
                    ViewBag.Message = "El registro no pudo ser eliminado";
                }
            }

            return PartialView("Modal");
        }
    }
}
