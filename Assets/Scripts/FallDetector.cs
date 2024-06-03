using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class FallDetector : MonoBehaviour
{
    [SerializeField]
    private float m_threshold = 10f;

    private void Start()
    {
        StartCoroutine(FallCheck());
    }

    private IEnumerator FallCheck()
    {
        while (true)
        {
            if (transform.position.y < m_threshold)
            {
                transform.position = Vector3.zero;
                yield return new WaitForSeconds(2);
            }

            yield return null;
        }
    }
}