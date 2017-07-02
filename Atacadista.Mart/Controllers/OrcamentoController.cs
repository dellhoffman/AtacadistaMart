using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Atacadista.Mart.Models;
using System.ComponentModel.DataAnnotations;

namespace Atacadista.Mart.Controllers
{
    [Produces("application/json")]
    [Route("api/mart/[controller]")]
    public class OrcamentoController : Controller
    {
        public IOrcamentoRepository Orcamentos { get; set; }
        public IStatusPedidoRepository Pedidos { get; set; }

        public OrcamentoController(IOrcamentoRepository orcamentos, IStatusPedidoRepository pedidos)
        {
            Orcamentos = orcamentos;
            Pedidos = pedidos;
        }

        // GET: api/MartApi
        /// <summary>
        /// Listar Or�amentos
        /// </summary>
        /// <returns>Lista de Or�amentos</returns>
        [HttpGet]
        public IEnumerable<Orcamento> Get()
        {
            return Orcamentos.GetAll();
        }

        /// <summary>
        /// Obter Or�amento
        /// </summary>
        /// <param name="numOrcamento"></param>
        /// <returns>Retorna um Or�amento</returns>
        [Produces("application/json", Type = typeof(Orcamento))]
        [HttpGet("{numOrcamento}", Name = "GetOrcamento")]
        public IActionResult GetOrcamento(int numOrcamento)
        {
            var item = Orcamentos.Find(numOrcamento);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        /// <summary>
        /// Solicitar um or�amento.
        /// </summary>
        /// <remarks>
        ///  
        ///     POST /Solicitacao
        ///     {
        ///         "CodProduto": 1,         
        ///         "QtdItens": 3,
        ///         "Observacao": ""
        ///     }
        /// 
        /// </remarks>
        /// <param name="solicitacao"></param>
        /// <returns>Novo Or�amento</returns>
        /// <response code="201">Retorna um or�amento</response>
        /// <response code="400">Se a solicita��o for null</response>
        [HttpPost(Name = "SolicitarOrcamento")]
        [ProducesResponseType(typeof(Orcamento), 201)]
        [ProducesResponseType(typeof(Orcamento), 400)]
        public IActionResult Post([FromBody, Required]Solicitacao solicitacao)
        {
            if (solicitacao == null)
            {
                return BadRequest();
            }
            Orcamento orcamento = Orcamentos.GerarOrcamento(solicitacao);
            Orcamentos.Add(orcamento);
            return CreatedAtRoute("GetOrcamento", new { NumOrcamento = orcamento.NumOrcamento }, orcamento);
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Excluir Or�amento - DESATIVADO
        /// </summary>
        /// <param name="numOrcamento"></param>
        [HttpDelete("{numOrcamento}")]
        public void Delete(int numOrcamento)
        {
        }
    }
}
