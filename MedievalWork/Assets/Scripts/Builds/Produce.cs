using Ell.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ell.Builds
{
    public class Produce : Base
    {
        public override int IncreaseCount(ResourcesName resourcesName, int Count)
        {
            return 0;
        }

        public override int ReduceCount(ResourcesName resourcesName, int Count)
        {
            return 0;
        }
    }
}
