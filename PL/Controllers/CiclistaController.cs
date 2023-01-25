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
                        ML.Ciclista ciclistaItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Ciclista>(resultItem.ToString());

                        //ciclistaItem.Nombre = resultItem.nombre;
                        //ciclistaItem.Direccion = resultItem.direccion;
                        //ciclistaItem.Edad = resultItem.edad;
                        //ciclistaItem.Nivel = resultItem.nivel;
                        //ciclistaItem.MembresiaActiva = resultItem.membresiaActiva;

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
    }
}
