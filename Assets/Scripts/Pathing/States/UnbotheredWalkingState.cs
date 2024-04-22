using BeanCore.Unity.ReferenceResolver;
using BeanCore.Unity.ReferenceResolver.Attributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

namespace Pathing.States
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnbotheredWalkingState : StateBehaviour
    {
        [BindComponent]
        private NavMeshAgent m_agent;

        [BindComponent]
        private XRSimpleInteractable m_interactable;

        [SerializeField] private float m_destinationKillDistance = 1f;

        [SerializeField] private MinMaxFloat m_speed;
        [SerializeField] private MinMaxFloat m_scale;

        [Header("runner")]
        [SerializeField] private float m_runnerChance;
        [SerializeField] private MinMaxFloat m_runnerSpeed;

        public override void Start()
        {
            base.Start();
            m_interactable.activated.AddListener((_) => TriggerQuestioning());
        }

        public override void OnSwitchAway(StateBehaviour newBehaviour)
        {
            m_agent.ResetPath();
        }

        public override void OnSwitchTo(StateBehaviour oldBehaviour)
        {
        }

        public void InitMovement(Vector3 targetPosition)
        {
            ReferenceResolver.ResolveReferences(this);

            float scale = m_scale.GetRandomInRange();
            transform.localScale = new Vector3(scale, scale, scale);

            // select if it should use runner speed or not
            m_agent.speed = Random.value <= m_runnerChance ? m_runnerSpeed.GetRandomInRange() : m_speed.GetRandomInRange();

            m_agent.SetDestination(targetPosition);
        }

        private void Update()
        {
            if (m_agent.remainingDistance < m_destinationKillDistance)
            {
                Destroy(gameObject);
            }
        }

        public void TriggerQuestioning()
        {
            StateMachine.CurrentState = GetComponent<QuestioningState>();
        }
    }
}