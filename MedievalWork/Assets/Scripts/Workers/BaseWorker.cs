using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Workers
{
    public class BaseWorker : MonoBehaviour, IWorker
    {

        private RectTransform StorageRect;
        private Builds.Storage StorageScripts;
        [SerializeField] private RectTransform Mine;
        private Builds.MineBuild MineScripts;
        [SerializeField] private Vector2 Target;
        private RectTransform Pos;

        private float timeMove = 100f;
        private float time = 0.01f;
        private float speed = 70f;

        private Animator animator;
        private AnimatorController controller;

        private DelivaryResouces delivaryResouces;
        private GameObject ResourcesGroup;
        private Button ButtonClick;

        public Dictionary<ResourcesName, int> ResourcesCount
        {
            get;
            private set;
        }

        private int CountMax = 5;
        public void Init(Builds.MineBuild MineScripts)
        {
            this.MineScripts = MineScripts;
        }
        private void Start()
        {
            Invoke("InitLocal", 1f);
        }
        private void InitLocal()
        {
            ResourcesCount = new Dictionary<ResourcesName, int>();
            Pos = GetComponent<RectTransform>();
            Target = new Vector2();

            animator = GetComponent<Animator>();
            StorageScripts = Builds.Storage.GetInstanse();
            StorageRect = StorageScripts.GetComponent<RectTransform>();
            Mine = MineScripts.transform.GetComponent<RectTransform>();
            Target = Mine.anchoredPosition;
            ResourcesGroup = Resources.Load("DelivaryGroup") as GameObject;
            ButtonClick = GetComponent<Button>();
            ButtonClick.onClick.AddListener(() => OnClick());
            StartCoroutine(MoveWorker());
        }
        private IEnumerator MoveWorker()
        {
            while (Pos.anchoredPosition != Target)
            {
                yield return new WaitForSeconds(time);
                Move();
            }

            ComeToTarget();
            SetTarget();
            StartCoroutine(MoveWorker());
        }
        private void ComeToTarget()
        {
            if (Target == StorageRect.anchoredPosition)
            {
                foreach (var item in ResourcesCount)
                {
                    StorageScripts.IncreaseCount(item.Key, item.Value);
                }
                PrintDeliveryResources();
                ResourcesCount = new Dictionary<ResourcesName, int>();
            }
            else if (Target == Mine.anchoredPosition)
            {
                var Names = MineScripts.GetResoursesName();
                for (int i = 0; i < Names.Length; i++)
                {
                    IncreaseCount(Names[i], MineScripts.ReduceCount(Names[i], CountMax));
                }
                //PrintDeliveryResources();
            }
        }
        public void PrintDeliveryResources()
        {
            var obj = Instantiate(ResourcesGroup, transform.parent);
            var pos = obj.GetComponent<RectTransform>();
            pos.anchoredPosition = Target;
            delivaryResouces = obj.GetComponent<DelivaryResouces>();
            delivaryResouces.Init(ResourcesCount);
            Destroy(obj,1f);
        }
        public void Move()
        {
            SetActiveAnimations();
            Pos.anchoredPosition = Vector2.MoveTowards(Pos.anchoredPosition, Target, timeMove * Time.fixedDeltaTime);
        }
        private void SetActiveAnimations()
        {
            if (Target.y < Pos.anchoredPosition.y)
            {
                animator.Play(AnimName.Down.ToString());
            }
            else animator.Play(AnimName.Up.ToString());
        }
        public void OnClick()
        {
            Debug.Log($"On Click to {this.GetType()}");
            if (ResourcesCount.Count == 0)
            {
                Debug.Log($" Not Resource ");
            }
            else
            {
                string str = "";
                foreach (var item in ResourcesCount)
                {
                    str += $"Resource[{item.Key}] = {item.Value}\n";
                }
                Debug.Log(str);
            }
        }
        public void SetTarget()
        {
            if (Pos.anchoredPosition == Mine.anchoredPosition)
            {
                Target = StorageRect.anchoredPosition;
            }
            else
            {
                Target = Mine.anchoredPosition;
            }
        }
        public int IncreaseCount(ResourcesName resourcesName, int Count)
        {
            if (Count <= 0)
            {
                Debug.Log($"Увеличение на {Count}");
                return 0;
            }

            if (ResourcesCount == null) ResourcesCount = new Dictionary<ResourcesName, int>();
            if (ResourcesCount.ContainsKey(resourcesName))
            {
                ResourcesCount[resourcesName] += Count;
            }
            else
            {
                ResourcesCount.Add(resourcesName, Count);
            }
            return Count;
        }
        public int ReduceCount(ResourcesName resourcesName, int Count)
        {
            throw new NotImplementedException();
        }
        enum AnimName
        {
            Idle,
            Left,
            Right,
            Down,
            Up
        }


    }
}