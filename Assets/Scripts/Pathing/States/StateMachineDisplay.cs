using System;
using BeanCore.Unity.ReferenceResolver;
using BeanCore.Unity.ReferenceResolver.Attributes;
using Unity.VisualScripting;
using UnityEngine;

namespace Pathing.States
{
    [RequireComponent(typeof(StateMachine))]
    public class StateMachineDisplay : ReferenceResolvedBehaviour
    {
        [BindComponent]
        private StateMachine m_stateMachine;

        [SerializeField] 
        private TextMesh m_textMesh;

        public override void Start()
        {
            base.Start();
            m_stateMachine.StateChanged += (_, _) => m_textMesh.text = m_stateMachine.CurrentState.GetType().Name;
        }
    }
}