using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ell.Resources
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
