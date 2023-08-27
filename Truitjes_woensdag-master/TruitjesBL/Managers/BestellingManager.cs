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
   
    public class BestellingManager
    {
        private IBestellingRepository bestellingRepo;

        public BestellingManager(IBestellingRepository bestellingRepo)
        {
            this.bestellingRepo = bestellingRepo;
        }
        public void VoegBestellingToe(Bestelling bestelling)
        {
            try
            {
                if (bestelling == null) throw new BestellingManagerException("VoegBestellingToe");
                if (bestellingRepo.BestaatBestelling(bestelling)) throw new BestellingManagerException("VoegBestellingToe");
                if (bestelling.Klant==null) throw new BestellingManagerException("VoegBestellingToe");
                //TODO bestaat klant ?
                if (bestelling.GeefTruitjes().Count()==0) throw new BestellingManagerException("VoegBestellingToe");
                bestellingRepo.VoegBestellingToe(bestelling);
            }
            catch(Exception ex)
            {
                throw new BestellingManagerException("VoegBestellingToe",ex);
            }
        }
        public IReadOnlyList<Bestelling> ZoekBestellingen(int? bestellingId,int? klantId,
            DateTime? startDatum,DateTime? eindDatum)
        {
            List<Bestelling> bestellingen=new List<Bestelling>();
            try
            {
                if (bestellingId.HasValue)
                {
                    if (bestellingRepo.BestaatBestelling(bestellingId.Value))
                        bestellingen.Add(bestellingRepo.GeefBestelling(bestellingId.Value));
                    else
                        throw new BestellingManagerException("ZoekBestellingen - id bestaat niet");
                }
                else
                {
                    if ((klantId!=null) || (startDatum!=null) || (eindDatum!=null))
                    {
                        bestellingen.AddRange(bestellingRepo.GeefBestellingen(klantId,startDatum,eindDatum));
                    }
                    else
                    {
                        throw new BestellingManagerException("ZoekBestellingen - velden zijn leeg");
                    }
                }
                return bestellingen;
            }
            catch (Exception ex)
            {
                throw new BestellingManagerException("ZoekBestellingen",ex);
            }
        }
        public void VerwijderBestelling(Bestelling bestelling)
        {
            try
            {
                if (!bestellingRepo.BestaatBestelling(bestelling))
                {
                    throw new BestellingManagerException("VerwijderBestelling");
                }
                else
                {
                    bestellingRepo.VerwijderBestelling(bestelling);
                }
            }
            catch(Exception ex)
            {
                throw new BestellingManagerException("VerwijderBestelling",ex);
            }
        }
        public void UpdateBestelling(Bestelling bestelling)
        {
            try
            {
                if (bestelling==null) throw new BestellingManagerException("UpdateBestelling");
                if (bestelling.Klant==null) throw new BestellingManagerException("UpdateBestelling");
                if (bestellingRepo.BestaatBestelling(bestelling.BestellingId)) throw new BestellingManagerException("UpdateBestelling");
                if (bestelling.GeefTruitjes().Count()==0) throw new BestellingManagerException("UpdateBestelling");
                Bestelling dbBestelling=bestellingRepo.GeefBestelling(bestelling.BestellingId);
                if (dbBestelling.HeeftZelfdeProperties(bestelling)) throw new BestellingManagerException("UpdateBestelling");
                bestellingRepo.UpdateBestelling(bestelling);
            }
            catch(Exception e)
            {
                throw new BestellingManagerException("UpdateBestelling",e);
            }
        }
    }
}
