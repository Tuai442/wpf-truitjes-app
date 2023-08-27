using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruitjesBL.Exceptions;

namespace TruitjesBL.Model
{
    public class Bestelling
    {
        private int bestellingId;
        private double prijs;
        private Dictionary<Truitje, int> _truitjes = new Dictionary<Truitje, int>();
        private bool betaald;

        public Bestelling(int bestellingId, bool betaald)
        {
            BestellingId = bestellingId;
            Betaald = betaald;
        }

        public Bestelling()
        {
        }

        public Bestelling(Dictionary<Truitje, int> truitjes, int bestellingId, DateTime tijdstip, bool betaald, double prijs, Klant klant)
        {
            _truitjes = truitjes;
            BestellingId = bestellingId;
            Tijdstip = tijdstip;
            Betaald = betaald;
            Prijs = prijs;
            ZetKlant(klant);
        }

        public int BestellingId
        {
            get => bestellingId; set
            {
                if (value <= 0) throw new BestellingException("BestellingId invalid"); bestellingId = value;
            }
        }
        public DateTime Tijdstip { get; set; }
        public bool Betaald
        {
            get => betaald; set
            {
                betaald = value;
                if (betaald)
                {
                    prijs = KostPrijs();
                }
                else
                {
                    prijs = 0.0;
                }
            }
        }
        public double Prijs
        {
            get => prijs; set
            {
                if (value < 0) throw new BestellingException("prijs invalid"); prijs = value;
            }
        }
        public Klant Klant { get; set; }
        public bool HeeftZelfdeProperties(Bestelling andereBestelling)
        {
            if (andereBestelling == null) throw new BestellingException("HeeftZelfdeProperties");
            if (bestellingId!=andereBestelling.BestellingId) return false;
            if ((Klant==null) && (andereBestelling.Klant!=null)) return false;
            if (Klant != null)
                if (!Klant.Equals(andereBestelling.Klant)) return false;
            if (Tijdstip.Ticks!=andereBestelling.Tijdstip.Ticks) return false;
            if (betaald!=andereBestelling.Betaald) return false;
            //TODO check dict
            if (andereBestelling._truitjes.Count != _truitjes.Count) return false;
            var x = _truitjes.Except(andereBestelling._truitjes);
            if (_truitjes.Except(andereBestelling._truitjes).Any()) return false;
            return true;
        }
        public double KostPrijs()
        {
            double korting;
            double prijs = 0.0;
            if (Klant == null)
            {
                korting = 0.0;
            }
            else
            {
                korting = Klant.Korting();
            }
            foreach (KeyValuePair<Truitje, int> kvp in _truitjes)
            {
                prijs += (kvp.Key.Prijs * kvp.Value * (1 - korting));
            }
            return prijs;
        }
        public IReadOnlyDictionary<Truitje,int> GeefTruitjes()
        {
            return _truitjes;
        }
        public void VoegTruitjeToe(Truitje truitje,int aantal)
        {
            if (truitje == null) throw new BestellingException("VoegTruitjeToe");
            if (aantal<=0) throw new BestellingException("VoegTruitjeToe");
            if (_truitjes.ContainsKey(truitje))
            {
                _truitjes[truitje]+=aantal;
            }
            else
            {
                _truitjes.Add(truitje, aantal);
            }
        }
        public void VerwijderTruitje(Truitje truitje,int aantal)
        {
            if (truitje == null) throw new BestellingException("VerwijderTruitje");
            if (aantal <= 0) throw new BestellingException("VerwijderTruitje");
            if (_truitjes.ContainsKey(truitje))
            {
                if (_truitjes[truitje] < aantal) throw new BestellingException("VerwijderTruitje");
                else if (_truitjes[truitje] == aantal)
                {
                    _truitjes.Remove(truitje);
                }
                else
                {
                    _truitjes[truitje] -= aantal;
                }
            }
            else
            {
                throw new BestellingException("VerwijderTruitje");
            }
        }
        public void ZetKlant(Klant nieuweKlant)
        {
            if (nieuweKlant == null) throw new BestellingException("ZetKlant");
            if (nieuweKlant.Equals(Klant)) throw new BestellingException("ZetKlant");
            if (Klant!=null)
            {
                if (Klant.HeeftBestelling(this))
                    Klant.VerwijderBestelling(this);
            }
            if (!nieuweKlant.HeeftBestelling(this))
                nieuweKlant.VoegBestellingToe(this);
            Klant = nieuweKlant;
           //nieuweKlant.VoegBestellingToe(this);
        }
        public void VerwijderKlant()
        {
            Klant = null;
        }
    }
}
