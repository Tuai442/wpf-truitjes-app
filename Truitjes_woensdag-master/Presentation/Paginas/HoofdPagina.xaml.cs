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

namespace Presentation.Paginas
{
    /// <summary>
    /// Interaction logic for HoofdPagina.xaml
    /// </summary>
    public partial class HoofdPagina : UserControl
    {

        public EventHandler<Page> NavigeerHanler;
        private Controller _controller; 
        public HoofdPagina(Controller controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigeerHanler.Invoke(sender, new BeheerderPagina(_controller));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigeerHanler.Invoke(sender, new BestelSelecteerTruitjePagina());

        }
    }
}
