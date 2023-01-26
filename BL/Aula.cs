using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Aula
    {

        public static ML.Result GetAll()
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {
                    var aulas = context.Aulas.FromSqlRaw("GetAllAula").ToList();

                    result.Objects = new List<object>();

                    if (aulas != null)
                    {
                        foreach (var obj in aulas)
                        {
                            ML.Aula aula = new ML.Aula();
                            aula.IdAula = obj.IdAula;
                            aula.NombreAula = obj.NombreAula;

                            result.Objects.Add(aula);
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
