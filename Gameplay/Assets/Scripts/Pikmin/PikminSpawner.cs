using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pikmin
{
    //皮克斯出生点
    public class PikminSpawner : MonoBehaviour
    {
        [SerializeField] int spawnNum = 1;
        public float radius = 1;
        public void SpawnPikmin(Pikmin pikmin, ref List<Pikmin> pikminList)
        {
            for (int i = 0; i < spawnNum; i++)
            {
                Pikmin newPikmin = Instantiate(pikmin);
                newPikmin.transform.position = transform.position + (Random.insideUnitSphere * radius);
                pikminList.Add(newPikmin);
            }
        }
    }
}

