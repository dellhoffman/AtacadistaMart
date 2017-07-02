using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Atacadista.Mart.Models
{
    public class RespostaClienteOrcamento
    {
        [Required]
        public int NumOrcamento { get; set; }
        [Required]
        public bool Confirmacao { get; set; }
    }
}
