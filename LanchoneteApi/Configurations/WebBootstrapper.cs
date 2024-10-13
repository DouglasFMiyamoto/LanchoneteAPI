using LanchoneteApi.Presenters;

namespace LanchoneteApi.Configurations
{
    /// <summary>
    /// Injeção de dependência da camada web
    /// </summary>
    public static class WebBootstrapper
    {
        /// <summary>
        /// Registra as dependências
        /// </summary>
        /// <param name="services"></param>
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<ICheckoutPedidoPresenter, CheckoutPedidoPresenter>();
        }
    }
}
