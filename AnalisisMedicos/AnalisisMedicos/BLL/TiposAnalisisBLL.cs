using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AnalisisMedicos.DAL;
using AnalisisMedicos.Entidades;

namespace AnalisisMedicos.BLL
{
    public class TiposAnalisisBLL
    {
        public static bool Guardar(TiposAnalisis tiposAnalisis)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.tiposAnalisis.Add(tiposAnalisis) != null)
                {
                    paso = db.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Modificar(TiposAnalisis tiposAnalisis)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Entry(tiposAnalisis).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.tiposAnalisis.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;

                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }


        public static TiposAnalisis Buscar(int id)
        {
            Contexto db = new Contexto();
            TiposAnalisis tiposAnalisis;

            try
            {
                tiposAnalisis = db.tiposAnalisis.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return tiposAnalisis;
        }

        public static List<TiposAnalisis> GetList(Expression<Func<TiposAnalisis, bool>> tiposAnalisis)
        {
            List<TiposAnalisis> Lista = new List<TiposAnalisis>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.tiposAnalisis.Where(tiposAnalisis).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return Lista;
        }
    }
}
