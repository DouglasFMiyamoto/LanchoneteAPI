using Application.Repository;
using Lanchonete.infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Lanchonete.Infrastructure.Configurations
{
    public static class DependencyInjection
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
