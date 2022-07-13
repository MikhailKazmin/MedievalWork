using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ell.Resources;
using System.Linq;

namespace Ell.Builds
{
    public abstract class Base : MonoBehaviour
    {
        protected Button ButtonClick;
        [SerializeField] private bool ResourceIsSave = false;
        public bool isBuildSelection { get; protected set; } = false;
        public delegate void Del();
        public Del OnUnSeliction { get; protected set; }

        public List<Data> dataResources { get; protected set; }
        public Dictionary<ResourcesName, int> ResourcesCount { get; protected set; }

        protected virtual void Awake()
        {
            ButtonClick = GetComponent<Button>();
            ButtonClick.onClick.AddListener(() => OnClick());
            Debug.Log($"Awake to {GetType()}");
            ResourcesCount = GetResources();
            dataResources = UnityEngine.Resources.LoadAll<Data>("Resources").ToList();
            OnUnSeliction += () => isBuildSelection = false;
        }


        private Dictionary<ResourcesName, int> GetResources()
        {
            var SaveDict = new Dictionary<ResourcesName, int>();
            if (ResourceIsSave)// из сохранения
            {
                int countResourceMiningInBuild = Random.Range(1, 4);

                for (int i = 0; i <= countResourceMiningInBuild; i++)
                {
                    SaveDict.Add((ResourcesName)i, Random.Range(1, 40));
                }
            }
            return SaveDict;
        }
        public virtual void OnClick()
        {
            isBuildSelection = true;
            //Debug.Log($"On Click to {this.GetType()}");
            //if (ResourcesCount.Count == 0)
            //{
            //    Debug.Log($" Not Resource ");
            //}
            //else
            //{
            //    string str = "";
            //    foreach (var item in ResourcesCount)
            //    {
            //        str += $"Resource[{item.Key}] = {item.Value}\n";
            //    }
            //    Debug.Log(str);
            //}

        }

        public abstract int ReduceCount(ResourcesName resourcesName, int Count);


        public abstract int IncreaseCount(ResourcesName resourcesName, int Count);
        protected void OnDestroy()
        {
            ButtonClick.onClick.RemoveAllListeners();
        }
    }
}