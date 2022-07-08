using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.ResourcesItem;

namespace Assets.Scripts.Builds
{
    public class Mining : Base, IMining
    {
        private float TimeCreate = 1f;
        public int CountCreate { get; protected set; } = 1;
        public int Level { get; protected set; } = 1;
        private static GameObject PanelBuilds;
        private bool isCurrentBuildSelection = false;
        public delegate void Del();
        public Del OnIncriseCountPerSecond;
        public Del OnUnSelictionCurrentBuild;
        protected UIMenuBuilds uIMenu = new UIMenuBuilds();

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(MiningResources());
            OnUnSelictionCurrentBuild += () => isCurrentBuildSelection = false;
            OnIncriseCountPerSecond += ImprovementTimeCreate;
        }

        private void ImprovementTimeCreate()
        {
            CountCreate+=10;
            Level++;
            if (PanelBuilds.transform.parent.gameObject.activeSelf == true && isCurrentBuildSelection)
            {
                ButtonClick.onClick?.Invoke();
            }
        }
        public void Init(GameObject PanelBuilds)
        {
            Mining.PanelBuilds = PanelBuilds;
        }
        private IEnumerator MiningResources()
        {
            while (true)
            {
                yield return new WaitForSeconds(TimeCreate);
                var test = new Dictionary<ResourcesName, int>();
                foreach (var item in ResourcesCount)
                {
                    test.Add(item.Key, IncreaseCount(item.Key, CountCreate));
                }
                ResourcesCount = test;
                if (PanelBuilds.transform.parent.gameObject.activeSelf == true && isCurrentBuildSelection)
                {
                    ButtonClick.onClick?.Invoke();
                }
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
                if (PanelBuilds.transform.parent.gameObject.activeSelf == true && isCurrentBuildSelection)
                {
                    ButtonClick.onClick?.Invoke();
                }
                return Count;
            }
            else
            {
                var CurrentCount = ResourcesCount[resourcesName];
                ResourcesCount[resourcesName] = 0;
                if (PanelBuilds.transform.parent.gameObject.activeSelf == true && isCurrentBuildSelection)
                {
                    ButtonClick.onClick?.Invoke();
                }
                
                return CurrentCount;
            }
            
            
        }
        public override void OnClick()
        {
            isCurrentBuildSelection = true;
            uIMenu.OnPrintedCurrent(PanelBuilds, this);
        }
        public ResourcesName[] GetResoursesName()
        {
            ResourcesName[] arr = new ResourcesName[ResourcesCount.Count];

            return arr = ResourcesCount.Keys.ToArray();
        }
    }
}