using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public override void ReduceCount(ResourcesName resourcesName, int Count)
        {
            if (ResourcesCount[resourcesName] >= Count)
            {
                ResourcesCount[resourcesName] -= Count;
            }
            else
            {
                ResourcesCount[resourcesName] = 0;
            }
            
        }
    }
}