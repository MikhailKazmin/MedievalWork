using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ResourcesItem;
//using static Assets.Scripts.Workers;

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
            Buy.OnCreateBuildAndWorker += Builds;
        }

        public void Builds()
        {
            var obj = Instantiate(MineObj, transform.GetChild(((int)TypeBuilds.Mines)));
            obj.name = $"{TypeBuilds.Mines}_{MinesList.Count}";
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-450f, 450f), Random.Range(-1100f, 1100f));
            MinesList.Add(obj);

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