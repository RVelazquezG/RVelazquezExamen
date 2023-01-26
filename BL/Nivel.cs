using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Nivel
    {
        public static ML.Result GetAll()
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {
                    var niveles = context.Nivels.FromSqlRaw("GetAllNivel").ToList();

                    result.Objects = new List<object>();

                    if (niveles != null)
                    {
                        foreach (var obj in niveles)
                        {
                            ML.Nivel nivel = new ML.Nivel();
                            nivel.IdNivel = obj.IdNivel;
                            nivel.NombreNivel = obj.NombreNivel;

                            result.Objects.Add(nivel);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
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
