using AnalisisMedicos.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisisMedicos.DAL
{
    public class Contexto : DbContext
    {
        public DbSet <Usuarios> usuarios  {get; set;}
        public DbSet <Analisis> analisis { get; set; }
        public DbSet <TiposAnalisis> tiposAnalisis { get; set; }


        public Contexto() : base("ConStr")
        { }
    }
}
