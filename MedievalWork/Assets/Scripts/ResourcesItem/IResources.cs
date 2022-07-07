using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ResourcesItem
{
    interface IResources
    {
    }

    public enum ResourcesName
    {
        WoodLog,
        Wool,
        Coal,
        WoodenPlank
    }

    public enum ResourcesType
    {
        Mining,
        Craft,
        Money
    }
}
