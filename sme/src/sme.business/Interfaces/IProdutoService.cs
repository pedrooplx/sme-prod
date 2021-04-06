using sme.business.Models;
using System;
using System.Threading.Tasks;

namespace sme.business.Interfaces
{
    public interface IProdutoService
    {
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Remover(Guid id);
    }
}
