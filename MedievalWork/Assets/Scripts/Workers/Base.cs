using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.ResourcesItem;
using Assets.Scripts;
using TMPro;
using System.Linq;

namespace Assets.Scripts.Workers
{
    public class Base : MonoBehaviour, IWorker
    {
        private RectTransform StorageRect;
        private Builds.Storage StorageScripts;
        private RectTransform Mine;
        private Builds.Mining MineScripts;
        private Vector2 Target;
        private RectTransform Pos;
        private GameObject PanelCart;
        public float timeMove { get; protected set; } = 10f;
        private float time = 0.01f;
        private Animator animator;
        private static GameObject ResourcesGroup;
        private List<ResourcesItem.Data> dataResources = new List<ResourcesItem.Data>();
        private Button ButtonClick;
        public delegate void Del();
        public Del OnIncriseCountMax;
        public Del OnUnSelictionCurrentCart;
        public Del OnIncriseVelocity;
        private bool isCurrentCartSelection = false;

        public Dictionary<ResourcesName, int> ResourcesCount{ get;  private set;}
        public int CountMax { get; private set; } = 5;


        private void Start()
        {
            ResourcesCount = new Dictionary<ResourcesName, int>();
            Pos = GetComponent<RectTransform>();
            Target = new Vector2();

            animator = GetComponent<Animator>();
            StorageScripts = Builds.Storage.GetInstanse();
            StorageRect = StorageScripts.GetComponent<RectTransform>();
            Mine = MineScripts.transform.GetComponent<RectTransform>();
            Target = Mine.anchoredPosition;
            if (ResourcesGroup == null) ResourcesGroup = Resources.Load("DelivaryGroup") as GameObject;
            dataResources = Resources.LoadAll<ResourcesItem.Data>("Resources").ToList();
            ButtonClick = GetComponent<Button>();
            ButtonClick.onClick.AddListener(() => OnClick());
            StartCoroutine(Move());
            OnUnSelictionCurrentCart += () => isCurrentCartSelection = false;

        }
        public IEnumerator Move()
        {
            while (Pos.anchoredPosition != Target)
            {
                yield return new WaitForSeconds(time);
                Move();
            }

            ComeToTarget();
            SetTarget();
            StartCoroutine(this.Move());

            void Move()
            {
                SetActiveAnimations();
                Pos.anchoredPosition = Vector2.MoveTowards(Pos.anchoredPosition, Target, timeMove * 10f * Time.fixedDeltaTime);
            }
            void ComeToTarget()
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
                }
                if (PanelCart.transform.parent.gameObject.activeSelf == true && isCurrentCartSelection)
                {
                    ButtonClick.onClick?.Invoke();
                }
            }
            void SetTarget()
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
        }
        private void SetActiveAnimations()
        {
            if (Target.y < Pos.anchoredPosition.y) animator.Play(AnimName.Down.ToString());
            else animator.Play(AnimName.Up.ToString());
        }
        public void PrintDeliveryResources()
        {
            if (ResourcesCount.Count != 0) Instantiate(ResourcesGroup, transform.parent).GetComponent<Delivery>().Init(ResourcesCount, dataResources, Target);
        }
        public void OnClick()
        {
            //Debug.Log("Click");
            isCurrentCartSelection = true;
            UIMenu.OnPrintedCurrent(ResourcesCount, dataResources, this, PanelCart, transform.GetComponent<Image>().sprite);
        }
        public int IncreaseCount(ResourcesName resourcesName, int Count)
        {
            if (Count <= 0) return 0;

            if (ResourcesCount == null) ResourcesCount = new Dictionary<ResourcesName, int>();

            if (ResourcesCount.ContainsKey(resourcesName)) ResourcesCount[resourcesName] += Count;
            else ResourcesCount.Add(resourcesName, Count);

            return Count;
        }
        public int ReduceCount(ResourcesName resourcesName, int Count)
        {
            throw new NotImplementedException();
        }
        public void Init(GameObject PanelCart, Builds.Mining MineScripts)
        {
            this.PanelCart = PanelCart;
            this.MineScripts = MineScripts;
            OnIncriseCountMax += () => CountMax += 5;
            OnIncriseVelocity += () => timeMove += 10;
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