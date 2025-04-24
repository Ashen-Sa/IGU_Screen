using System.Configuration;
using System.Data;
using System.Windows;
using SPIL.IGUConfigurator;

namespace GlassConfigurator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static DatabaseService DatabaseService { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string connectionString = ConfigurationManager.ConnectionStrings["GlassConfiguratorDB"].ConnectionString;
            string connectionString2 = ConfigurationManager.ConnectionStrings["GlassConfiguratorDB"].ConnectionString;
            DatabaseService = new DatabaseService(connectionString, connectionString2);
        }
    }

}
