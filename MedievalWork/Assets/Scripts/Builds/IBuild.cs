using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Builds
{
    interface IBuild : IObjectLogicsGame
    {
        int IncreaseCount(ResourcesName resourcesName, int Count);
        void ReduceCount(ResourcesName resourcesName, int Count);
    }
}
