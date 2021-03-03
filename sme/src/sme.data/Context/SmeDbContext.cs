using Microsoft.EntityFrameworkCore;
using sme.business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace sme.data.Context
{
    public class SmeDbContext : DbContext
    {
        public SmeDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}
