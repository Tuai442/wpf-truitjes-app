using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Numerics;
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
using TruitjesUI;

namespace Presentation.Paginas
{
    /// <summary>
    /// Interaction logic for BeheerderPagina.xaml
    /// </summary>
    public partial class BeheerderPagina : Page
    {
        private Controller _controller;
        private BestellingManager _bestellingManager;
        private KlantManager _klantManager;
        private ObservableCollection<TruitjeData> truitjes = new ObservableCollection<TruitjeData>();
        private Bestelling _bestelling;

        public EventHandler<Page> NavigeerHanler;

        public BeheerderPagina(Controller controller)
        {
            InitializeComponent();
            _controller = controller;
            _bestellingManager = _controller.GeefBestellingManager();
            _klantManager = _controller.GeefKlantManager();
            _bestelling = new Bestelling();
            BestellingTruitjes.ItemsSource = truitjes;
        }

      

        private void SelecteerKlantButton_Click(object sender, RoutedEventArgs e)
        {
            //string text = KlantTextBox.Text;
        }

        private void SelecteerTruitjeButton_Click(object sender, RoutedEventArgs e)
        {
            BestelSelecteerTruitjePagina bestelSelecteerTruitjePagina = new BestelSelecteerTruitjePagina();
            bestelSelecteerTruitjePagina.NavigeerHanler += NavigeerNaarPagina;
            NavigeerHanler.Invoke(sender, bestelSelecteerTruitjePagina);

            //if (w.ShowDialog() == true)
            //{
            //    _bestelling.VoegTruitjeToe(w.truitje, 1);
            //    TruitjeData td = new TruitjeData(w.truitje, 1);
            //    td.PropertyChanged += TruitjeData_PropertyChanged;
            //    truitjes.Add(td);
            //    PrijsTextBox.Text = _bestelling.KostPrijs().ToString(); ;
            //}
        }

        private void TruitjeData_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ZoekBestellingButton_Click(object sender, RoutedEventArgs e)
        {
            int? bestellingId = null;
            int? klantId = null;
            DateTime? startDatum = null;
            DateTime? eindDatum = null;

            if (!string.IsNullOrEmpty(BestellingIdTextBox.Text)){
                int intOut;
                bool gelukt = int.TryParse(BestellingIdTextBox.Text, out intOut);
                if (!gelukt)
                {
                    bestellingId = intOut;
                    MessageBox.Show($"Kan het teken {BestellingIdTextBox.Text} niet naar een id omzetten.");
                }
            }
            Klant klant = GeefKlantUitTextBox();
            if (klant is not null)
            {
                klantId = klant.KlantId;
            }
            if (!string.IsNullOrEmpty(StartDatumDatePicker.Text))
            {
                startDatum = DateTime.ParseExact(StartDatumDatePicker.Text,
                    "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(EindDatumDatePicker.Text))
            {
                eindDatum = DateTime.ParseExact(StartDatumDatePicker.Text,
                    "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            //TODO: optionele parameters kunnen weg
            IReadOnlyList<Bestelling> bestellingen =
                _bestellingManager.ZoekBestellingen(bestellingId, klantId, startDatum, eindDatum);
            BestellingenAanpassenListBox.ItemsSource = bestellingen;


        }

        private void SelecteerKlantAanpassenButton_Click(object sender, RoutedEventArgs e)
        {
            Klant klant = GeefKlantUitTextBox();
            if (klant is not null)
            {
                KlantAanpassenTextBox.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                KlantAanpassenTextBox.Foreground = new SolidColorBrush(Colors.Red);
                MessageBox.Show("Kan de klant niet vinden");
            }
            
            
        }

        private Klant GeefKlantUitTextBox()
        {
            Klant klant = null;
            string[] input = KlantAanpassenTextBox.Text.Split(",");
            if(input.Length == 2)
            {
                string naam = input[0];
                string adres = input[1];
                //TODO: optionele params moete weg.
                IReadOnlyList<Klant> klanten =
                    _klantManager.ZoekKlanten(0, naam: naam, adres: adres);

                if (klanten.Count > 0)
                {
                    klant = klanten[0];
                } 
            }
            return klant;


        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlaatsBestellingButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void NavigeerNaarPagina(object? sender, Page? page)
        {
            NavigeerHanler.Invoke(sender, page);
        }
    }
}
