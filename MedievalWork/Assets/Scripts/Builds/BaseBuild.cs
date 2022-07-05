﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.ResourcesItem;

namespace Assets.Scripts.Builds
{
    public abstract class BaseBuild : MonoBehaviour
    {
        protected Button ButtonClick;
        [SerializeField] private bool ResourceIsSave = false;

        //public Dictionary<ResourcesName, int> ResourcesCount
        //{
        //    get => ResourcesCount;
        //    protected set => ResourcesCount = value;
        //}

        public Dictionary<ResourcesName, int> ResourcesCount
        {
            get;
            protected set;
        }
        protected virtual void Awake()
        {
            ButtonClick = GetComponent<Button>();
            ButtonClick.onClick.AddListener(() => OnClick());
            Debug.Log($"Awake to {this.GetType()}");
            ResourcesCount = GetResources();
            
        }


        private Dictionary<ResourcesName, int> GetResources()
        {
            var SaveDict = new Dictionary<ResourcesName, int>();
            if (ResourceIsSave)// из сохранения
            {
                int countResourceMiningInBuild = Random.Range(1, 4);

                for (int i = 0; i < countResourceMiningInBuild; i++)
                {
                    SaveDict.Add((ResourcesName)i, Random.Range(1, 15));
                }
            }
            return SaveDict;
        }
        public virtual void OnClick()
        {
            Debug.Log($"On Click to {this.GetType()}");
            if (ResourcesCount.Count == 0)
            {
                Debug.Log($" Not Resource ");
            }
            else
            {
                string str = "";
                foreach (var item in ResourcesCount)
                {
                    str += $"Resource[{item.Key}] = {item.Value}\n";
                }
                Debug.Log(str);
            }
            
        }

        public abstract int ReduceCount(ResourcesName resourcesName, int Count);


        public abstract int IncreaseCount(ResourcesName resourcesName, int Count);
        protected void OnDestroy()
        {
            ButtonClick.onClick.RemoveAllListeners();
        }
    }
}