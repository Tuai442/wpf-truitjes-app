using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruitjesBL.Model;

namespace TruitjesBL.Interfaces
{
    public interface IKlantRepository
    {
        bool BestaatKlant(Klant klant);
        bool BestaatKlant(int value);
        void VoegKlantToe(Klant klant);
        void VerwijderKlant(Klant klant);
        Klant GeefKlant(int klantId);
        void UpdateKlant(Klant klant);
        IEnumerable<Klant> GeefKlanten(string naam, string adres);
        
    }
}
