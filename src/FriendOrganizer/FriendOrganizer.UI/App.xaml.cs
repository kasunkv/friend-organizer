using FriendOrganizer.UI.Data;
using FriendOrganizer.UI.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;

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
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainWindow>();
            services.AddTransient<IFriendDataService, FriendDataService>();
        }
    }
}
