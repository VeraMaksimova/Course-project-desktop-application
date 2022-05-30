using Microsoft.Extensions.DependencyInjection;
using Furniture_Shop.Services;
using Furniture_Shop.ViewModels;
using Furniture_Shop.Views.Pages;
using Furniture_Shop.Views.Windows;

namespace Furniture_Shop
{
    public class ViewModelLocator
    {
        private static ServiceProvider _provider;

        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddTransient<MainViewModel>();
            services.AddTransient<Page1ViewModel>();
            services.AddScoped<LogPageViewModel>();
            services.AddScoped<SuccsessPageViewModel>();
            services.AddScoped<UserMainPageViewModel>();
            services.AddScoped<AdminViewModel>();
            services.AddScoped<AdminOrdersViewModel>();
            services.AddScoped<AdminUsersViewModel>();

            services.AddSingleton<PageService>();
            services.AddSingleton<EventBus>();
            services.AddSingleton<MessageBus>();
            services.AddSingleton<SuccsessPageViewModel>();
            services.AddSingleton<UserMainPageViewModel>();
            services.AddSingleton<AdminViewModel>();
            services.AddSingleton<AdminOrdersViewModel>();
            services.AddSingleton<AdminUsersViewModel>();

            _provider = services.BuildServiceProvider();

            foreach (var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }
        }
        public AdminUsersViewModel AdminUsersViewModel => _provider.GetRequiredService<AdminUsersViewModel>();
        public AdminOrdersViewModel AdminOrdersViewModel => _provider.GetRequiredService<AdminOrdersViewModel>();
        public AdminViewModel AdminViewModel => _provider.GetRequiredService<AdminViewModel>();
        public MainViewModel MainViewModel => _provider.GetRequiredService<MainViewModel>();
        public LogPageViewModel LogPageViewModel => _provider.GetRequiredService<LogPageViewModel>();
        public Page1ViewModel Page1ViewModel => _provider.GetRequiredService<Page1ViewModel>();
        public SuccsessPageViewModel SuccsessPageViewModel => _provider.GetRequiredService<SuccsessPageViewModel>();

        public UserMainPageViewModel UserMainPageViewModel => _provider.GetRequiredService<UserMainPageViewModel>();
      
    }
}
