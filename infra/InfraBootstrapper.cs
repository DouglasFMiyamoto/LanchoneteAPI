using Application.Repository;
using infra.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infra
{
    public static class InfraBootstrapper
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<IPedidoItemRepository, PedidoItemRepository>();
        }
    }
}
