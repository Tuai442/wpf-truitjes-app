using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruitjesBL.Exceptions;

namespace TruitjesBL.Model
{
    public class Club
    {
        public Club(string competitie, string ploegNaam)
        {
            if (string.IsNullOrWhiteSpace(competitie)) throw new TruitjeException("Club - competitie is null");
            if (string.IsNullOrWhiteSpace(ploegNaam)) throw new TruitjeException("Club - ploegNaam is null");
            Competitie = competitie;
            PloegNaam = ploegNaam;
        }

        public string Competitie { get;private set; }
        public string PloegNaam { get;private set; }   
    }
}
