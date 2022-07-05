using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Builds
{
    public class Storage : BaseBuild, IStorage
    {
        private static Storage Instanse = null;

        public static Storage GetInstanse() => Instanse;

        protected override void Awake()
        {
            base.Awake();
            if (Instanse == null) Instanse = this;
            else if (Instanse == this) Destroy(gameObject);
        }
        public override int IncreaseCount(ResourcesName resourcesName, int Count)
        {
            if (Count <= 0)
            {
                Debug.Log($"Увеличение на {Count}");
                return 0;
            }

            if (ResourcesCount == null) ResourcesCount = new Dictionary<ResourcesName, int>();

            if (ResourcesCount.ContainsKey(resourcesName))
            {
                ResourcesCount[resourcesName] += Count;
            }
            else
            {
                ResourcesCount.Add(resourcesName,Count);
            }
            return Count;
            
        }

        public override int ReduceCount(ResourcesName resourcesName, int Count)
        {
            throw new System.NotImplementedException();
        }

        

    }
}