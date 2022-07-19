using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ell.Resources;
using Ell.BaseScripts;

namespace Ell.Builds
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
            var obj = Instantiate(MineObj, transform.GetChild((int)TypeBuilds.Mines));
            obj.name = $"{TypeBuilds.Mines}_{MinesList.Count}";
            //Vector2 pos = new Vector2(Random.Range(-450f, 450f), Random.Range(-1100f, 1100f));
            float angle = Random.Range(0f, 360f);
            float posX = 0 + Mathf.Clamp((MinesList.Count + 1) * 300, -Screen.width/2, Screen.width/2) * Mathf.Cos(angle);
            float posY = 0 + Mathf.Clamp((MinesList.Count + 1) * 50, -Screen.height/2, Screen.height/2) * Mathf.Sin(angle);
            
            Vector2 pos = new Vector2(posX, posY);

            obj.GetComponent<RectTransform>().anchoredPosition = pos;
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