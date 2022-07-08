using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ResourcesItem;

namespace Assets.Scripts.Builds
{
    public class Creator : MonoBehaviour
    {
        [SerializeField] private GameObject MineObj;
        [SerializeField] private GameObject PanelBuilds;
        public List<GameObject> MinesList { get; private set; } = new List<GameObject>();
        private static Creator Instanse = null;

        public static Creator GetInstanse() => Instanse;

        private void Awake()
        {
            
            if (Instanse == null) Instanse = this;
            else if (Instanse == this) Destroy(gameObject);


            for (int i = 0; i < 5; i++)
            {
                var obj = Instantiate(MineObj, transform.GetChild(((int)TypeBuilds.Mines)));
                obj.name = $"{TypeBuilds.Mines}_{i}";
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-450f,450f), Random.Range(-1100f, 1100f));
                MinesList.Add(obj);

            }
            MineObj.GetComponent<Mining>().Init(PanelBuilds);
        }


        public enum TypeBuilds
        {
            Storage,
            Mines,
            Productions
        }
    }
}