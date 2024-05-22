using System;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class LazyFollowHeadsetYaw : MonoBehaviour
    {
        private const int HISTORY_LENGTH = 120;

        private readonly RollingArray<Quaternion> m_headRotations = new RollingArray<Quaternion>(HISTORY_LENGTH);
        private readonly RollingArray<Vector3> m_headPositions = new RollingArray<Vector3>(HISTORY_LENGTH);
        private Camera m_camera = null!;

        private void Start()
        {
            m_camera = Camera.main!;
        }

        private void LateUpdate()
        {
            m_headPositions.Add(m_camera.gameObject.transform.position);
            m_headRotations.Add(m_camera.gameObject.transform.rotation);

            Vector3 averagePosition = GetAveragePosition();
            Quaternion averageRotation = GetAverageRotation();

            transform.SetPositionAndRotation(averagePosition, averageRotation);
        }

        private Vector3 GetAveragePosition()
        {
            Vector3 acc = Vector3.zero;

            foreach (Vector3 position in m_headPositions)
            {
                acc += position;
            }

            return acc / m_headPositions.Count;
        }

        private Quaternion GetAverageRotation()
        {
            float averageWeight = 1f / m_headRotations.Count ;

            Quaternion average = Quaternion.identity;
            foreach (Quaternion rotation in m_headRotations)
            {
                average *= Quaternion.Slerp (Quaternion.identity, rotation, averageWeight);
            }

            return average;
        }
    }
}