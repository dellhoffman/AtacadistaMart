using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atacadista.Mart.Models
{
    public interface IStatusPedidoRepository
    {
        void Add(StatusPedido item);
        IEnumerable<StatusPedido> GetAll();
        StatusPedido Find(int NumOrcamento);
        StatusPedido Remove(int NumOrcamento);
        void Update(StatusPedido item);
        string GetStatus(int v);
        StatusPedido GerarPedido(RespostaClienteOrcamento resposta);
        bool StatusIsValid(int codStatus);
    }
}
