using GRPCDemo.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace GRPCDemo.WPFUI
{

    public partial class App : Application
    {
        private readonly ServiceProvider serviceProvider;

        public App()
        {
            serviceProvider = ConfigureServices(new ServiceCollection()).BuildServiceProvider();
            SetupExceptionHandling();
        }

        private IServiceCollection ConfigureServices(ServiceCollection services)
            => services
                .AddSingleton<MainWindow>()
                .AddSingleton(new DemoClientFactory("https://localhost:5050"));
        
        protected override void OnStartup(StartupEventArgs e)
            => serviceProvider.GetRequiredService<MainWindow>().Show();

        protected override void OnExit(ExitEventArgs e) 
            => serviceProvider.Dispose();

        private void SetupExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) 
                => MessageBox.Show(e.ExceptionObject.ToString(), "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                
            Dispatcher.UnhandledException += (s, e) =>
            {
                MessageBox.Show(e.Exception.ToString(), "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                e.Handled = true;
            };

            TaskScheduler.UnobservedTaskException += (s, e) 
                => MessageBox.Show(e.Exception.ToString(), "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
        }
    }
}
