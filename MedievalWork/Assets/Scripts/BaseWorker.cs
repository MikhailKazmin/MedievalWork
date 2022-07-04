using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class BaseWorker : MonoBehaviour, IWorker
    {

        [SerializeField] private RectTransform Storage;
        [SerializeField] private RectTransform Mine;
        [SerializeField] private Vector2 Target;
        [SerializeField] private RectTransform Pos;

        [SerializeField] private float timeMove = 1f;
        [SerializeField] private float time = 0.5f;
        [SerializeField] private float speed = 100f;

        [SerializeField] private Animator animator;
        public Dictionary<ResourcesName, int> ResourcesCount
        {
            get => ResourcesCount;
            private set => ResourcesCount = value;
        }

        private void Awake()
        {
            Pos = GetComponent<RectTransform>();
            Target = new Vector2();
            Target = Storage.anchoredPosition;
            StartCoroutine(MoveWorker());
            animator = GetComponent<Animator>();
        }
        private IEnumerator MoveWorker()
        {
            while (Pos.anchoredPosition != Target)
            {
                yield return new WaitForSeconds(time);
                Move();
            }
            SetTarget();
            StartCoroutine(MoveWorker());
        }
        public void Move()
        {
            SetActiveAnimations();
            Pos.anchoredPosition = Vector2.MoveTowards(Pos.anchoredPosition, Target, timeMove * Time.fixedDeltaTime * speed);
        }
        private void SetActiveAnimations()
        {
            if (Target == Storage.anchoredPosition)
            {
                animator.Play(AnimName.Down.ToString());
            }
            else animator.Play(AnimName.Up.ToString());
        }
        public void OnClick()
        {
            throw new System.NotImplementedException();
        }
        public void SetTarget()
        {
            if (Pos.anchoredPosition == Mine.anchoredPosition)
            {
                Target = Storage.anchoredPosition;
            }
            else
            {
                Target = Mine.anchoredPosition;
            }
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