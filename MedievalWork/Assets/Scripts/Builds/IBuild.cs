using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.ResourcesItem;

namespace Assets.Scripts.Builds
{
    interface IBuild : IObjectLogicsGame
    {
        int IncreaseCount(ResourcesName resourcesName, int Count);
        int ReduceCount(ResourcesName resourcesName, int Count);
        
    }
}
