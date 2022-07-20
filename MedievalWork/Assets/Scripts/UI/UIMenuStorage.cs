using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Ell.Resources;
using System.Collections.Generic;
using System.Linq;
using TMPro;

using Ell.Builds;

namespace Ell.UI
{
    public class UIMenuStorage : UIBase
    {

        private Transform _Data;
        private Transform _DataPref;
        private List<Button> _buttonResources = new List<Button>();
        private Image _IconDataPref;
        private TextMeshProUGUI _NameDataPref;
        private TextMeshProUGUI _CostDataPref;
        private TextMeshProUGUI _CountDataPref;

        private Button _sell;
        private Slider _countSell;
        private TextMeshProUGUI _countSellText;

        private Transform _TypeResources;
        private Transform _TypeResourcesPref;
        private ResourcesName _currentResource;
        private void Init(GameObject Panel)
        {
            if (_Name == null && _Exit == null && _MenuObjects == null)
            {
                _MenuObjects = Panel.transform.parent;
                _Name = _MenuObjects.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
                _Exit = _MenuObjects.GetChild(0).GetComponent<Button>();
                _Previous = _MenuObjects.GetChild(1).GetChild(2).GetComponent<Button>();
                _Next = _MenuObjects.GetChild(1).GetChild(1).GetComponent<Button>();
            }
            _Panel = Panel.transform;

            _Data = _Panel.GetChild(0).GetChild(1);
            _DataPref = _Data.GetChild(0);

            _IconDataPref = _DataPref.GetChild(0).GetComponent<Image>();
            _NameDataPref = _DataPref.GetChild(1).GetComponent<TextMeshProUGUI>();
            _CountDataPref = _DataPref.GetChild(2).GetComponent<TextMeshProUGUI>();
            _CostDataPref = _DataPref.GetChild(3).GetComponent<TextMeshProUGUI>();

            _TypeResources = _Panel.GetChild(0).GetChild(0);
            _TypeResourcesPref = _TypeResources.GetChild(0);
            _sell = _Panel.GetChild(1).GetChild(1).GetComponent<Button>();
            _countSell = _Panel.GetChild(1).GetChild(0).GetComponent<Slider>();
            _countSell.minValue = 1;
            _countSellText = _countSell.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        }
        private void PrintSellCount(Storage _script)
        {
            if (_script.ResourcesCount.ContainsKey(_currentResource))
            {
                _countSell.maxValue = _script.ResourcesCount[_currentResource];
                _countSellText.text = _countSell.value.ToString();
            }
        }
        public void OnPrintedCurrent(GameObject Panel, Base Script)
        {
            if (Panel.transform.parent.gameObject.activeSelf != true)
            {
                Init(Panel);
                _Panel.gameObject.SetActive(true);
                _MenuObjects.gameObject.SetActive(true);
                _Exit.onClick.AddListener(() => OnExitPanelStorage(Script as Storage));

                _sell.onClick.AddListener(() => QWe(Script as Storage));

                _Name.text = Script.transform.name;
                _Next.gameObject.SetActive(false);
                _Previous.gameObject.SetActive(false);
                _countSell.onValueChanged.AddListener(delegate { PrintSellCount(Script as Storage); });
            }
            PrintResourcesInStorage(Script as Storage);
        }
        private void QWe(Storage _script)
        {
            _script.ReduceCount(_currentResource, (int)_countSell.value);
            if (!_script.ResourcesCount.ContainsKey(_currentResource))
            {
                //_sell.onClick.RemoveAllListeners();
                //_countSell.onValueChanged.RemoveAllListeners();
                _sell.gameObject.SetActive(false);
                _countSell.gameObject.SetActive(false);
            }
        }
        private void AddListener(Button button, ResourcesName resourcesName, Storage _script)
        {
            Storage script = _script;
            ResourcesName res = resourcesName;
            button.onClick.AddListener(() => PrintSell(res, script));
        }
        private void PrintSell(ResourcesName resourcesName, Storage _script)
        {
            Debug.Log(resourcesName);
            if (_sell == null || _countSell == null) return;
            if (_sell.gameObject.activeSelf == false || _countSell.gameObject.activeSelf == false)
            {
                _sell.gameObject.SetActive(true);
                _countSell.gameObject.SetActive(true);
            }
            _currentResource = resourcesName;
            PrintSellCount(_script);
        }
        private void PrintResourcesInStorage(Storage _script)
        {
            for (int i = 0; i < _Data.childCount; i++)
            {
                _Data.GetChild(i).gameObject.SetActive(false);
            }
            int k = 0;
            foreach (var Resource in _script.ResourcesCount)
            {

                if (_script.ResourcesCount.Count > k &&
                    //k != 0 &&
                    _script.ResourcesCount.Count > _Data.childCount &&
                    _Data.childCount <= k)
                {
                    var qwee = Instantiate(_DataPref, _Data);
                    var but = qwee.GetComponent<Button>();
                    _buttonResources.Add(but);
                    AddListener(but, Resource.Key, _script);
                }
                var prefab = _Data.GetChild(k);
                if (_buttonResources.Count == 0)
                {
                    _buttonResources.Add(prefab.GetComponent<Button>());
                    AddListener(_buttonResources[0], Resource.Key, _script);
                }
                var dataResource = _script.dataResources.Where(p => p.resourcesName == Resource.Key).Select(p => p).First();
                prefab.GetChild(0).GetComponent<Image>().sprite = dataResource.sprite; //Icon
                prefab.GetChild(1).GetComponent<TextMeshProUGUI>().text = dataResource.name; //name
                prefab.GetChild(2).GetComponent<TextMeshProUGUI>().text = Resource.Value.ToString(); //Count
                prefab.GetChild(3).GetComponent<TextMeshProUGUI>().text = dataResource.Cost.ToString() + "$"; //Cost
                prefab.name = dataResource.name;
                prefab.gameObject.SetActive(true);
                //if (_buttonResources.Count < _Data.childCount)
                //{
                //    var but = prefab.GetComponent<Button>();
                //    _buttonResources.Add(but);
                //    AddListener(but,Resource.Key, _script);
                //}
                k++;
            }
            PrintSellCount(_script);
            //Debug.Log("Готово");
        }
        private void OnExitPanelStorage(Storage _script)
        {
            Debug.Log($"{GetType()} ");
            _script.OnUnSeliction?.Invoke();
            _Exit.onClick.RemoveAllListeners();
            _MenuObjects.gameObject.SetActive(false);
            _Panel.gameObject.SetActive(false);
            _sell.onClick.RemoveAllListeners();
            _sell.gameObject.SetActive(false);
            //_countSell.onValueChanged.RemoveAllListeners();
            _countSell.gameObject.SetActive(false);
        }
    }

