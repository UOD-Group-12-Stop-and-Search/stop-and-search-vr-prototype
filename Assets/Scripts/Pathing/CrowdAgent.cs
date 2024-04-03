using System;
using BeanCore.Unity.ReferenceResolver;
using BeanCore.Unity.ReferenceResolver.Attributes;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Pathing
{
    public class CrowdAgent : MonoBehaviour
    {
        private static readonly Color[] s_colors = new Color[]
        {
            Color.red,
            Color.blue,
            Color.green,
            Color.yellow,
            Color.magenta,
        };

        [BindComponent]
        private NavMeshAgent m_agent;

        [SerializeField] private MeshRenderer m_bodyRenderer;
        [SerializeField] private float m_destinationKillDistance = 1f;

        [SerializeField] private MinMaxFloat m_speed;
        [SerializeField] private MinMaxFloat m_scale;

        [Header("runner")]
        [SerializeField] private float m_runnerChance;
        [SerializeField] private MinMaxFloat m_runnerSpeed;

        public void InitAgent(Vector3 targetPosition)
        {
            ReferenceResolver.ResolveReferences(this);

            float scale = m_scale.GetRandomInRange();
            transform.localScale = new Vector3(scale, scale, scale);

            // select if it should use runner speed or not
            m_agent.speed = Random.value <= m_runnerChance ? m_runnerSpeed.GetRandomInRange() : m_speed.GetRandomInRange();

            m_agent.SetDestination(targetPosition);

            m_bodyRenderer.material.color = s_colors.GetRandomElement();
        }

        private void Update()
        {
            if (m_agent.remainingDistance < m_destinationKillDistance)
            {
                Destroy(gameObject);
            }
        }

        public void MatchSpeed(CrowdAgent other)
        {
            m_agent.speed = other.m_agent.speed;
        }
    }
}