using Presentation.Paginas;
using System;
using System.Collections.Generic;
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

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controller _controller;
        public MainWindow(Controller controller)
        {
            InitializeComponent();
            _controller = controller;

            HoofdPagina hoofdPagina = new HoofdPagina(controller);
            hoofdPagina.NavigeerHanler += NavigeerNaarPagina;
            HoofdFrame.Navigate(hoofdPagina);
        }

        private void NavigeerNaarPagina(object? sender, Page? page)
        {
            HoofdFrame.Navigate(page);
        }

    }
}
