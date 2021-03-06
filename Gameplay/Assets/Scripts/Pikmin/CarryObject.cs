﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

namespace Pikmin
{
    /// <summary>
    /// 搬运物
    /// </summary>
    [SelectionBase]
    public class CarryObject : InteractiveObject
    {
        [SerializeField] private DestinationScript destination;
        private NavMeshAgent agent = default;
        private Coroutine destinationRoutine = default;
        private float originalAgentSpeed;
        private Renderer objectRenderer;
        private Collider collider;
        [SerializeField] [ColorUsage(false, true)] private Color captureColor;

        public override void Init()
        {
            base.Init();
            destination = FindObjectOfType<DestinationScript>();
            objectRenderer = GetComponentInChildren<Renderer>();
            agent = GetComponent<NavMeshAgent>();
            collider = GetComponent<Collider>();
            originalAgentSpeed = agent.speed;
        }


        public override void Interact()
        {
            if (destinationRoutine != null)
                StopCoroutine(destinationRoutine);

            agent.enabled = true;
            destinationRoutine = StartCoroutine(GetInPosition());

            IEnumerator GetInPosition()
            {
                (FindObjectOfType(typeof(PikminManager)) as PikminManager).StartIntetaction(this);

                agent.avoidancePriority = 50;
                agent.isStopped = false;
                agent.SetDestination(destination.Point());
                yield return new WaitUntil(() => agent.IsDone());
                agent.enabled = false;
                collider.enabled = false;

                (FindObjectOfType(typeof(PikminManager)) as PikminManager).FinishInteraction(this);

                //Delete UI
                //if (fractionObject != null)
                //    Destroy(fractionObject);

                //飞船 获取动画
                float time = 1.3f;
                Sequence s = DOTween.Sequence();
                s.AppendCallback(() => destination.StartCapture());//飞船 开始捕获
                s.Append(objectRenderer.material.DOColor(captureColor, "_EmissionColor", time));//搬运物 颜色改变
                s.Join(transform.DOMove(destination.transform.position, time).SetEase(Ease.InQuint));//搬运物 移动
                s.Join(transform.DOScale(0, time).SetEase(Ease.InQuint));//搬运物 尺寸变小
                s.AppendCallback(() => destination.FinishCapture());//飞船 完成捕获
                s.Append(destination.transform.DOPunchScale(-Vector3.one, .5f, 10, 1));//飞船 动画
            }
        }

        public override void UpdateSpeed(int extra)
        {
            agent.speed = (extra > 0) ? originalAgentSpeed + (extra * .2f) : originalAgentSpeed;
        }

        public override void StopInteract()
        {
            agent.avoidancePriority = 30;
            agent.isStopped = true;
            if (destinationRoutine != null)
                StopCoroutine(destinationRoutine);
        }

        private void Update()
        {
            //if(fractionObject != null)
            //    fractionObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + uiOffset);
        }
    }
}
