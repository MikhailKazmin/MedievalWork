using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.ResourcesItem;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace Assets.Scripts.Builds
{
    public class UIMenuBuilds : MonoBehaviour
    {
        private static GameObject prefabResource;
        public static void OnPrintedCurrent(GameObject PanelBuilds, Dictionary<ResourcesName, int> ResourcesCount, List<Data> dataResources, Mining Script)
        {
            if (PanelBuilds.transform.parent.gameObject.activeSelf != true)
            {
                PanelBuilds.SetActive(true);
                PanelBuilds.transform.parent.gameObject.SetActive(true);
                PanelBuilds.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnExitPanelBuild(PanelBuilds, Script));
                PanelBuilds.transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnButtonImproveCountMax(Script, PanelBuilds));
                PrintStatsImprovement(Script, PanelBuilds);
            }
            PrintResourcesInBuild(PanelBuilds, ResourcesCount, dataResources, Script);
            
        }
        private static void PrintResourcesInBuild(GameObject PanelBuilds, Dictionary<ResourcesName, int> ResourcesCount, List<Data> dataResources, Mining Script)
        {
            if (prefabResource == null)
            {
                prefabResource = PanelBuilds.transform.GetChild(2).GetChild(0).gameObject;
            }

            for (int i = 0; i < prefabResource.transform.parent.childCount; i++)
            {
                prefabResource.transform.parent.GetChild(i).gameObject.SetActive(false);
            }
            int k = 0;
            foreach (var Resource in ResourcesCount)
            {
                //Debug.Log(Resource.Key.ToString());
                if (ResourcesCount.Count > k && k != 0 && ResourcesCount.Count > prefabResource.transform.parent.childCount)
                {
                    var qwee = Instantiate(prefabResource, prefabResource.transform.parent);
                }

                var prefab = prefabResource.transform.parent.GetChild(k);
                prefab.gameObject.SetActive(true);
                var dataResource = dataResources.Where(p => p.resourcesName == Resource.Key).Select(p => p).First();
                prefab.GetChild(0).GetComponent<Image>().sprite = dataResource.sprite; //Icon
                prefab.GetChild(1).GetComponent<TextMeshProUGUI>().text = dataResource.name; //name
                prefab.GetChild(2).GetComponent<TextMeshProUGUI>().text = Script.CountCreate.ToString()+" per s"; //Speed
                prefab.GetChild(3).GetComponent<TextMeshProUGUI>().text = Resource.Value.ToString(); //Count
                k++;
            }
        }

        private static void PrintStatsImprovement(Mining Script, GameObject PanelBuilds)
        {
            var ImprovementElement = PanelBuilds.transform.GetChild(3).GetChild(0);
            //ImprovementElement.GetChild(0).GetComponent<Image>().sprite = dataImprovement;
            //ImprovementElement.GetChild(1).GetChild(0).GetComponent<Image>().sprite = dataImprovement; //NameImprovement
            ImprovementElement.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = Script.Level.ToString() + " LVL"; //LVLImprovement
            ImprovementElement.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = Script.CountCreate.ToString() + " per s"; //CountPerSecondImprovement

        }

        private static void OnButtonImproveCountMax(Mining Script, GameObject PanelBuilds)
        {
            Script.OnIncriseCountPerSecond?.Invoke();
            PrintStatsImprovement(Script, PanelBuilds);
        }
        private static void OnExitPanelBuild(GameObject PanelBuilds, Mining Script)
        {
            Script.OnUnSelictionCurrentBuild?.Invoke();
            PanelBuilds.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners(); //Button Exit
            for (int i = 0; i < PanelBuilds.transform.GetChild(3).childCount; i++)
            {
                PanelBuilds.transform.GetChild(3).GetChild(i).GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners(); // Улучшения
            }
            
            PanelBuilds.transform.parent.gameObject.SetActive(false);
            PanelBuilds.SetActive(false);
        }
    }
}