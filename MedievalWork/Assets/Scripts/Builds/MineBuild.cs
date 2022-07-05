using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.ResourcesItem;

namespace Assets.Scripts.Builds
{
    public class MineBuild : BaseBuild, IMineBuild
    {
        [SerializeField] private float time = 1f;

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(MiningResources());
        }
        private IEnumerator MiningResources()
        {
            while (true)
            {
                yield return new WaitForSeconds(time);
                var test = new Dictionary<ResourcesName, int>();
                foreach (var item in ResourcesCount)
                {
                    test.Add(item.Key, IncreaseCount(item.Key, 1));
                }
                ResourcesCount = test;
            }
        }

        public override int IncreaseCount(ResourcesName resourcesName, int Count)
        {
            return ResourcesCount[resourcesName] + Count;
        }

        public override int ReduceCount(ResourcesName resourcesName, int Count)
        {
            if (ResourcesCount[resourcesName] >= Count)
            {
                ResourcesCount[resourcesName] -= Count;
                return Count;
            }
            else
            {
                var CurrentCount = ResourcesCount[resourcesName];
                ResourcesCount[resourcesName] = 0;
                return CurrentCount;
            }
            
            
        }

        public ResourcesName[] GetResoursesName()
        {
            ResourcesName[] arr = new ResourcesName[ResourcesCount.Count];

            return arr = ResourcesCount.Keys.ToArray();
        }
    }
}