using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ResourcesItem;

namespace Assets.Scripts.Builds
{
    public class Storage : Base, IStorage
    {
        private static Storage Instanse = null;
        [SerializeField] private GameObject PanelStorage;
        public static Storage GetInstanse() => Instanse;
        private UIMenuStorage uIMenu = new UIMenuStorage();
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
            if (PanelStorage.transform.parent.gameObject.activeSelf == true && isBuildSelection)
            {
                ButtonClick.onClick?.Invoke();
            }
            return Count;
            
        }

        public override int ReduceCount(ResourcesName resourcesName, int Count)
        {
            throw new System.NotImplementedException();
        }

        public override void OnClick()
        {
            base.OnClick();

            uIMenu.OnPrintedCurrent(PanelStorage, this);
        }    

    }
}