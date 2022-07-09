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
                //_Icon.sprite = Cart;
                _Improvment.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnButtonImproveCountMax(Script));
                _Improvment.GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnButtonImproveVelocity(Script));
                _Exit.onClick.AddListener(() => OnExitPanelCart(Script));
                _Previous.onClick.AddListener(() => _PreviousWorker(Script));
                _Next.onClick.AddListener(() => _NextWorker(Script));
            }
            PrintCurrentResourcesInCart(Script);
            PrintCurrentPropertiesCart(Script);
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
            _Previous.onClick.RemoveAllListeners();
            _Next.onClick.RemoveAllListeners();
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