using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruitjesBL.Exceptions;
using TruitjesBL.Interfaces;

namespace TruitjesBL.Managers
{
    public class ClubManager
    {
        private IClubRepository clubRepo;

        public ClubManager(IClubRepository clubRepo)
        {
            this.clubRepo = clubRepo;
        }

        public IReadOnlyList<string> GeefCompetities()
        {
            try
            {
                return clubRepo.GeefCompetities();
            }
            catch(Exception ex)
            {
                throw new ClubManagerException("GeefCompetities",ex);
            }
        }
        public IReadOnlyList<string> GeefClubs(string competitie)
        {
            try
            {
                return clubRepo.GeefClubs(competitie);
            }
            catch (Exception ex)
            {
                throw new ClubManagerException("GeefClubs",ex);
            }
        }
    }
}
