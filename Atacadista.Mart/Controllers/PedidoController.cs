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
    [Route("api/mart/pedido")]
    public class PedidoController : Controller
    {
        public IOrcamentoRepository Orcamentos { get; set; }
        public IStatusPedidoRepository Pedidos { get; set; }

        public PedidoController(IOrcamentoRepository orcamentos, IStatusPedidoRepository pedidos)
        {
            Orcamentos = orcamentos;
            Pedidos = pedidos;
        }

        // GET: api/MartApi
        /// <summary>
        /// Listar todos os pedidos
        /// </summary>
        /// <returns>Lista de Pedidos </returns>
        [HttpGet]
        public IEnumerable<StatusPedido> Get()
        {
            return Pedidos.GetAll();
        }

        // GET: api/MartApi/5
        /// <summary>
        /// Obter Pedido
        /// </summary>
        /// <param name="numOrcamento"></param>
        /// <returns>Retorna o Pedido</returns>
        [Produces("application/json", Type = typeof(StatusPedido))]
        [HttpGet("{numOrcamento}", Name = "GetPedido")]
        public IActionResult Get(int numOrcamento)
        {
            var item = Pedidos.Find(numOrcamento);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        /// <summary>
        /// Atualizar Pedido
        /// </summary>
        /// <remarks>
        ///  
        ///     POST /Atualizar Status do Pedido
        ///     {
        ///        Código = 0: atualiza como "Cancelado/Sem Status";
        ///        Código = 1: atualiza como "Solicitado";
        ///        Código = 2: atualiza como "Em fabricação";
        ///        Código = 3: atualiza como "Finalizado";
        ///        Código = 4: atualiza como "Despachado";
        ///     }
        /// 
        /// </remarks>
        /// <param name="numOrcamento"></param>
        /// <param name="codStatus"></param>
        /// <returns>Status do Pedido</returns>
        /// <response code="201">Retorna o status do pedido</response>
        /// <response code="404">Se o Pedido não existir</response>      
        [HttpPut("{numOrcamento}, {codStatus}", Name = "Atualizar")]
        [ProducesResponseType(typeof(StatusPedido), 201)]
        [ProducesResponseType(typeof(StatusPedido), 404)]
        public IActionResult Atualizar(int numOrcamento, int codStatus)
        {
            StatusPedido pedido = Pedidos.Find(numOrcamento);

            if (pedido == null)
            {
                return NotFound();
            }
            if (!Pedidos.StatusIsValid(codStatus))
            {
                return NotFound(codStatus);
            }
            pedido.Status = Pedidos.GetStatus(codStatus);

            return CreatedAtRoute("GetPedido", new { NumOrcamento = pedido.NumOrcamento }, pedido);
        }

        /// <summary>
        /// Finalizar Pedido
        /// </summary>
        /// <remarks>
        ///  
        ///     POST /Resposta
        ///     {
        ///         "NumOrcamento": 1,         
        ///         "Confirmacao": true
        ///     }
        /// 
        /// </remarks>
        /// <param name="resposta"></param>
        /// <returns>Status do Pedido</returns>
        /// <response code="200">Pedido finalizado</response>
        /// <response code="201">Retorna o status do pedido</response>
        /// <response code="400">Se a resposta for null</response>
        /// <response code="404">Se o orçameno não existir</response>      
        [HttpPut(Name = "FinalizarPedido")]
        [ProducesResponseType(typeof(StatusPedido), 200)]
        [ProducesResponseType(typeof(StatusPedido), 201)]
        [ProducesResponseType(typeof(StatusPedido), 400)]
        [ProducesResponseType(typeof(StatusPedido), 404)]
        public IActionResult FinalizarPedido([FromBody, Required]RespostaClienteOrcamento resposta)
        {
            if (resposta == null)
            {
                return BadRequest();
            }

            var item = Orcamentos.Find(resposta.NumOrcamento);
            if (item == null)
            {
                return NotFound();
            }
            StatusPedido pedido = Pedidos.Find(resposta.NumOrcamento);
            if (resposta.Confirmacao)
            {
                pedido = Pedidos.GerarPedido(resposta);
                Pedidos.Add(pedido);
                return CreatedAtRoute("GetPedido", new { NumOrcamento = pedido.NumOrcamento }, pedido);
            }
            else if (pedido != null)
            {
                pedido.Status = Pedidos.GetStatus(0);
            }
            return new OkResult();
        }


        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Excluir Pedido - DESATIVADO
        /// </summary>
        /// <param name="numOrcamento">NumOrcamento</param>
        [HttpDelete("{numOrcamento}")]
        public void Delete(int numOrcamento)
        {
        }
    }
}
