using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Ell.Builds;
using Ell.BaseScripts;

namespace Ell.Resources
{
    public class Money : MonoBehaviour
    {
        [SerializeField] private Button _but;
        [SerializeField] private TextMeshProUGUI _textCostBuild;
        [SerializeField] private Button _Exit;
        [SerializeField] private GameObject _PanelBuy;
        [SerializeField] private static int _count = 1;
        [SerializeField] private TextMeshProUGUI _textCount;
        private int _costBuild = 1;

        private static Action OnСhangeCount;

        private void Start()
        {
            _textCostBuild = _but.GetComponentInChildren<TextMeshProUGUI>();
            _textCostBuild.text = _costBuild.ToString() + "$";
            OnСhangeCount += PrintCount;
            _but.onClick.AddListener(() => CreateBuildAndWorker());
            _Exit.onClick.AddListener(() => _PanelBuy.SetActive(!_PanelBuy.activeSelf));
            PrintCount();
        }
        private void CreateBuildAndWorker()
        {
            if (CheckOnBuy(_costBuild))
            {

                ReduceCount(_costBuild);
                Buy.OnCreateBuildAndWorker?.Invoke();
                
                _costBuild *= 50;
                _textCostBuild.text = _costBuild.ToString() + "$";
            }
        }
        private void PrintCount()
        {
            if (CheckOnBuy(_costBuild))
            {
                _but.interactable = true;
            }
            else _but.interactable = false;
            _textCount.text = _count.ToString();
        }
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