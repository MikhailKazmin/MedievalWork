using Ell.Resources;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Ell.Workers;

namespace Ell.UI
{
    public class UIMenu : UIBase
    {
        private Image _Icon;
        private Transform _Data;
        private Transform _Improvment; //Frame-BuyImprovements

        private Transform _Improvments;
        private Transform _ImprovmentPrefCountCreateBuild;
        private Transform _ImprovmentPrefStatsCountCreateBuild;
        private TextMeshProUGUI _ImprovmentPrefStatsNameCountMax;
        private TextMeshProUGUI _ImprovmentPrefStatsLVLCountMax;
        private TextMeshProUGUI _ImprovmentPrefStatsCountMax;
        private Button _ImprovmentPrefButtonMaxCount;
        private TextMeshProUGUI _ImprovmentPrefButtonMaxCountCost;

        private Transform _ImprovmentPrefOnClick;
        private Transform _ImprovmentPrefStatsOnClick;
        private TextMeshProUGUI _ImprovmentPrefStatsNameVelocity;
        private TextMeshProUGUI _ImprovmentPrefStatsLVLVelocity;
        private TextMeshProUGUI _ImprovmentPrefStatsCountVelocity;
        private Button _ImprovmentPrefButtonVelocity;
        private TextMeshProUGUI _ImprovmentPrefButtonVelocityCost;


