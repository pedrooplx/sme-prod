using Microsoft.EntityFrameworkCore;
using sme.business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Caso o mapping de um campo string não tenha sido definido, automaticamente será definido por default como varchar(100)
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            //Adicionando mappings nas entidades
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmeDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
