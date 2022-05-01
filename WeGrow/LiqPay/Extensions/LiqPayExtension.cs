using WeGrow.LiqPay.Interfaces;
using WeGrow.LiqPay.Services;

namespace WeGrow.LiqPay.Extensions
{
    public static class LiqPayExtension
    {
        public static void AddLiqPay(this IServiceCollection services)
        {
            services.AddScoped<ILiqPayService, LiqPayService>();
        }
    }
}
