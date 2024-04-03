using System;
using System.Collections;
using BeanCore.Unity.ReferenceResolver;
using BeanCore.Unity.ReferenceResolver.Attributes;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Pathing
{
    public class NavAgentConstantFollow : ReferenceResolvedBehaviour
    {
        [BindComponent]
        private NavMeshAgent m_agent;

        public Transform Target;
        public float RepathSecondsInterval = 1f;

        public override void Start()
        {
            base.Start();
            StartCoroutine(SetTargetLoop());
        }

        private IEnumerator SetTargetLoop()
        {
            while (true)
            {
                if (Target != null)
                {
                    m_agent.SetDestination(Target.position);
                }
                yield return new WaitForSeconds(RepathSecondsInterval);
            }
        }
    }
}