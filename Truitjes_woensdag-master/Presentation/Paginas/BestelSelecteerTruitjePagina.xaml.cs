using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TruitjesBL;
using TruitjesBL.Managers;
using TruitjesBL.Model;
using TruitjesDL.Repositories;

namespace Presentation.Paginas
{
    /// <summary>
    /// Interaction logic for BestelSelecteerTruitjePagina.xaml
    /// </summary>
    public partial class BestelSelecteerTruitjePagina : Page
    {
        private ObservableCollection<string> _competities;
        private ClubManager _clubManager;
        public EventHandler<Page> NavigeerHanler;
        public Truitje truitje { get; set; }
        public BestelSelecteerTruitjePagina()
        {
            InitializeComponent();
            _clubManager = new ClubManager(new ClubRepositoryADO("2022-2023", ConfigurationManager.ConnectionStrings["VerkoopDBConnection"].ToString()));

            List<string> maten = Enum.GetNames(typeof(MaatTruitje)).ToList();
            maten.Insert(0, "<alles>");
            MaatComboBox.ItemsSource = maten;
            MaatComboBox.SelectedIndex = 0;
            _competities = new ObservableCollection<string>(_clubManager.GeefCompetities());
            _competities.Insert(0, "<geen competitie>");
            CompetitieComboBox.ItemsSource = _competities;
            CompetitieComboBox.SelectedIndex = 0;
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItemUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelecteerTruitje_click(object sender, RoutedEventArgs e)
        {
            truitje = (Truitje)VoetbaltruitjesSelectie.SelectedItem;
            DialogResult = true;
            Close();
        }

        
    }
}
