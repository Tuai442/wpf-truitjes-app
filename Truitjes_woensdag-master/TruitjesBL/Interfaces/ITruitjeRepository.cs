using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruitjesBL.Model;

namespace TruitjesBL.Interfaces
{
    public interface ITruitjeRepository
    {
        void VoegTruitjeToe(Truitje truitje);
    }
}
