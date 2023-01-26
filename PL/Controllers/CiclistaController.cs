using BL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace PL.Controllers
{
    public class CiclistaController : Controller
    {
        public IActionResult GetAllCiclista()
        {
            ML.Ciclista ciclista = new ML.Ciclista();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("https://ciclistastest.azurewebsites.net/");
                var responseTask = cliente.GetAsync("Ciclistas");
                responseTask.Wait();

                var resultServicio = responseTask.Result;

                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsStringAsync();
                    dynamic resultJSON = JArray.Parse(readTask.Result.ToString());
                    readTask.Wait();

                    ciclista.Ciclistas = new List<object>();

                    foreach(var resultItem in resultJSON)
                    {
                        ML.Ciclista ciclistaItem = new ML.Ciclista();

                        ciclistaItem.NombreCiclista = resultItem.nombre;
                        ciclistaItem.Direccion = resultItem.direccion;
                        ciclistaItem.Edad = resultItem.edad;
                        ciclistaItem.Nivele = resultItem.nivel;
                        ciclistaItem.Membresia = resultItem.membresiaActiva;

                        ciclista.Ciclistas.Add(ciclistaItem);
                    }
                }
            }
           
            return View(ciclista);
        }

        public IActionResult Add(ML.Ciclista ciclista)
        {
            ML.Result result = new ML.Result();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("http://localhost:5215/");
                var responseTask = cliente.PostAsJsonAsync("api/Ciclista/Add", ciclista);
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
            ML.Ciclista ciclista = new ML.Ciclista();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("http://localhost:5215/");
                var responseTask = cliente.GetAsync("api/Ciclista/GetAll");
                responseTask.Wait();

                var resultServicio = responseTask.Result;
                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    result.Objects = new List<object>();
                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Ciclista resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Ciclista>(resultItem.ToString());
                        result.Objects.Add(resultItemList);

                    }
                }
                ciclista.Ciclistas = result.Objects;
            }
            return View(ciclista);
        }

        [HttpGet]
        public ActionResult Form(int? IdCiclista)
        {

            ML.Ciclista ciclista = new ML.Ciclista();
            ciclista.Nivel = new ML.Nivel();

            ML.Result resultNivel = BL.Nivel.GetAll();


            if (IdCiclista == null)
            {
                ciclista.Nivel.Niveles = resultNivel.Objects;
                return View(ciclista);
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

                        var responseTask = client.GetAsync($"api/Ciclista/GetById/{IdCiclista}");

                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();
                            ML.Ciclista resultItemList = new ML.Ciclista();

                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Ciclista>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en la tabla Ciclista";
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

                    ciclista = (ML.Ciclista)result.Object;
                    ciclista.Nivel.Niveles = resultNivel.Objects;
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar el ciclista seleccionado";
                }

                return View(ciclista);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Ciclista ciclista)
        {

            if (ciclista.IdCiclista == 0)
            {
                ML.Result result = BL.Ciclista.Add(ciclista);
                if (result.Correct)
                {
                    ciclista = (ML.Ciclista)result.Object;
                    ViewBag.Message = " El ciclista ha sido agregado con exito";
                    return PartialView("Modal");
                }
                else
                {
                    ciclista.Nivel = new ML.Nivel();

                    ML.Result resultNivel = BL.Nivel.GetAll();

                    ciclista.Nivel.Niveles = resultNivel.Objects;
                    return View(ciclista);
                }

            }
            else
            {

                ML.Result result = BL.Ciclista.Update(ciclista);
                if (result.Correct)
                {
                    ciclista = (ML.Ciclista)result.Object;
                    ViewBag.Mensaje = "El ciclista seleccionado ha sido actualizado con exito";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al actualizar el ciclista seleccionado";
                    return PartialView("Modal");
                }
            }

            return View(ciclista);
        }

        [HttpGet]
        public ActionResult Delete(int IdCiclista)
        {
            ML.Result reesult = new ML.Result();

            using (var client = new HttpClient())
            {
                //HttpPost
                client.BaseAddress = new Uri("http://localhost:5215/");
                var postTask = client.DeleteAsync($"api/ciclista/Delete/{IdCiclista}");
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    ViewBag.Message = "El ciclista ha sido eliminado";
                }
                else
                {
                    ViewBag.Message = "El ciclista no pudo ser eliminado";
                }
            }

            return PartialView("Modal");
        }

    }

}

