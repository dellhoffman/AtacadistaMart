using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Atacadista.Mart.Models
{
    public class OrcamentoRepository : IOrcamentoRepository
    {
        private static ConcurrentDictionary<int, Orcamento> _orcamentos =
              new ConcurrentDictionary<int, Orcamento>();

        public OrcamentoRepository()
        {
            Add(new Orcamento
            {
                NumOrcamento = 1,
                DtEntrega = DateTime.Now.AddDays(3),
                Valor = 700
            });
        }

        public void Add(Orcamento item)
        {
            _orcamentos[item.NumOrcamento] = item;
        }

        public Orcamento Find(int NumOrcamento)
        {
            Orcamento item;
            _orcamentos.TryGetValue(NumOrcamento, out item);
            return item;
        }

        public IEnumerable<Orcamento> GetAll()
        {
            return _orcamentos.Values;
        }

        public Orcamento Remove(int NumOrcamento)
        {
            Orcamento item;
            _orcamentos.TryGetValue(NumOrcamento, out item);
            _orcamentos.TryRemove(NumOrcamento, out item);
            return item;
        }

        public void Update(Orcamento item)
        {
            _orcamentos[item.NumOrcamento] = item;
        }

        public Orcamento GerarOrcamento(Solicitacao solicitacao)
        {
            return new Orcamento()
            {
                NumOrcamento = GetAll().Count() + 1,
                DtEntrega = DateTime.Now.AddDays(2),
                Valor = Math.Round(GerarValor(solicitacao.CodProduto, solicitacao.QtdItens), 2)
            };
        }

        public decimal GerarValor(int codProduto, int qtdItens)
        {
            if (codProduto >= 1 && codProduto <= 3)
            {
                return codProduto * qtdItens * 30;
            }
            else if (codProduto >= 4 && codProduto <= 7)
            {
                return codProduto * qtdItens * 70;
            }
            else if (codProduto >= 8 && codProduto <= 12)
            {
                return codProduto * qtdItens * 80;
            }
            else if (codProduto >= 13)
            {
                return codProduto * qtdItens * 130;
            }
            return 0;
        }
    }
}
