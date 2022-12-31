using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class PagoRealizado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }        
        public string Observacion { get; set; }
        public decimal Importe { get; set; }
        public int CuentaPagarID { get; set; }
        public CuentaPagar CuentaPagar { get; set; }
        public DateTime FechaPago { get; set; }
    }
}
