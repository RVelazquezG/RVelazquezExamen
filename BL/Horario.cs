using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Horario
    {
        public static ML.Result GetAll()
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {
                    var horarios = context.Horarios.FromSqlRaw("GetAllHorario").ToList();

                    result.Objects = new List<object>();

                    if (horarios != null)
                    {
                        foreach (var obj in horarios)
                        {
                            ML.Horario horario = new ML.Horario();
                            horario.IdHorario = obj.IdHorario;
                            horario.Descripcion = obj.Descripcion;

                            result.Objects.Add(horario);
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
