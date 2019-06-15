using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisisMedicos.Entidades
{
    public class AnalisisDetalle
    {
        [Key]
        public int AnalisisId { get; set; }
        public int TipoId { get; set; }
        public string Resultado { get; set; }

        public AnalisisDetalle()
        {
            AnalisisId = 0;
            TipoId = 0;
            Resultado = string.Empty;
        }
    }
}
