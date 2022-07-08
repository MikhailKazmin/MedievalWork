using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

namespace Assets.Scripts.Builds
{
    public class UIMenuBuilds : UIBase
    {
        private Mining _script;
        private Image _Icon;

        private Transform _Data;
        private Transform _DataPref;
        private Image _IconDataPref;
        private TextMeshProUGUI _NameDataPref;
        private TextMeshProUGUI _SpeedDataPref;
        private TextMeshProUGUI _CountDataPref;

        private Transform _Improvments; 
        private Transform _ImprovmentPref;
        private Transform _ImprovmentPrefStats;
        private TextMeshProUGUI _ImprovmentPrefStatsName;
        private TextMeshProUGUI _ImprovmentPrefStatsLVL;
        private TextMeshProUGUI _ImprovmentPrefStatsCount;
        private Button _ImprovmentPrefButton;

        private void Init(GameObject PanelBuilds, Base Script)
        {
            if (_Name == null && _Exit == null && _MenuObjects == null)
            {
                _MenuObjects = PanelBuilds.transform.parent;
                _Name = _MenuObjects.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
                _Exit = _MenuObjects.GetChild(0).GetComponent<Button>();
            }

            _script = Script as Mining;
            _Panel = PanelBuilds.transform;
            _Icon = _Panel.GetChild(0).GetComponentInChildren<Image>();

            _Data = _Panel.GetChild(1);
            _DataPref = _Data.GetChild(0);
            _IconDataPref = _DataPref.GetChild(0).GetComponent<Image>();
            _NameDataPref = _DataPref.GetChild(1).GetComponent<TextMeshProUGUI>();
            _SpeedDataPref = _DataPref.GetChild(2).GetComponent<TextMeshProUGUI>();
            _CountDataPref = _DataPref.GetChild(3).GetComponent<TextMeshProUGUI>();

            _Improvments = _Panel.GetChild(2);
            _ImprovmentPref = _Improvments.GetChild(0);
            _ImprovmentPrefStats = _ImprovmentPref.GetChild(1);
            _ImprovmentPrefStatsName = _ImprovmentPrefStats.GetChild(0).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefStatsLVL = _ImprovmentPrefStats.GetChild(1).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefStatsCount = _ImprovmentPrefStats.GetChild(2).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefButton = _ImprovmentPref.GetChild(2).GetComponent<Button>();
        }
        public void OnPrintedCurrent(GameObject Panel, Base Script)
        {
            if (Panel.transform.parent.gameObject.activeSelf != true)
            {
                Init(Panel, Script);
                _Panel.gameObject.SetActive(true);
                _MenuObjects.gameObject.SetActive(true);
                _Icon.sprite = _script.gameObject.GetComponent<Image>().sprite;
                _Exit.onClick.AddListener(OnExitPanel);

                _Name.text = Script.transform.name;
                _ImprovmentPrefButton.onClick.AddListener(() => OnButtonImproveCountMax());
                PrintStatsImprovement();
                Debug.Log(_Exit.onClick.GetPersistentEventCount());
                
            }
            Debug.Log($"{Script.name}");
            PrintResourcesInBuild();

        }
        private void OnExitPanel()
        {
            _script.OnUnSelictionCurrentBuild?.Invoke();
            //_ImprovmentPref.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
            //_Exit.onClick.RemoveAllListeners();
            _MenuObjects.gameObject.SetActive(false);
            _Panel.gameObject.SetActive(false);
        }
        private void PrintResourcesInBuild()
        {
            for (int i = 0; i < _Data.childCount; i++)
            {
                _Data.GetChild(i).gameObject.SetActive(false);
            }
            int k = 0;
            foreach (var Resource in _script.ResourcesCount)
            {
                if (_script.ResourcesCount.Count > k && k != 0 && _script.ResourcesCount.Count > _Data.childCount) Instantiate(_DataPref, _Data);

                var prefab = _Data.GetChild(k);
                prefab.gameObject.SetActive(true);
                var dataResource = _script.dataResources.Where(p => p.resourcesName == Resource.Key).Select(p => p).First();
                prefab.GetChild(0).GetComponent<Image>().sprite = dataResource.sprite;
                prefab.GetChild(1).GetComponent<TextMeshProUGUI>().text = dataResource.name;
                prefab.GetChild(2).GetComponent<TextMeshProUGUI>().text = _script.CountCreate.ToString()+" per s";
                prefab.GetChild(3).GetComponent<TextMeshProUGUI>().text = Resource.Value.ToString();
                k++;
            }
        }
        private void PrintStatsImprovement()
        {
            //_IconData.sprite = dataImprovement;
            _ImprovmentPrefStatsName.text = "Скорость добычи";
            _ImprovmentPrefStatsLVL.text = _script.Level.ToString() + " LVL";
            _ImprovmentPrefStatsCount.text = _script.CountCreate.ToString() + " per s";
        }
        private void OnButtonImproveCountMax()
        {
            _script.OnIncriseCountPerSecond?.Invoke();
            PrintStatsImprovement();
        }

    }
}