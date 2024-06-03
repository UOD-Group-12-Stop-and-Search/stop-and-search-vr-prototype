using System;
using BeanCore.Unity.ReferenceResolver;
using BeanCore.Unity.ReferenceResolver.Attributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pathing
{
    [RequireComponent(typeof(SphereCollider))]
    public class AgentTarget : MonoBehaviour
    {
        [BindComponent]
        private SphereCollider m_sphereCollider;

        private void Awake()
        {
            ReferenceResolver.ResolveReferences(this);
        }

        public Vector3 GetRandomTargetPosition()
        {
            Vector3 offset = Random.insideUnitSphere * m_sphereCollider.radius;
            // zero out the y offset to stop it from potentially appearing under the map
            offset.y = 0;
            return transform.position + offset;
        }
    }
}