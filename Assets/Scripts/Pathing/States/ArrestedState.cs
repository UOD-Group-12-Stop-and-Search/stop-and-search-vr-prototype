using BeanCore.Unity.ReferenceResolver.Attributes;
using UnityEngine;
using UnityEngine.AI;

namespace Pathing.States
{
    public class ArrestedState : StateBehaviour
    {
        [BindComponent]
        private NavMeshAgent m_agent = null!;

        [SerializeField]
        private GameObject m_arrestedIconPrefab = null!;

        [SerializeField]
        private Transform m_arrestedIconHost = null!;

        public override void OnSwitchAway(StateBehaviour? newBehaviour)
        {
        }

        public override void OnSwitchTo(StateBehaviour? oldBehaviour)
        {
            RunResolve();
            m_agent.enabled = true;
            
            GameObject target = GameObject.FindWithTag("ArrestedMoveTo");
            m_agent.SetDestination(target.transform.position);

            Instantiate(m_arrestedIconPrefab, m_arrestedIconHost);
        }
    }
}