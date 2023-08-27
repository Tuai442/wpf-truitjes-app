using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruitjesBL.Exceptions;

namespace TruitjesBL.Model
{
    public class ClubSet
    {
        public ClubSet(bool uit, string versie)
        {
            if (string.IsNullOrWhiteSpace(versie)) throw new TruitjeException("ClubSet - versie is null");
            Uit = uit;
            Versie = versie;
        }

        public bool Uit { get;private set; }
        public string Versie { get;private set; }
    }
}