    class StorageUI
    {
        protected static Transform _MenuObjects;
        protected static TextMeshProUGUI _Name;
        protected static Button _Exit;
        protected static Button _Next;
        protected static Button _Previous;
        protected Transform _Panel;


        private Transform _Data;
        private Transform _DataPref;
        private List<Button> _buttonResources = new List<Button>();
        private Image _IconDataPref;
        private TextMeshProUGUI _NameDataPref;
        private TextMeshProUGUI _CostDataPref;
        private TextMeshProUGUI _CountDataPref;

        private Button _sell;
        private Slider _countSell;
        private TextMeshProUGUI _countSellText;

        private Transform _TypeResources;
        private Transform _TypeResourcesPref;

        private void Init(GameObject Panel)
        {
            if (_Name == null && _Exit == null && _MenuObjects == null)
            {
                _MenuObjects = Panel.transform.parent;
                _Name = _MenuObjects.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
                _Exit = _MenuObjects.GetChild(0).GetComponent<Button>();
                _Previous = _MenuObjects.GetChild(1).GetChild(2).GetComponent<Button>();
                _Next = _MenuObjects.GetChild(1).GetChild(1).GetComponent<Button>();
            }
            _Panel = Panel.transform;

            _Data = _Panel.GetChild(0).GetChild(1);
            _DataPref = _Data.GetChild(0);

            _IconDataPref = _DataPref.GetChild(0).GetComponent<Image>();
            _NameDataPref = _DataPref.GetChild(1).GetComponent<TextMeshProUGUI>();
            _CountDataPref = _DataPref.GetChild(2).GetComponent<TextMeshProUGUI>();
            _CostDataPref = _DataPref.GetChild(3).GetComponent<TextMeshProUGUI>();

            _TypeResources = _Panel.GetChild(0).GetChild(0);
            _TypeResourcesPref = _TypeResources.GetChild(0);
            _sell = _Panel.GetChild(1).GetChild(1).GetComponent<Button>();
            _countSell = _Panel.GetChild(1).GetChild(0).GetComponent<Slider>();
            _countSell.minValue = 1;
            _countSellText = _countSell.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        }

        public abstract class UIBase : MonoBehaviour
        {
            protected static Transform _MenuObjects;
            protected static TextMeshProUGUI _Name;
            protected static Button _Exit;
            protected static Button _Next;
            protected static Button _Previous;
            protected Transform _Panel;

        }
    }
}