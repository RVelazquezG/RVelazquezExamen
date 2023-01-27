using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Ciclista
    {
        public static ML.Result GetAll()
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {
                    var ciclistas = context.Ciclista.FromSqlRaw("GetAllCiclista").ToList();

                    result.Objects = new List<object>();

                    if (ciclistas != null)
                    {
                        foreach (var obj in ciclistas)
                        {
                            ML.Ciclista ciclista = new ML.Ciclista();
                            ciclista.IdCiclista = obj.IdCiclista;
                            ciclista.NombreCiclista = obj.NombreCiclista;
                            ciclista.Direccion = obj.Direccion;
                            ciclista.Edad = obj.Edad.Value;
                            ciclista.Membresia = obj.Membresia.Value;

                            ciclista.Nivel = new ML.Nivel();
                            ciclista.Nivel.IdNivel = obj.IdNivel.Value;
                            ciclista.Nivel.NombreNivel = obj.NombreNivel;


                            result.Objects.Add(ciclista);
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

        public static ML.Result Add(ML.Ciclista ciclista)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {
                    var ciclistas = context.Database.ExecuteSqlRaw($"AddCiclista '{ciclista.NombreCiclista}','{ciclista.Direccion}','{ciclista.Edad}','{ciclista.Membresia}', {ciclista.Nivel.IdNivel}");

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

        public static Result Update(ML.Ciclista ciclista)
        {
            Result result = new Result();
            try
            {

                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {

                    {
                        var updateResult = context.Database.ExecuteSqlRaw(($"UpdateCiclista '{ciclista.IdCiclista}', '{ciclista.NombreCiclista}','{ciclista.Direccion}', '{ciclista.Edad}', '{ciclista.Membresia}', '{ciclista.Nivel.IdNivel}'"));


                        if (updateResult >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se actualizó el registro del ciclista";
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

        public static Result GetById(int IdCiclista)
        {
            Result result = new Result();
            try
            {

                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {

                    {
                        var obj = context.Ciclista.FromSqlRaw(($"GetByIdCiclista '{IdCiclista}'")).AsEnumerable().FirstOrDefault();

                        result.Objects = new List<object>();

                        if (obj != null)
                        {

                            ML.Ciclista ciclista = new ML.Ciclista();
                            ciclista.IdCiclista = obj.IdCiclista;
                            ciclista.NombreCiclista = obj.NombreCiclista;
                            ciclista.Direccion = obj.Direccion;
                            ciclista.Edad = obj.Edad.Value;
                            ciclista.Membresia = obj.Membresia.Value;

                            ciclista.Nivel = new ML.Nivel();
                            ciclista.Nivel.IdNivel = obj.IdNivel.Value;
                            ciclista.Nivel.NombreNivel = obj.NombreNivel;
                            result.Object = ciclista;

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla ciclista";
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

        public static Result Delete(int IdCiclista)
        {
            Result result = new Result();

            try
            {
                using (DL.RvelazquezExamenContext context = new DL.RvelazquezExamenContext())
                {

                    var query = context.Database.ExecuteSqlRaw(($"DeleteCiclista {IdCiclista}"));
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
