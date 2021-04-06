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
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

            //Validar se não existe fornecedor com o mesmo documento
            return;
        }

        public Task Atualizar(Fornecedor fornecedor)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarEndereco(Endereco endereco)
        {
            throw new NotImplementedException();
        }
    }
}
