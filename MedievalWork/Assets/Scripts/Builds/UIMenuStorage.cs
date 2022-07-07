using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.ResourcesItem;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace Assets.Scripts.Builds
{
    public class UIMenuStorage : MonoBehaviour
    {
        private static GameObject prefabResource;
        public static void OnPrintedCurrent(GameObject PanelStorage, Dictionary<ResourcesName, int> ResourcesCount, List<Data> dataResources)
        {
            if (PanelStorage.transform.parent.gameObject.activeSelf != true)
            {
                PanelStorage.SetActive(true);
                PanelStorage.transform.parent.gameObject.SetActive(true);
                PanelStorage.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnExitPanelStorage(PanelStorage));
                
                //    PanelCart.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnButtonImproveCountMax(Script, PanelCart));

            }
            PrintResourcesInStorage(PanelStorage, ResourcesCount, dataResources);
            //PrintCurrentResourcesInCart(ResourcesCount, dataResources, PanelCart);
        }
        private static void PrintResourcesInStorage(GameObject PanelStorage, Dictionary<ResourcesName, int> ResourcesCount, List<Data> dataResources)
        {
            if (prefabResource == null)
            {
                prefabResource = PanelStorage.transform.GetChild(0).GetChild(1).GetChild(0).gameObject;
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
                prefab.GetChild(2).GetComponent<TextMeshProUGUI>().text = Resource.Value.ToString(); //Count
                prefab.GetChild(3).GetComponent<TextMeshProUGUI>().text = dataResource.Cost.ToString()+"$"; //Cost
                k++;
            }
        }
        private static void OnExitPanelStorage(GameObject PanelStorage)
        {
            PanelStorage.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
            PanelStorage.transform.parent.gameObject.SetActive(false);
            PanelStorage.SetActive(false);
        }
    }
}