using Microsoft.EntityFrameworkCore;
using sme.business.Interfaces;
using sme.business.Models;
using sme.data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sme.data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(SmeDbContext context) : base(context) { }

        public async Task<Produto> ObterProdutoFornecedor(Guid produtoId)
        {
            return await context.Produtos.AsNoTracking()
                .Include(f => f.Fornecedor) //inner join
                .FirstOrDefaultAsync(p => p.Id == produtoId);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await context.Produtos.AsNoTracking()
                .Include(f => f.Fornecedor) //inner join
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(p => p.FornecedorId == fornecedorId);
        }
    }
}
