using BeanCore.Unity.ReferenceResolver;
using BeanCore.Unity.ReferenceResolver.Attributes;
using UnityEngine;

namespace Pathing
{
    [RequireComponent(typeof(SphereCollider))]
    public class AgentTarget : ReferenceResolvedBehaviour
    {
        [BindComponent]
        private SphereCollider m_sphereCollider;

        public Vector3 GetRandomTargetPosition()
        {
            Vector3 offset = Random.insideUnitSphere * m_sphereCollider.radius;
            // zero out the y offset to stop it from potentially appearing under the map
            offset.y = 0;
            return transform.position + offset;
        }
    }
}