using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ResourcesItem;

namespace Assets.Scripts.Workers
{
    public class Creator : MonoBehaviour
    {
        [SerializeField] private GameObject WorkerObj;
        [SerializeField] private GameObject PanelCart;
        private List<GameObject> WorkersList = new List<GameObject>();

        private static Creator Instanse = null;
        private Builds.Creator creatorBuilds;
        public static Creator GetInstanse() => Instanse;

        private void Start()
        {

            if (Instanse == null) Instanse = this;
            else if (Instanse == this) Destroy(gameObject);
            creatorBuilds = Builds.Creator.GetInstanse();

            for (int i = 0; i < creatorBuilds.MinesList.Count; i++)
            {
                var obj = Instantiate(WorkerObj, transform);
                obj.name = $"Worker_{i} Is {creatorBuilds.MinesList[i].name}";
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                obj.GetComponent<Base>().Init(PanelCart, creatorBuilds.MinesList[i].GetComponent<Builds.Base>() as Builds.Mining);
                WorkersList.Add(obj);

            }
        }
    }

}