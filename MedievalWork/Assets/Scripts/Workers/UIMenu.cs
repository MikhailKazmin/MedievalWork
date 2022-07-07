using Assets.Scripts.ResourcesItem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Workers
{
    public class UIMenu : MonoBehaviour
    {
        //private GameObject PanelCart;
        public static void OnPrintedCurrent(Dictionary<ResourcesName, int> ResourcesCount, List<ResourcesItem.Data> dataResources, Base Script, GameObject PanelCart, Sprite Cart)
        {
            //PanelCart = PanelCart;
            if (PanelCart.transform.parent.gameObject.activeSelf != true)
            {
                PanelCart.SetActive(true);
                PanelCart.transform.parent.gameObject.SetActive(true);
                PrintCurrentPropertiesCart(Script.CountMax, Cart, PanelCart);
                PanelCart.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnButtonImproveCountMax(Script, PanelCart));
                PanelCart.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener(() => OnButtonImproveVelocity(Script, PanelCart));
                PanelCart.transform.GetChild(2).GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnExitPanelCart(Script, PanelCart));
            }
            PrintCurrentResourcesInCart(ResourcesCount, dataResources, PanelCart);
        }
        private static void PrintCurrentPropertiesCart(int CountMax, Sprite Cart, GameObject PanelCart)
        {
            PanelCart.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Cart; //ObjectCurrent
            PanelCart.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = CountMax.ToString(); //maxCount
            PanelCart.transform.GetChild(2).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = (10).ToString(); //velocity
        }
        private static void PrintCurrentResourcesInCart(Dictionary<ResourcesName, int> ResourcesCount, List<ResourcesItem.Data> dataResources, GameObject PanelCart)
        {
            for (int i = 0; i < PanelCart.transform.GetChild(1).childCount; i++)
            {
                PanelCart.transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
            }
            int k = 0;
            var PrefabResource = PanelCart.transform.GetChild(1).GetChild(0).gameObject;
            var Prefab = PanelCart.transform.GetChild(1);
            foreach (var Resource in ResourcesCount)
            {
                if (ResourcesCount.Count > k && k != 0 && ResourcesCount.Count > PanelCart.transform.GetChild(1).childCount)
                {
                    var qwee = Instantiate(PrefabResource, PrefabResource.transform.parent);
                }
                Prefab.GetChild(k).gameObject.SetActive(true);
                var dataResource = dataResources.Where(p => p.resourcesName == Resource.Key).Select(p => p);
                Prefab.GetChild(k).GetChild(0).GetComponent<Image>().sprite = dataResource.First().sprite; //Icon
                Prefab.GetChild(k).GetChild(1).GetComponent<TextMeshProUGUI>().text = Resource.Value.ToString(); //Count
                k++;
            }
        }
        private static void OnExitPanelCart(Base Script, GameObject PanelCart)
        {
            Script.OnUnSelictionCurrentCart?.Invoke();
            PanelCart.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
            PanelCart.transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
            PanelCart.transform.parent.gameObject.SetActive(false);
            PanelCart.SetActive(false);
        }
        private static void OnButtonImproveCountMax(Base Script, GameObject PanelCart)
        {
            Script.OnIncriseCountMax?.Invoke();
            PanelCart.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = (Script.CountMax).ToString(); //maxCount
        }
        private static void OnButtonImproveVelocity(Base Script, GameObject PanelCart)
        {
            Script.OnIncriseVelocity?.Invoke();
            PanelCart.transform.GetChild(2).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = (Script.timeMove).ToString(); //Velocity
        }
    }
}