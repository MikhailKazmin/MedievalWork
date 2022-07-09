using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

namespace Assets.Scripts.Builds
{
    public class UIMenuBuilds : UIBase
    {
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

        private void Init(GameObject PanelBuilds, Mining Script)
        {
            if (_Name == null && _Exit == null && _MenuObjects == null && _Previous == null && _Next == null)
            {
                _MenuObjects = PanelBuilds.transform.parent;
                _Name = _MenuObjects.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
                _Exit = _MenuObjects.GetChild(0).GetComponent<Button>();
                _Previous = _MenuObjects.GetChild(1).GetChild(2).GetComponent<Button>();
                _Next = _MenuObjects.GetChild(1).GetChild(1).GetComponent<Button>();
            }

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
                Init(Panel, Script as Mining);
                _Panel.gameObject.SetActive(true);
                _MenuObjects.gameObject.SetActive(true);
                _Icon.sprite = Script.gameObject.GetComponent<Image>().sprite;
                _Exit.onClick.AddListener(() => OnExitPanel(Script as Mining));
                _Previous.onClick.AddListener(() => _PreviousBuild(Script as Mining));
                _Next.onClick.AddListener(() => _NextBuild(Script as Mining));
                _Name.text = Script.transform.name;
                _ImprovmentPrefButton.onClick.AddListener(() => OnButtonImproveCountMax(Script as Mining));
                PrintStatsImprovement(Script as Mining);
            }
            PrintResourcesInBuild(Script as Mining);
            _Next.gameObject.SetActive(true);
            _Previous.gameObject.SetActive(true);
        }

        private void _PreviousBuild(Mining _script)
        {
            _Exit.onClick.Invoke();
            var NumberCurrentBuild = _script.transform.GetSiblingIndex();
            if (NumberCurrentBuild > 0) _script.transform.parent.GetChild(--NumberCurrentBuild).GetComponent<Button>().onClick.Invoke();
            else _script.transform.parent.GetChild(_script.transform.parent.childCount-1).GetComponent<Button>().onClick.Invoke();
        }
        private void _NextBuild(Mining _script)
        {
            _Exit.onClick.Invoke();
            var NumberCurrentBuild = _script.transform.GetSiblingIndex();
            if (NumberCurrentBuild < _script.transform.parent.childCount-1) _script.transform.parent.GetChild(++NumberCurrentBuild).GetComponent<Button>().onClick.Invoke();
            else _script.transform.parent.GetChild(0).GetComponent<Button>().onClick.Invoke();
        }
        private void OnExitPanel(Mining _script)
        {
            _script.OnUnSeliction?.Invoke();
            _ImprovmentPref.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
            _Previous.onClick.RemoveAllListeners();
            _Next.onClick.RemoveAllListeners();
            _Exit.onClick.RemoveAllListeners();
            _MenuObjects.gameObject.SetActive(false);
            _Panel.gameObject.SetActive(false);
        }
        private void PrintResourcesInBuild(Mining _script)
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
        private void PrintStatsImprovement(Mining _script)
        {
            //_IconData.sprite = dataImprovement;
            _ImprovmentPrefStatsName.text = "Скорость добычи";
            _ImprovmentPrefStatsLVL.text = _script.Level.ToString() + " LVL";
            _ImprovmentPrefStatsCount.text = _script.CountCreate.ToString() + " per s";
        }
        private void OnButtonImproveCountMax(Mining _script)
        {
            _script.OnIncriseCountPerSecond?.Invoke();
            PrintStatsImprovement(_script);
        }

    }
}