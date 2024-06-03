using System;
using UnityEngine;

namespace UI
{
    public class MatchHeadsetYawOnStart : MonoBehaviour
    {
        private void Start()
        {
            this.WaitFramesThenExecute(1, () =>
            {
                transform.rotation = Quaternion.Euler(0, Camera.main!.transform.rotation.eulerAngles.y, 0);
            });
        }
    }
}