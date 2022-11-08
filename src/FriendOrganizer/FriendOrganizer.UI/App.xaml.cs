using FriendOrganizer.DataAccess.Extensions;
using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prism.Events;
using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace FriendOrganizer.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public IServiceProvider ServiceProvider { get; set; }
        public IConfiguration Configuration { get; set; }


        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddScoped<MainViewModel>();
            services.AddScoped<INavigationViewModel, NavigationViewModel>();
            services.AddScoped<IFriendDetailViewModel, FriendDetailViewModel>();
            services.AddScoped<MainWindow>();
            services.AddScoped<IFriendDataService, FriendDataService>();

            // Important - Workaround for the limiration of .Net core DI registering the same dependency for multiple interfaces
            // 01. Register the concrete dependency explicitly first
            // 02. Then forward/delegate the requests to interfaces using the factory methods by requireing the registered dependency.
            services.AddScoped<LookupDataService>();
            services.AddScoped<IFriendLookupDataService>(sp => sp.GetRequiredService<LookupDataService>());

            // Register Prism event aggregator
            services.AddSingleton<IEventAggregator, EventAggregator>();


            services.ConfigureEntityFrameworkCore(Configuration);
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Unexpected error occured. Please inform the admin.{Environment.NewLine}Error: {e.Exception.Message}", "Unexpected Error");

            e.Handled = true;
        }
    }
}
