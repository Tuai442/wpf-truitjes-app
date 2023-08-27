using TruitjesBL.Exceptions;

namespace TruitjesBL.Model
{
    public class Klant
    {
        private List<Bestelling> _bestellingen = new List<Bestelling>();
        private int klantId;
        private string naam;
        private string adres;
        private const int adresLengte = 5;
        private const int p10 = 5;
        private const int p20 = 10;

        public Klant(int klantId, string naam, string adres)
        {
            KlantId = klantId;
            Naam = naam;
            Adres = adres;
        }

        public Klant(List<Bestelling> bestellingen, int klantId, string naam, string adres)
        {
            _bestellingen = bestellingen;
            KlantId = klantId;
            Naam = naam;
            Adres = adres;
        }

        public int KlantId
        {
            get => klantId;
            set { if (value > 0) klantId = value; else throw new KLantException("klantid not valid"); }
        }
        public string Naam
        {
            get => naam;
            set { if (string.IsNullOrWhiteSpace(value)) throw new KLantException("naam not valid"); else naam = value; }
        }
        public string Adres
        {
            get => adres;
            set { if (value.Length >= adresLengte) adres = value; else throw new KLantException("adres not valid"); }
        }
        public virtual double Korting() { //geen procent, maar wel decimaal
            if (_bestellingen.Count > p20) return 0.2;
            else if (_bestellingen.Count > p10) return 0.1;
            else return 0.0;
            } 
        public IReadOnlyList<Bestelling> Bestellingen()
        {
            return _bestellingen.AsReadOnly();
        }
        public void VoegBestellingToe(Bestelling bestelling)
        {
            if (bestelling==null) throw new KLantException("bestelling is null");
            if (!_bestellingen.Contains(bestelling)) //TODO equals implementeren bestelling
            {
                _bestellingen.Add(bestelling);
                if (bestelling.Klant!=this)
                    bestelling.ZetKlant(this);
            }
            else throw new KLantException("bestelling bestaat reeds");
        }
        public void VerwijderBestelling(Bestelling bestelling)
        {
            if (bestelling == null) throw new KLantException("bestelling is null");
            if (_bestellingen.Contains(bestelling)) 
            {
                if (bestelling.Klant==this)
                    bestelling.VerwijderKlant();
                _bestellingen.Remove(bestelling);
                
            }
            else throw new KLantException("bestelling bestaat niet");
        }
        public bool HeeftBestelling(Bestelling bestelling)
        {
            return _bestellingen.Contains(bestelling);
        }
        public bool HeeftZelfdeProperties(Klant andereKlant)
        {
            if (andereKlant == null) throw new KLantException("HeeftZelfdeProperties");
            if (!andereKlant.Naam.Equals(Naam)) return false;
            if (!andereKlant.Adres.Equals(Adres)) return false;
            if (andereKlant.klantId!=klantId) return false;
            return true;
        }
        public override bool Equals(object? obj)
        {
            return obj is Klant klant &&
                   KlantId == klant.KlantId;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(KlantId);
        }
    }
}