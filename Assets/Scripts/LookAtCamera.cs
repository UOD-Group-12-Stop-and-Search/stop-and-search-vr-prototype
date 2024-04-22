using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform m_camTransform;

    private void Start()
    {
        m_camTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(m_camTransform);
    }
}