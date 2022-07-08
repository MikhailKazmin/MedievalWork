using Assets.Scripts.ResourcesItem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Workers
{
    public class UIMenu : UIBase
    {
        private Image _Icon;
        private Transform _Data;
        private Transform _Improvment; //Frame-BuyImprovements
        private TextMeshProUGUI _ImprovmentMaxCount;
        private TextMeshProUGUI _ImprovmentVelocity;

        private void Init(GameObject PanelCart)
        {
            if (_Name == null && _Exit == null && _MenuObjects == null)
            {
                _MenuObjects = PanelCart.transform.parent;
                _Name = _MenuObjects.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
                _Exit = _MenuObjects.GetChild(0).GetComponent<Button>();
                
            }
            _Panel = PanelCart.transform;
            _Icon = _Panel.GetChild(0).GetComponentInChildren<Image>();
            _Data = _Panel.GetChild(1);
            _ImprovmentMaxCount = _Panel.GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            _ImprovmentVelocity = _Panel.GetChild(2).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>();
            _Improvment = _Panel.GetChild(2).GetChild(1);
        }
        public void OnPrintedCurrent(Base Script, GameObject PanelCart, Sprite Cart)
        {
            
            if (PanelCart.transform.parent.gameObject.activeSelf != true)
            {
                Init(PanelCart);
                
                _Panel.gameObject.SetActive(true);
                _MenuObjects.gameObject.SetActive(true);
                _Name.text = Script.transform.name;
                _Icon.sprite = Cart;
                _Improvment.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnButtonImproveCountMax(Script));
                _Improvment.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnButtonImproveVelocity(Script));
                _Exit.onClick.AddListener(() => OnExitPanelCart(Script));
                Debug.Log($"{this.GetType()} "  + _Exit.onClick.GetPersistentEventCount());
            }
            PrintCurrentResourcesInCart(Script);
            PrintCurrentPropertiesCart(Script);
            Debug.Log(_Exit.onClick.GetPersistentEventCount());
        }
        private void PrintCurrentPropertiesCart(Base Script)
        {
            _ImprovmentMaxCount.text = Script.CountMax.ToString();
            _ImprovmentVelocity.text = Script.timeMove.ToString(); 
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
            Debug.Log($"{this.GetType()} ");
            Script.OnUnSelictionCurrentCart?.Invoke();
            _Improvment.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
            _Improvment.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
            _Exit.onClick.RemoveAllListeners();
            _MenuObjects.gameObject.SetActive(false);
            _Panel.gameObject.SetActive(false);
        }
        private void OnButtonImproveCountMax(Base Script)
        {
            Script.OnIncriseCountMax?.Invoke();
            _ImprovmentMaxCount.text = (Script.CountMax).ToString();
        }
        private void OnButtonImproveVelocity(Base Script)
        {
            Script.OnIncriseVelocity?.Invoke();
            _ImprovmentVelocity.text = (Script.timeMove).ToString();
        }
    }
}