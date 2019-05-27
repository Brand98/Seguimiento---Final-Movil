using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Linq;

namespace FinalSeguimiento.Models
{
    [Table("registro")]
    public class Registro
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string MOVIMIENTO { get; set; }
        public string CONCEPTO { get; set; }
        public double VALOR { get; set; }
        public string OBSERVACION { get; set; }
        public DateTime FECHA { get; set; }
        public string RECURRENCIA { get; set; }
    }
}
