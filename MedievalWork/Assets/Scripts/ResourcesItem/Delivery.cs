using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ell.Resources
{
    public class Delivery : MonoBehaviour
    {
        private RectTransform Pos;
        private GameObject Resource;
        private List<GameObject> Group = new List<GameObject>();
        private List<Data> data = new List<Data>();

        private void Awake() => Destroy(gameObject, 1f);
        private void Update()
        {
            var pos = new Vector2(Pos.anchoredPosition.x + 10f, Pos.anchoredPosition.y + 10f);
            Pos.anchoredPosition = Vector2.MoveTowards(Pos.anchoredPosition, pos, 100f * Time.fixedDeltaTime);
        }
        public void Init(Dictionary<ResourcesName, int> ResourcesCount, List<Data> dataResources, Vector2 PointCreate)
        {
            Pos = GetComponent<RectTransform>();
            Pos.anchoredPosition = PointCreate;
            Resource = transform.GetChild(0).gameObject;
            if (transform.childCount <= ResourcesCount.Count)
            {
                Group.Add(Resource);
                var NumberOfMissingRecources = ResourcesCount.Count - transform.childCount;
                for (int i = 0; i < NumberOfMissingRecources; i++)
                {
                    Group.Add(Instantiate(Resource, transform));
                }
            }
            int k = 0;
            foreach (var item in ResourcesCount)
            {
                var dataResource = dataResources.Where(p => p.resourcesName == item.Key).Select(p => p);
                Group[k].transform.GetChild(0).GetComponent<Image>().sprite = dataResource.First().sprite;
                Group[k].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.Value.ToString();
                k++;
            }
        }
    }
}
