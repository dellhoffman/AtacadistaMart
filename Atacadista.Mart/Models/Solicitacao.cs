using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Atacadista.Mart.Models
{
    public class Solicitacao
    {
        [Required]
        public int CodProduto { get; set; }
        [Required]
        public int QtdItens { get; set; }
        public string Observacao { get; set; }
    }
}
