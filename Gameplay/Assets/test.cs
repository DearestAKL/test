using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(CD());
    }

    float coldTime = 1.0f;
    IEnumerator CD()
    {
        float startTime = Time.time;
        while (startTime + coldTime >= Time.time)
        {
            yield return new WaitForSeconds(0.1f);
        }
    }
}
