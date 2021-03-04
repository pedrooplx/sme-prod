using Microsoft.EntityFrameworkCore;
using sme.business.Interfaces;
using sme.business.Models;
using sme.data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace sme.data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(SmeDbContext context) : base(context) { }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await context.Fornecedores.AsNoTracking()
                .Include(f => f.Endereco)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await context.Fornecedores.AsNoTracking()
                .Include(f => f.Endereco)
                .Include(f => f.Produto)
                .FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
