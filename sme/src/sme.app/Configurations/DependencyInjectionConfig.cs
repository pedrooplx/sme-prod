using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using sme.app.Extentions;
using sme.business.Interfaces;
using sme.business.Notifications;
using sme.business.Services;
using sme.data.Context;
using sme.data.Repository;

namespace sme.app.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<SmeDbContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttribuiteAdapterProvider>(); //Adicionando atributo personalizado no client-side

            //Regra de negocio
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            return services;
        }
    }
}