using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruitjesBL.Model;

namespace TruitjesUI
{
    public class TruitjeData :INotifyPropertyChanged
    {
        public TruitjeData(Truitje truitjes, int aantal)
        {
            Truitje= truitjes;
            Aantal = aantal;
        }

        public Truitje Truitje{ get; set; }
        private int aantal;
        public int Aantal
        {
            get { return aantal; }
            set
            {
                aantal = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
