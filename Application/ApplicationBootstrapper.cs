using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationBootstrapper
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IClienteUseCase, ClienteUseCase>();
            services.AddTransient<IProdutoUseCase, ProdutoUseCase>();
            services.AddTransient<IPedidoUseCase, PedidoUseCase>();
            services.AddTransient<ICheckoutPedidoUseCase, CheckoutPedidoUseCase>();
        }
    }
}
