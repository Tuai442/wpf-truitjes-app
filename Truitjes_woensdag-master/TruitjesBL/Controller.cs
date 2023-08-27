using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruitjesBL.Interfaces;
using TruitjesBL.Managers;

namespace TruitjesBL
{
    public class Controller
    {
        private IBestellingRepository _bestellingRepository;
        private IClubRepository _clubRepository;
        private IKlantRepository _klantRepository;
        private ITruitjeRepository _truitjeRepository;

        private BestellingManager _bestellingManager;
        private KlantManager _klantManager;

        public Controller(IBestellingRepository bestellingRepository, IClubRepository clubRepository,
            IKlantRepository klantRepository, ITruitjeRepository truitjeRepository)
        {
            _bestellingRepository = bestellingRepository;
            _clubRepository = clubRepository;
            _klantRepository = klantRepository;
            _truitjeRepository = truitjeRepository;

            _bestellingManager = new BestellingManager(_bestellingRepository);
            _klantManager = new KlantManager(_klantRepository);
        }

        public BestellingManager GeefBestellingManager()
        {
            return _bestellingManager;
        }

        public KlantManager GeefKlantManager()
        {
            return _klantManager;
        }
    }
}
