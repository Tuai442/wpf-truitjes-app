using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruitjesBL.Model;

namespace TruitjesBL.Interfaces
{
    public interface IBestellingRepository
    {
        bool BestaatBestelling(Bestelling bestelling);
        void VoegBestellingToe(Bestelling bestelling);
        Bestelling GeefBestelling(int value);
        IEnumerable<Bestelling> GeefBestellingen(int? klantId, DateTime? startDatum, DateTime? eindDatum);
        bool BestaatBestelling(int value);
        void VerwijderBestelling(Bestelling bestelling);
        void UpdateBestelling(Bestelling bestelling);
    }
}
