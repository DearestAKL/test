using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Pikmin
{
    /// <summary>
    /// 可交互物体
    /// </summary>
    public class InteractiveObject : MonoBehaviour
    {
        [Header("UI")]
        //public GameObject fractionPrefab;
        public Vector3 uiOffset;
        //public Transform canvas;

        [Header("Variables")]
        [SerializeField] private int pikminNeeded = 1;
        public float radius = 1;
        private int currentPikmin = 0;

        [HideInInspector]
        //public GameObject fractionObject;

        private void Awake()
        {
            Init();
        }

        public virtual void Init()
        {
            //fractionObject = Instantiate(fractionPrefab, canvas);
            //fractionObject.SetActive(false);
        }

        public void AssignPikmin()
        {
            currentPikmin++;

            //UI
            //fractionObject.SetActive(true);
            //fractionObject.transform.GetChild(0).DOComplete();
            //fractionObject.transform.GetChild(0).DOPunchScale(Vector3.one, .3f, 10, 1);
            //fractionObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentPikmin.ToString();
            //fractionObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = pikminNeeded.ToString();

            if (currentPikmin == pikminNeeded)
                Interact();

            if (currentPikmin > pikminNeeded)
                UpdateSpeed(currentPikmin - pikminNeeded);
        }

        public void ReleasePikmin()
        {
            if (currentPikmin == 0)
                return;

            currentPikmin--;

            //if (currentPikmin == 0)
            //{
            //    fractionObject.SetActive(false);
            //}
            //else
            //{
            //    fractionObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentPikmin.ToString();
            //    fractionObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = pikminNeeded.ToString();
            //}

            if (currentPikmin < pikminNeeded)
                StopInteract();
        }

        public Vector3 GetPositon()
        {
            float angle = currentPikmin * Mathf.PI * 2f / pikminNeeded;
            return transform.position + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
        }

        //交互
        public virtual void Interact()
        {

        }

        //停止交互
        public virtual void StopInteract()
        {

        }

        //更新速度
        public virtual void UpdateSpeed(int extra)
        {

        }
    }
}
