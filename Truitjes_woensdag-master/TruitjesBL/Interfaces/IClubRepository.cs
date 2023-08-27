using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruitjesBL.Interfaces
{
    public interface IClubRepository
    {
        IReadOnlyList<string> GeefCompetities();
        IReadOnlyList<string> GeefClubs(string competitie);
    }
}
