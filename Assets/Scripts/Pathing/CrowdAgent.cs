using System;
using BeanCore.Unity.ReferenceResolver;
using BeanCore.Unity.ReferenceResolver.Attributes;
using Pathing.States;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

namespace Pathing
{
    [RequireComponent(typeof(UnbotheredWalkingState))]
    public class CrowdAgent : MonoBehaviour
    {
        private static readonly Color[] s_colors = {
            Color.red,
            Color.blue,
            Color.green,
            Color.yellow,
            Color.magenta,
        };

        [BindComponent]
        private StateMachine m_stateMachine;

        [BindComponent]
        private UnbotheredWalkingState m_walkingState;

        [BindComponent]
        private NavMeshAgent m_agent;

        [SerializeField] private MeshRenderer m_bodyRenderer;

        public void InitAgent(Vector3 targetPosition)
        {
            ReferenceResolver.ResolveReferences(this);

            m_bodyRenderer.material.color = s_colors.GetRandomElement();

            m_walkingState.InitMovement(targetPosition);
        }

        public void MatchSpeed(CrowdAgent other)
        {
            m_agent.speed = other.m_agent.speed;
        }
    }
}