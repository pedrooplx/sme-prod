using sme.business.Interfaces;
using sme.business.Models;
using sme.business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace sme.business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        public async Task Adicionar(Fornecedor fornecedor)
        {
            //Validar o estado da entidade
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor) 
                && !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return;

            //Validar se não existe fornecedor com o mesmo documento
            return;
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
