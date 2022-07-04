using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Builds
{
    public class Storage : BaseBuild, IStorage
    {


        public override int IncreaseCount(ResourcesName resourcesName, int Count)
        {
            throw new System.NotImplementedException();
        }

        public override void ReduceCount(ResourcesName resourcesName, int Count)
        {
            throw new System.NotImplementedException();
        }

    }
}