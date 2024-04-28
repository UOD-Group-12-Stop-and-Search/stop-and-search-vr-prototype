using System;
using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    public T Instance { get; private set; }

    private void OnEnable()
    {
        if (Instance != null)
        {
            Debug.LogError($"Ruh Roh! An instance of type {typeof(T)} already exists!");
        }
        Instance = (T)this;
    }
}