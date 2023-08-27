using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TruitjesBL;
using TruitjesBL.Interfaces;
using TruitjesBL.Model;
using TruitjesDL.Repositories;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Tuur\Documents\Gegevens\BACK_UP_LAPTOP\hogent\Programmeren gevorderd\Truitjes_woensdag-master\Truitjes_woensdag-master\Databank\Database.mdf"";Integrated Security=True";
        private ITruitjeRepository _truitjeRepo;
        private IKlantRepository _klantRepo;
        private IBestellingRepository _bestellingRepo;
        private IClubRepository _clubRepository;
        
        private Controller _controller;
        private MainWindow _mainWindow;
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            _truitjeRepo = new TruitjeRepositoryADO(_connectionString);
            _klantRepo = new KlantRepositoryADO(_connectionString);
            _clubRepository = new ClubRepositoryADO("", _connectionString);

            _controller = new Controller(_bestellingRepo, _clubRepository,
                _klantRepo, _truitjeRepo);

            _mainWindow = new MainWindow(_controller);
            _mainWindow.Show();
        }


    }
}
