using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CiclistaClase
    {
        public static ML.Result GetAll(ML.CiclistaClase ciclistaClase)
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {
                    var ciclistaClases = context.CiclistaClases.FromSqlRaw($"GetAllCiclistaClase '{ciclistaClase.Ciclista.NombreCiclista}','{ciclistaClase.Clase.Nombre}'").ToList();

                    result.Objects = new List<object>();

                    if (ciclistaClases != null)
                    {
                        foreach (var obj in ciclistaClases)
                        {
                            ML.CiclistaClase cClase = new ML.CiclistaClase();
                            cClase.IdRelacion = obj.IdRelacion;

                            cClase.Ciclista = new ML.Ciclista();
                            cClase.Ciclista.IdCiclista = obj.IdCiclista.Value;
                            cClase.Ciclista.NombreCiclista = obj.NombreCiclista;

                            cClase.Clase = new ML.Clase();
                            cClase.Clase.IdClase = obj.IdClase.Value;
                            cClase.Clase.Nombre = obj.Nombre;

                            cClase.Clase.Aula = new ML.Aula();
                            cClase.Clase.Aula.IdAula = obj.IdAula.Value;
                            cClase.Clase.Aula.NombreAula = obj.NombreAula;

                            cClase.Clase.Horario = new ML.Horario();
                            cClase.Clase.Horario.IdHorario = obj.IdHorario.Value;
                            cClase.Clase.Horario.Descripcion = obj.Descripcion;

                            result.Objects.Add(cClase);
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

        public static ML.Result Add(ML.CiclistaClase ciclistaClase)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {
                    var ciclistas = context.Database.ExecuteSqlRaw($"AddCiclistaClase '{ciclistaClase.Ciclista.IdCiclista}','{ciclistaClase.Clase.IdClase}'");

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

        public static Result Update(ML.CiclistaClase ciclistaClase)
        {
            Result result = new Result();
            try
            {

                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {

                    {
                        var updateResult = context.Database.ExecuteSqlRaw(($"UpdateCiclistaClase '{ciclistaClase.IdRelacion}', '{ciclistaClase.Ciclista.IdCiclista}','{ciclistaClase.Clase.IdClase}'"));


                        if (updateResult >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se actualizó el registro";
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

        public static Result GetById(int IdRelacion)
        {
            Result result = new Result();
            try
            {

                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {

                    {
                        var obj = context.CiclistaClases.FromSqlRaw(($"GetByIdCiclistaClase '{IdRelacion}'")).AsEnumerable().FirstOrDefault();

                        result.Objects = new List<object>();

                        if (obj != null)
                        {

                            ML.CiclistaClase cClase = new ML.CiclistaClase();
                            cClase.IdRelacion = obj.IdRelacion;

                            cClase.Ciclista = new ML.Ciclista();
                            cClase.Ciclista.IdCiclista = obj.IdCiclista.Value;
                            cClase.Ciclista.NombreCiclista = obj.NombreCiclista;

                            cClase.Clase = new ML.Clase();
                            cClase.Clase.IdClase = obj.IdClase.Value;
                            cClase.Clase.Nombre = obj.Nombre;

                            cClase.Clase.Aula = new ML.Aula();
                            cClase.Clase.Aula.IdAula = obj.IdAula.Value;
                            cClase.Clase.Aula.NombreAula = obj.NombreAula;

                            cClase.Clase.Horario = new ML.Horario();
                            cClase.Clase.Horario.IdHorario = obj.IdHorario.Value;
                            cClase.Clase.Horario.Descripcion = obj.Descripcion;
                            result.Object = cClase;

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla";
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

        public static Result Delete(int IdRelacion)
        {
            Result result = new Result();

            try
            {
                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {

                    var query = context.Database.ExecuteSqlRaw(($"DeleteCiclistaClase {IdRelacion}"));
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
