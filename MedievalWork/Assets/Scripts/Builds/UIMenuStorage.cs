using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.ResourcesItem;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace Assets.Scripts.Builds
{
    public class UIMenuStorage : UIBase
    {
        //private Storage _script;

        private Transform _Data;
        private Transform _DataPref;
        private Image _IconDataPref;
        private TextMeshProUGUI _NameDataPref;
        private TextMeshProUGUI _CostDataPref;
        private TextMeshProUGUI _CountDataPref;

        private Transform _TypeResources;
        private Transform _TypeResourcesPref;
        private void Init(GameObject Panel)
        {
            if (_Name == null && _Exit == null && _MenuObjects == null)
            {
                _MenuObjects = Panel.transform.parent;
                _Name = _MenuObjects.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
                _Exit = _MenuObjects.GetChild(0).GetComponent<Button>();
            }

            //_script = Script as Storage;
            _Panel = Panel.transform;

            _Data = _Panel.GetChild(0).GetChild(1);
            _DataPref = _Data.GetChild(0);
            _IconDataPref = _DataPref.GetChild(0).GetComponent<Image>();
            _NameDataPref = _DataPref.GetChild(1).GetComponent<TextMeshProUGUI>();
            _CountDataPref = _DataPref.GetChild(2).GetComponent<TextMeshProUGUI>();
            _CostDataPref = _DataPref.GetChild(3).GetComponent<TextMeshProUGUI>();

            _TypeResources = _Panel.GetChild(0).GetChild(0);
            _TypeResourcesPref = _TypeResources.GetChild(0);

        }
        public void OnPrintedCurrent(GameObject Panel, Base Script)
        {
            if (Panel.transform.parent.gameObject.activeSelf != true)
            {
                Init(Panel);
                _Panel.gameObject.SetActive(true);
                _MenuObjects.gameObject.SetActive(true);
                _Exit.onClick.AddListener(() => OnExitPanelStorage(Script as Storage));
                _Name.text = Script.transform.name;
            }
            PrintResourcesInStorage(Script as Storage);
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
                //Debug.Log(Resource.Key.ToString());
                if (_script.ResourcesCount.Count > k && k != 0 && _script.ResourcesCount.Count > _Data.childCount)
                {
                    var qwee = Instantiate(_DataPref, _Data);
                }
                
                var prefab = _Data.GetChild(k);
                prefab.gameObject.SetActive(true);
                var dataResource = _script.dataResources.Where(p => p.resourcesName == Resource.Key).Select(p => p).First();
                prefab.GetChild(0).GetComponent<Image>().sprite = dataResource.sprite; //Icon
                prefab.GetChild(1).GetComponent<TextMeshProUGUI>().text = dataResource.name; //name
                prefab.GetChild(2).GetComponent<TextMeshProUGUI>().text = Resource.Value.ToString(); //Count
                prefab.GetChild(3).GetComponent<TextMeshProUGUI>().text = dataResource.Cost.ToString()+"$"; //Cost
                k++;
            }
        }
        private void OnExitPanelStorage(Storage _script)
        {
            Debug.Log($"{this.GetType()} ");
            _Exit.onClick.RemoveAllListeners();
            _MenuObjects.gameObject.SetActive(false);
            _Panel.gameObject.SetActive(false);
        }
    }
}