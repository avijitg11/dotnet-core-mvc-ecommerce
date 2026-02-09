using BulkyStore_Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BulkyStore_Extensions
{
    public static class StripePaymentExtension
    {
        public static void StripePaymentInject(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<StripeSettings>(options =>
            {
                options.SecretKey = builder.Configuration["BulkyStoreSendGridSecretKey"];
                options.PublishableKey = builder.Configuration["BulkyStoreStripePublishableKey"];
            });
        }
    }
}
