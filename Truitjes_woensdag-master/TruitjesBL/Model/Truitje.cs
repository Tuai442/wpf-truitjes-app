using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruitjesBL.Exceptions;

namespace TruitjesBL.Model
{
    public class Truitje
    {
        private int truitjeId;
        private double prijs;
        private string seizoen;
        private Club club;
        private ClubSet clubSet;

        public Truitje(double prijs, string seizoen, MaatTruitje maat, Club club, ClubSet clubSet)
        {
            Prijs = prijs;
            Seizoen = seizoen;
            Maat = maat;
            Club = club;
            ClubSet = clubSet;
        }

        public Truitje(int truitjeId, double prijs, string seizoen, MaatTruitje maat, Club club, ClubSet clubSet) 
        {
            TruitjeId = truitjeId;
            Prijs = prijs;
            Seizoen = seizoen;
            Maat = maat;
            Club = club;
            ClubSet = clubSet;
        }

        public int TruitjeId
        {
            get => truitjeId;
            set { if (value <= 0) throw new TruitjeException("TruitjeId <=0"); else truitjeId = value; }
        }
        public double Prijs
        {
            get => prijs;
            set { if (value < 0) throw new TruitjeException("prijs<0"); else prijs = value; }
        }
        public string Seizoen
        {
            get => seizoen;
            set { if (string.IsNullOrWhiteSpace(value)) throw new TruitjeException("seizoen is null"); else seizoen = value; }
        }
        public MaatTruitje Maat { get; set; }
        public Club Club
        {
            get => club;
            set { if (value == null) throw new TruitjeException("club is null"); else club = value; }
        }
        public ClubSet ClubSet
        {
            get => clubSet;
            set { if (value == null) throw new TruitjeException("clubset is null"); else clubSet = value; }
        }

        public override bool Equals(object? obj)
        {
            return obj is Truitje truitje &&
                   TruitjeId == truitje.TruitjeId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TruitjeId);
        }
    }
}
