﻿using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Builds;

namespace Assets.Scripts.ResourcesItem
{
    public class Money : MonoBehaviour
    {
        [SerializeField] private Button _but;
        [SerializeField] private Button _Exit;
        [SerializeField] private GameObject _PanelBuy;
        [SerializeField] private static int _count = 3;
        [SerializeField] private TextMeshProUGUI _textCount;

        private static Action OnСhangeCount;

        private void Start()
        {
            OnСhangeCount += PrintCount;
            _but.onClick.AddListener(() => CreateBuildAndWorker());
            _Exit.onClick.AddListener(() => _PanelBuy.SetActive(!_PanelBuy.activeSelf));
            PrintCount();
        }
        private void CreateBuildAndWorker()
        {
            if (CheckOnBuy(1))
            {
                ReduceCount(1);
                Buy.OnCreateBuildAndWorker?.Invoke();
            }
        }
        private void PrintCount() => _textCount.text = _count.ToString();
        public static bool CheckOnBuy(int Cost) => _count - Cost >= 0;
        public static int AddCount(int count)
        {
            _count += count;
            OnСhangeCount.Invoke();
            return _count;
        }
        public static int ReduceCount(int count)
        {
            _count -= count;
            OnСhangeCount.Invoke();
            return _count;
        }
    }
}