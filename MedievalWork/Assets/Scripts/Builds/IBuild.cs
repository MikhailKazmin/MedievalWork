using Ell.Resources;
using Ell.BaseScripts;

namespace Ell.Builds
{
    interface IBuild : IObjectLogicsGame
    {
        int IncreaseCount(ResourcesName resourcesName, int Count);
        int ReduceCount(ResourcesName resourcesName, int Count);
    }
    interface IMining : IBuild
    {

    }
    interface IProducing : IBuild
    {

    }
    interface IStorage : IBuild
    {

    }
}
