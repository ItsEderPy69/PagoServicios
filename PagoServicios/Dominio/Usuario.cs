using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dominio { 
	public class Usuario {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		public string nombre { get; set; }
		public string apellido { get; set; }
		public string NIS { get; set; }
		public int numero_cedula { get; set; }
	}
}