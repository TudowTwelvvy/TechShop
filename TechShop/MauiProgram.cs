using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TechShop.Data;
using TechShop.ViewModels;
using TechShop.Views;

namespace TechShop
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Register Views
            builder.Services.AddTransient<HomeView>();
            builder.Services.AddTransient<AccountView>();
            builder.Services.AddTransient<MenuView>();
            builder.Services.AddTransient<ShoppingCartView>();
            builder.Services.AddTransient<ProductDescriptionView>();

            // Register ViewModels
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<AccountViewModel>();
            builder.Services.AddTransient<MenuViewModel>();
            builder.Services.AddTransient<ShoppingViewModel>();
            builder.Services.AddTransient<ShoppingViewModel>();

            //Database Context
            builder.Services.AddSingleton<DatabaseContext>();

            return builder.Build();
        }
    }
}
