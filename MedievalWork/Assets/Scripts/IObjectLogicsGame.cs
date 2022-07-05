using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.ResourcesItem;

namespace Assets.Scripts
{
    public interface IObjectLogicsGame
    {
        Dictionary<ResourcesName, int> ResourcesCount { get; }
        int IncreaseCount(ResourcesName resourcesName, int Count);
        int ReduceCount(ResourcesName resourcesName, int Count);
        void OnClick();
    }
}
