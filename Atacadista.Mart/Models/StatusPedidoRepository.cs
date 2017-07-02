using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atacadista.Mart.Models
{
    public class StatusPedidoRepository : IStatusPedidoRepository
    {
        private static ConcurrentDictionary<int, StatusPedido> _status =
              new ConcurrentDictionary<int, StatusPedido>();

        public StatusPedidoRepository()
        {
            Add(new StatusPedido
            {
                NumOrcamento = 1,
                Status = GetStatus(1)
            });
        }

        public void Add(StatusPedido item)
        {
            _status[item.NumOrcamento] = item;
        }

        public StatusPedido Find(int NumOrcamento)
        {
            StatusPedido item;
            _status.TryGetValue(NumOrcamento, out item);
            return item;
        }

        public IEnumerable<StatusPedido> GetAll()
        {
            return _status.Values;
        }

        public StatusPedido Remove(int NumOrcamento)
        {
            StatusPedido item;
            _status.TryGetValue(NumOrcamento, out item);
            _status.TryRemove(NumOrcamento, out item);
            return item;
        }

        public void Update(StatusPedido item)
        {
            _status[item.NumOrcamento] = item;
        }

        public StatusPedido GerarPedido(RespostaClienteOrcamento resposta)
        {
            StatusPedido pedido = Find(resposta.NumOrcamento);
            if (pedido != null) { return pedido; }

            return new StatusPedido()
            {
                NumOrcamento = resposta.NumOrcamento,
                Status = GetStatus(1)
            };
        }

        public string GetStatus(int v)
        {
            switch (v)
            {
                case 0: return "Cancelado/Sem Status";
                case 1: return "Solicitado";
                case 2: return "Em fabricação";
                case 3: return "Finalizado";
                case 4: return "Despachado";

                default: return "Cancelado/Sem Status";
            }
        }

        public bool StatusIsValid(int codStatus)
        {
            return 0 >= codStatus && 4 <= codStatus;
        }
    }
}
