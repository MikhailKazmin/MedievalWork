using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class DelivaryResouces : MonoBehaviour
    {
        private RectTransform Pos;
        private GameObject Resource;
        private List<GameObject> GroupResources = new List<GameObject>();
        //private GameObject ResourcesGroup;

        private void Awake()
        {
            //ResourcesGroup = Resources.Load("DelivaryGroup") as GameObject;
            Pos = GetComponent<RectTransform>();
            //Resource = transform.GetChild(0).gameObject;
            Debug.Log(this.GetType());
        }
        private void Move()
        {
            //Instantiate(ResourcesGroup, transform);
            Pos = GetComponent<RectTransform>();

            while (true)
            {
                Debug.Log(this.GetType());
                //Pos.anchoredPosition = new Vector2(Pos.anchoredPosition.x + 0.1f, Pos.anchoredPosition.y + 0.1f);
                
            }
        }

        private void Update()
        {
            var pos = new Vector2(Pos.anchoredPosition.x + 10f, Pos.anchoredPosition.y + 10f);
            Pos.anchoredPosition = Vector2.MoveTowards(Pos.anchoredPosition, pos, 100f * Time.fixedDeltaTime);
        }
        public void Init(Dictionary<ResourcesName, int> ResourcesCount)
        {
            Pos = GetComponent<RectTransform>();
            Resource = transform.GetChild(0).gameObject;
            if (transform.childCount < ResourcesCount.Count)
            {
                GroupResources.Add(Resource);
                var NumberOfMissingRecources = ResourcesCount.Count - transform.childCount;
                for (int i = 0; i < NumberOfMissingRecources; i++)
                {
                    GroupResources.Add(Instantiate(Resource, transform));
                }
                
            }
            int k = 0;
            foreach (var item in ResourcesCount)
            {
                //GroupResources[0].transform.GetChild(0).GetComponent<Image>().sprite = data.Sprite;
                GroupResources[k].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.Value.ToString();
                k++;
            }
            //Move();
        }
    }
}