        private void Init(GameObject PanelCart, Base _script)
        {
            if (_Name == null && _Exit == null && _MenuObjects == null && _Previous == null && _Next == null)
            {
                _MenuObjects = PanelCart.transform.parent;
                _Name = _MenuObjects.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
                _Exit = _MenuObjects.GetChild(0).GetComponent<Button>();
                _Previous = _MenuObjects.GetChild(1).GetChild(2).GetComponent<Button>();
                _Next = _MenuObjects.GetChild(1).GetChild(1).GetComponent<Button>();
            }
            _Panel = PanelCart.transform;
            _Icon = _Panel.GetChild(0).GetChild(0).GetComponentInChildren<Image>();
            _Data = _Panel.GetChild(1);

            _Improvments = _Panel.GetChild(2);

            _ImprovmentPrefCountCreateBuild = _Improvments.GetChild(0);
            _ImprovmentPrefStatsCountCreateBuild = _ImprovmentPrefCountCreateBuild.GetChild(1);
            _ImprovmentPrefStatsNameCountMax = _ImprovmentPrefStatsCountCreateBuild.GetChild(0).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefStatsLVLCountMax = _ImprovmentPrefStatsCountCreateBuild.GetChild(1).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefStatsCountMax = _ImprovmentPrefStatsCountCreateBuild.GetChild(2).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefButtonMaxCount = _ImprovmentPrefCountCreateBuild.GetChild(2).GetComponent<Button>();
            _ImprovmentPrefButtonMaxCountCost = _ImprovmentPrefButtonMaxCount.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefButtonMaxCountCost.text = $"{HowCostImprovement(_script.LevelMaxCount, _script.CostUpdate)}$";

            _ImprovmentPrefOnClick = _Improvments.GetChild(1);
            _ImprovmentPrefStatsOnClick = _ImprovmentPrefOnClick.GetChild(1);
            _ImprovmentPrefStatsNameVelocity = _ImprovmentPrefStatsOnClick.GetChild(0).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefStatsLVLVelocity = _ImprovmentPrefStatsOnClick.GetChild(1).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefStatsCountVelocity = _ImprovmentPrefStatsOnClick.GetChild(2).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefButtonVelocity = _ImprovmentPrefOnClick.GetChild(2).GetComponent<Button>();
            _ImprovmentPrefButtonVelocityCost = _ImprovmentPrefButtonVelocity.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefButtonVelocityCost.text = $"{HowCostImprovement(_script.LevelVelocity, _script.CostUpdate)}$";
        }
        public void OnPrintedCurrent(Base Script, GameObject PanelCart, Sprite Cart)
        {
            if (PanelCart.transform.parent.gameObject.activeSelf != true)
            {
                Init(PanelCart, Script);
                _Panel.gameObject.SetActive(true);
                _MenuObjects.gameObject.SetActive(true);
                _Name.text = Script.transform.name;
                //_Icon.sprite = Cart;
                _Exit.onClick.AddListener(() => OnExitPanelCart(Script));
                _Previous.onClick.AddListener(() => _PreviousWorker(Script));
                _Next.onClick.AddListener(() => _NextWorker(Script));
                _ImprovmentPrefButtonMaxCount.onClick.AddListener(() => OnButtonImproveCountMax(Script));
                _ImprovmentPrefButtonVelocity.onClick.AddListener(() => OnButtonImproveVelocity(Script));
            }
            PrintImproveVelocity(Script);
            PrintImproveCountMax(Script);
            PrintCurrentResourcesInCart(Script);
            _Next.gameObject.SetActive(true);
            _Previous.gameObject.SetActive(true);
        }
        private void _PreviousWorker(Base _script)
        {
            _Exit.onClick.Invoke();
            var NumberCurrentBuild = _script.transform.GetSiblingIndex();
            if (NumberCurrentBuild > 0) _script.transform.parent.GetChild(--NumberCurrentBuild).GetComponent<Button>().onClick.Invoke();
            else _script.transform.parent.GetChild(_script.transform.parent.childCount - 1).GetComponent<Button>().onClick.Invoke();
        }
        private void _NextWorker(Base _script)
        {
            _Exit.onClick.Invoke();
            var NumberCurrentBuild = _script.transform.GetSiblingIndex();
            if (NumberCurrentBuild < _script.transform.parent.childCount - 1) _script.transform.parent.GetChild(++NumberCurrentBuild).GetComponent<Button>().onClick.Invoke();
            else _script.transform.parent.GetChild(0).GetComponent<Button>().onClick.Invoke();
        }
        private void PrintCurrentResourcesInCart(Base Script)
        {
            for (int i = 0; i < _Data.childCount; i++)
            {
                _Data.GetChild(i).gameObject.SetActive(false);
            }
            int k = 0;
            var PrefabResource = _Data.GetChild(0).gameObject;
            
            foreach (var Resource in Script.ResourcesCount)
            {
                if (Script.ResourcesCount.Count > k && k != 0 && Script.ResourcesCount.Count > _Data.childCount)
                {
                    var qwee = Instantiate(PrefabResource, _Data);
                }
                var CurretData = _Data.GetChild(k);
                CurretData.gameObject.SetActive(true);
                var dataResource = Script.dataResources.Where(p => p.resourcesName == Resource.Key).Select(p => p);
                CurretData.GetChild(0).GetComponent<Image>().sprite = dataResource.First().sprite; //Icon
                CurretData.GetChild(1).GetComponent<TextMeshProUGUI>().text = Resource.Value.ToString(); //Count
                k++;
            }
        }
        private void OnExitPanelCart(Base Script)
        {
            Script.OnUnSelictionCurrentCart?.Invoke();
            _ImprovmentPrefButtonMaxCount.onClick.RemoveAllListeners();
            _ImprovmentPrefButtonVelocity.onClick.RemoveAllListeners();
            _Previous.onClick.RemoveAllListeners();
            _Next.onClick.RemoveAllListeners();
            _Exit.onClick.RemoveAllListeners();
            _MenuObjects.gameObject.SetActive(false);
            _Panel.gameObject.SetActive(false);
        }
        private void OnButtonImproveCountMax(Base _script)
        {
            int cost = 0;
            cost = HowCostImprovement(_script.LevelMaxCount, _script.CostUpdate);
            
            if (Money.CheckOnBuy(cost))
            {
                Debug.Log(cost.ToString());
                Money.ReduceCount(cost);
                _script.OnIncriseCountMax?.Invoke();
            }
            PrintImproveCountMax(_script);
        }
        private void OnButtonImproveVelocity(Base _script)
        {
            int cost = 0;
            cost = HowCostImprovement(_script.LevelVelocity, _script.CostUpdate);
            if (Money.CheckOnBuy(cost))
            {
                Debug.Log(cost.ToString());
                Money.ReduceCount(cost);
                _script.OnIncriseVelocity?.Invoke();
            }
            PrintImproveVelocity(_script);
        }
        private void PrintImproveVelocity(Base _script)
        {
            _ImprovmentPrefButtonVelocityCost.text = $"{HowCostImprovement(_script.LevelVelocity, _script.CostUpdate)}$";
            _ImprovmentPrefStatsNameVelocity.text = "Скорость";
            _ImprovmentPrefStatsLVLVelocity.text = _script.LevelVelocity.ToString() + " LVL";
            _ImprovmentPrefStatsCountVelocity.text = _script.timeMove.ToString() + " Км/ч";
        }
        private void PrintImproveCountMax(Base _script)
        {
            _ImprovmentPrefButtonMaxCountCost.text = $"{HowCostImprovement(_script.LevelMaxCount, _script.CostUpdate)}$";
            _ImprovmentPrefStatsNameCountMax.text = "Вместимость";
            _ImprovmentPrefStatsLVLCountMax.text = _script.LevelMaxCount.ToString() + " LVL";
            _ImprovmentPrefStatsCountMax.text = _script.CountMax.ToString();
        }
        private int HowCostImprovement(int level, List<int> cost)
        {
            if (level < 10) return cost[0];
            else if (level < 20) return cost[1];
            else if (level < 30) return cost[2];
            else if (level < 40) return cost[3];
            return 50;
        }
    }
}