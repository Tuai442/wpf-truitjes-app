using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruitjesBL.Exceptions;
using TruitjesBL.Interfaces;
using TruitjesBL.Model;

namespace TruitjesBL.Managers
{
    public class KlantManager
    {
        private IKlantRepository klantRepo;

        public KlantManager(IKlantRepository klantRepo)
        {
            this.klantRepo = klantRepo;
        }

        public void VoegKlantToe(Klant klant)
        {
            try
            {
                if (klantRepo.BestaatKlant(klant))
                {
                    throw new KlantManagerException("voegKlantToe");
                }
                klantRepo.VoegKlantToe(klant);
            }
            catch (Exception ex)
            {
                throw new KlantManagerException("voegKlantToe", ex);
            }
        }
        public void VerwijderKlant(Klant klant)
        {
            try
            {
                if (!klantRepo.BestaatKlant(klant.KlantId)) throw new KlantManagerException("VerwijderKlant");
                klantRepo.VerwijderKlant(klant);
            }
            catch(Exception ex)
            {
                KlantManagerException ex2 = new KlantManagerException("VerwijderKlant", ex);
                ex2.Data.Add("klant",klant);
                throw ex2;
            }
        }
        public void UpdateKlant(Klant klant)
        {
            try
            {
                if (klantRepo.BestaatKlant(klant.KlantId))
                {
                    Klant dbKlant = klantRepo.GeefKlant(klant.KlantId);
                    if (dbKlant.HeeftZelfdeProperties(klant)) throw new KlantManagerException("updateKlant - klant is dezelfde");
                    klantRepo.UpdateKlant(klant);
                }
                else throw new KlantManagerException("updateKlant - klant bestaat niet");
            }
            catch(Exception e)
            {
                throw new KlantManagerException("updateKlant", e);
            }
        }
        public IReadOnlyList<Klant> ZoekKlanten(int? klantId, string naam, string adres)
        {
            List<Klant> klanten=new List<Klant>();
            try
            {
                if (klantId.HasValue)
                {
                    if (klantRepo.BestaatKlant(klantId.Value))
                        klanten.Add(klantRepo.GeefKlant(klantId.Value));
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(naam) || !string.IsNullOrWhiteSpace(adres))
                    {
                        klanten.AddRange(klantRepo.GeefKlanten(naam, adres));
                    }
                    else
                    {
                        throw new KlantManagerException("ZoekKlanten - naam en adres zijn leeg");
                    }
                }
                return klanten;
            }
            catch(Exception ex)
            {
                throw new KlantManagerException("ZoekKlanten", ex);
            }
        }
    }
}
