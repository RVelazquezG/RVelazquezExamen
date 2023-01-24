using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Ciclista
    {
        public static ML.Result Add(ML.Ciclista ciclista)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {
                    var ciclistas = context.Database.ExecuteSqlRaw($"CiclistaAdd '{ciclista.Nombre}','{ciclista.Direccion}',{ciclista.Edad}, '{ciclista.Nivel}', {ciclista.MembresiaActiva}");

                    if (ciclistas >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al agregar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;

        }

    }
}
