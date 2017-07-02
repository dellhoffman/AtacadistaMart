using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atacadista.Mart.Models
{
    public interface IOrcamentoRepository
    {
        void Add(Orcamento item);
        IEnumerable<Orcamento> GetAll();
        Orcamento Find(int NumOrcamento);
        Orcamento Remove(int NumOrcamento);
        void Update(Orcamento item);
        Orcamento GerarOrcamento(Solicitacao solicitacao);
        decimal GerarValor(int codProduto, int qtdItens);

    }
}
