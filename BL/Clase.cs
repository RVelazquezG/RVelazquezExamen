using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Clase
    {
        public static ML.Result GetAll()
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {
                    var clases = context.Clases.FromSqlRaw("GetAllClase").ToList();

                    result.Objects = new List<object>();

                    if (clases != null)
                    {
                        foreach (var obj in clases)
                        {
                            ML.Clase clase = new ML.Clase();
                            clase.IdClase = obj.IdClase;
                            clase.Nombre = obj.Nombre;

                            clase.Nivel = new ML.Nivel();
                            clase.Nivel.IdNivel = obj.IdNivel.Value;
                            clase.Nivel.NombreNivel = obj.NombreNivel;

                            clase.Horario = new ML.Horario();
                            clase.Horario.IdHorario = obj.IdNivel.Value;
                            clase.Horario.Descripcion = obj.Descripcion;

                            clase.Aula = new ML.Aula();
                            clase.Aula.IdAula = obj.IdAula.Value;
                            clase.Aula.NombreAula = obj.NombreAula;

                            result.Objects.Add(clase);
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


        public static ML.Result Add(ML.Clase clase)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {
                    var clases = context.Database.ExecuteSqlRaw($"AddClase '{clase.Nombre}','{clase.Nivel.IdNivel}','{clase.Horario.IdHorario}','{clase.Aula.IdAula}'");

                    if (clases >= 1)
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



        public static Result Update(ML.Clase clase)
        {
            Result result = new Result();
            try
            {

                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {

                    {
                        var updateResult = context.Database.ExecuteSqlRaw(($"UpdateClase '{clase.IdClase}', '{clase.Nombre}','{clase.Nivel.IdNivel}', '{clase.Horario.IdHorario}', '{clase.Aula.IdAula}'"));


                        if (updateResult >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se actualizó el registro de la clase";
                        }
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

        public static Result GetById(int IdClase)
        {
            Result result = new Result();
            try
            {

                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {

                    {
                        var obj = context.Clases.FromSqlRaw(($"GetByIdClase '{IdClase}'")).AsEnumerable().FirstOrDefault();

                        result.Objects = new List<object>();

                        if (obj != null)
                        {

                            ML.Clase clase = new ML.Clase();
                            clase.IdClase = obj.IdClase;
                            clase.Nombre = obj.Nombre;

                            clase.Nivel = new ML.Nivel();
                            clase.Nivel.IdNivel = obj.IdNivel.Value;
                            clase.Nivel.NombreNivel = obj.NombreNivel;

                            clase.Horario = new ML.Horario();
                            clase.Horario.IdHorario = obj.IdHorario.Value;
                            clase.Horario.Descripcion = obj.Descripcion;

                            clase.Aula = new ML.Aula();
                            clase.Aula.IdAula = obj.IdAula.Value;
                            clase.Aula.NombreAula = obj.NombreAula;

                            result.Object = clase;

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla clase";
                        }

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

        public static Result Delete(int IdClase)
        {
            Result result = new Result();

            try
            {
                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {

                    var query = context.Database.ExecuteSqlRaw(($"DeleteClase {IdClase}"));
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se eliminó el registro";
                    }

                    result.Correct = true;
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
