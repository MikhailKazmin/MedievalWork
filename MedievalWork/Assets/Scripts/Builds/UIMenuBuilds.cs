using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

namespace Assets.Scripts.Builds
{
    public class UIMenuBuilds : UIBase
    {
        private Image _Icon;
        private Button _OnClickToAdd;

        private Transform _Data;
        private Transform _DataPref;
        private Image _IconDataPref;
        private TextMeshProUGUI _NameDataPref;
        private TextMeshProUGUI _SpeedDataPref;
        private TextMeshProUGUI _CountDataPref;

        private Transform _Improvments; 
        private Transform _ImprovmentPrefCountCreateBuild;
        private Transform _ImprovmentPrefStatsCountCreateBuild;
        private TextMeshProUGUI _ImprovmentPrefStatsNameCountCreateBuild;
        private TextMeshProUGUI _ImprovmentPrefStatsLVLCountCreateBuild;
        private TextMeshProUGUI _ImprovmentPrefStatsCountCreateBuildCount;
        private Button _ImprovmentPrefButtonCountCreateBuild;
        private TextMeshProUGUI _ImprovmentPrefButtonCountCreateBuildCost;

        private Transform _ImprovmentPrefOnClick;
        private Transform _ImprovmentPrefStatsOnClick;
        private TextMeshProUGUI _ImprovmentPrefStatsNameOnClick;
        private TextMeshProUGUI _ImprovmentPrefStatsLVLOnClick;
        private TextMeshProUGUI _ImprovmentPrefStatsCountOnClick;
        private Button _ImprovmentPrefButtonOnClick;
        private TextMeshProUGUI _ImprovmentPrefButtonOnClickCost;

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
            _OnClickToAdd = _Icon.transform.GetChild(0).GetComponent<Button>();
            _Data = _Panel.GetChild(1);
            _DataPref = _Data.GetChild(0);
            _IconDataPref = _DataPref.GetChild(0).GetComponent<Image>();
            _NameDataPref = _DataPref.GetChild(1).GetComponent<TextMeshProUGUI>();
            _SpeedDataPref = _DataPref.GetChild(2).GetComponent<TextMeshProUGUI>();
            _CountDataPref = _DataPref.GetChild(3).GetComponent<TextMeshProUGUI>();

            _Improvments = _Panel.GetChild(2);
            _ImprovmentPrefCountCreateBuild = _Improvments.GetChild(0);
            _ImprovmentPrefStatsCountCreateBuild = _ImprovmentPrefCountCreateBuild.GetChild(1);
            _ImprovmentPrefStatsNameCountCreateBuild = _ImprovmentPrefStatsCountCreateBuild.GetChild(0).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefStatsLVLCountCreateBuild = _ImprovmentPrefStatsCountCreateBuild.GetChild(1).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefStatsCountCreateBuildCount = _ImprovmentPrefStatsCountCreateBuild.GetChild(2).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefButtonCountCreateBuild = _ImprovmentPrefCountCreateBuild.GetChild(2).GetComponent<Button>();
            _ImprovmentPrefButtonCountCreateBuildCost = _ImprovmentPrefButtonCountCreateBuild.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefButtonCountCreateBuildCost.text = "1";

            _ImprovmentPrefOnClick = _Improvments.GetChild(1);
            _ImprovmentPrefStatsOnClick = _ImprovmentPrefOnClick.GetChild(1);
            _ImprovmentPrefStatsNameOnClick = _ImprovmentPrefStatsOnClick.GetChild(0).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefStatsLVLOnClick = _ImprovmentPrefStatsOnClick.GetChild(1).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefStatsCountOnClick = _ImprovmentPrefStatsOnClick.GetChild(2).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefButtonOnClick = _ImprovmentPrefOnClick.GetChild(2).GetComponent<Button>();
            _ImprovmentPrefButtonOnClickCost = _ImprovmentPrefButtonOnClick.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _ImprovmentPrefButtonOnClickCost.text = "1";
        }
        public void OnPrintedCurrent(GameObject Panel, Base Script)
        {
            if (Script.isBuildSelection)
            {

            }
            if (Panel.transform.parent.gameObject.activeSelf != true)
            {
                Init(Panel, Script as Mining);
                _Panel.gameObject.SetActive(true);
                _MenuObjects.gameObject.SetActive(true);
                _Icon.sprite = Script.gameObject.GetComponent<Image>().sprite;
                _Exit.onClick.AddListener(() => OnExitPanel(Script as Mining));
                _Previous.onClick.AddListener(() => _PreviousBuild(Script as Mining));
                _Next.onClick.AddListener(() => _NextBuild(Script as Mining));
                _OnClickToAdd.onClick.AddListener(() => AddResourcesCountFromClick(Script as Mining));
                _Name.text = Script.transform.name;
                _ImprovmentPrefButtonCountCreateBuild.onClick.AddListener(() => OnButtonImproveCountMax(Script as Mining));
                _ImprovmentPrefButtonOnClick.onClick.AddListener(() => OnButtonImproveCountFromClick(Script as Mining));
                PrintStatsImprovementCountCreate(Script as Mining);
                PrintStatsImprovementCountFromClick(Script as Mining);
            }
            PrintResourcesInBuild(Script as Mining);
            _Next.gameObject.SetActive(true);
            _Previous.gameObject.SetActive(true);
        }
        private void AddResourcesCountFromClick(Mining Script)
        {
            Script.IncreaseCountFromClick(Script.CountCreateFromOnClick);
            PrintResourcesInBuild(Script);
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
            _ImprovmentPrefButtonCountCreateBuild.onClick.RemoveAllListeners();
            _ImprovmentPrefButtonOnClick.onClick.RemoveAllListeners();
            _Previous.onClick.RemoveAllListeners();
            _Next.onClick.RemoveAllListeners();
            _Exit.onClick.RemoveAllListeners();
            _OnClickToAdd.onClick.RemoveAllListeners();
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
        private void PrintStatsImprovementCountCreate(Mining _script)
        {
            //_IconData.sprite = dataImprovement;
            _ImprovmentPrefStatsNameCountCreateBuild.text = "Скорость добычи";
            _ImprovmentPrefStatsLVLCountCreateBuild.text = _script.LevelCountCreate.ToString() + " LVL";
            _ImprovmentPrefStatsCountCreateBuildCount.text = _script.CountCreate.ToString() + " в секунду";
        }

        private void PrintStatsImprovementCountFromClick(Mining _script)
        {
            //_IconData.sprite = dataImprovement;
            _ImprovmentPrefStatsNameOnClick.text = "Количество за клик";
            _ImprovmentPrefStatsLVLOnClick.text = _script.LevelCountCreateFromOnClick.ToString() + " LVL";
            _ImprovmentPrefStatsCountOnClick.text = _script.CountCreateFromOnClick.ToString() + " за клик";
        }
        private void OnButtonImproveCountMax(Mining _script)
        {
            _script.OnIncriseCountPerSecond?.Invoke();
            PrintStatsImprovementCountCreate(_script);
        }
        private void OnButtonImproveCountFromClick(Mining _script)
        {
            _script.OnIncriseCountFromOnClick?.Invoke();
            PrintStatsImprovementCountFromClick(_script);
        }
    }
}