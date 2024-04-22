using UnityEngine;

public class WorldCanvasCameraProvider : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}