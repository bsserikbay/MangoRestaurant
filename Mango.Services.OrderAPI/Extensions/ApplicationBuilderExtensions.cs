using Mango.Services.OrderAPI.Messaging;

namespace Mango.Services.OrderAPI.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IAzureServiceBusConsumer? ServiceBusConsumer { get; set; }
        public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
        {
            ServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            if (hostApplicationLife != null)
            {
                hostApplicationLife.ApplicationStarted.Register(OnStart);
                hostApplicationLife.ApplicationStopped.Register(OnStop);
            }
            
            return app;
        }
        private static void OnStart()
        {
            if (ServiceBusConsumer != null) { ServiceBusConsumer.Start(); }
        }
        private static void OnStop()
        {
            if (ServiceBusConsumer != null) { ServiceBusConsumer.Stop(); }
        }
    }
}
