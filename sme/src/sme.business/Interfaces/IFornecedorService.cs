using sme.business.Models;
using System;
using System.Threading.Tasks;

namespace sme.business.Interfaces
{
    public interface IFornecedorService : IDisposable
    {
        Task Adicionar(Fornecedor fornecedor);
        Task Atualizar(Fornecedor fornecedor);
        Task AtualizarEndereco(Endereco endereco);
        Task Remover(Guid id);
    }
}
