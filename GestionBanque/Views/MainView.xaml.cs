using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using GestionBanque.ViewModels;
using GestionBanque.Models;
using GestionBanque.Models.DataService;
using System.Windows;

namespace GestionBanque.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

            var config = new ConfigurationBuilder()
                            .AddJsonFile("autofac.json")
                            .Build();

            var module = new ConfigurationModule(config);

            var builder = new ContainerBuilder();
            builder.RegisterModule(module);

            builder.RegisterType<ClientSqliteDataService>()
                   .As<IDataService<Client>>()
                   .WithParameter("nomBd", "banque.bd")
                   .SingleInstance();

            builder.RegisterType<CompteSqliteDataService>()
                   .As<IDataService<Compte>>()
                   .WithParameter("nomBd", "banque.bd")
                   .SingleInstance();

            var container = builder.Build();
            DataContext = container.Resolve<MainViewModel>();
        }
    }
}
