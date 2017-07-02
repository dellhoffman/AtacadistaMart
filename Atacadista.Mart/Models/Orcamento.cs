using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Atacadista.Mart.Models
{
    public class Orcamento
    {
        [Required]
        public int NumOrcamento { get; set; }
        [Required]
        public DateTime DtEntrega { get; set; }
        [Required]
        public decimal Valor { get; set; }
    }
}
