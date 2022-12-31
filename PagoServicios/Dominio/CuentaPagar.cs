using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class CuentaPagar
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string Concepto { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
        public decimal Importe { get; set; }
        public decimal Saldo { get; set; }
        public int ServicioID { get; set; }
        public Servicios Servicio { get; set; }
        public decimal? cuota { get; set; }
    }
}
