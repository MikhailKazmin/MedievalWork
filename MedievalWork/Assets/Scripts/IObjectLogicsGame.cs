using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ell.Resources;

namespace Ell.BaseScripts
{
    public interface IObjectLogicsGame
    {
        Dictionary<ResourcesName, int> ResourcesCount { get; }
        int IncreaseCount(ResourcesName resourcesName, int Count);
        int ReduceCount(ResourcesName resourcesName, int Count);
        void OnClick();
    }
}
