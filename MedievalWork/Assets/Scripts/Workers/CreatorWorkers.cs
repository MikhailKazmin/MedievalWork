using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers
{
    public class CreatorWorkers : MonoBehaviour
    {
        [SerializeField] private GameObject WorkerObj;

        private List<GameObject> WorkersList = new List<GameObject>();

        private static CreatorWorkers Instanse = null;
        private Builds.CreatorBuilds creatorBuilds;
        public static CreatorWorkers GetInstanse() => Instanse;

        private void Start()
        {

            if (Instanse == null) Instanse = this;
            else if (Instanse == this) Destroy(gameObject);
            creatorBuilds = Builds.CreatorBuilds.GetInstanse();
            for (int i = 0; i < 5; i++)
            {
                var obj = Instantiate(WorkerObj, transform);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                obj.GetComponent<BaseWorker>().Init(creatorBuilds.MinesList[i].GetComponent<Builds.BaseBuild>() as Builds.MineBuild);
                WorkersList.Add(obj);

            }
        }
    }

}