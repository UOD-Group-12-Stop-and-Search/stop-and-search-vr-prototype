using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Pathing.States
{
    public class StateMachine : MonoBehaviour
    {
        private StateBehaviour? m_currentState = null;
        
        public StateBehaviour CurrentState
        {
            get => m_currentState;
            set
            {
                // skip if they're the same instance
                if (m_currentState == value) return;

                if (m_currentState != null)
                {
                    m_currentState.OnSwitchAway(value);
                    m_currentState.enabled = false;
                }

                StateBehaviour oldState = m_currentState;
                m_currentState = value;

                if (m_currentState != null)
                {
                    m_currentState.enabled = true;
                    m_currentState.OnSwitchTo(oldState);
                }

                StateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        [SerializeField]
        private StateBehaviour m_initialBehaviour = null!;

        [CanBeNull] public event EventHandler StateChanged;

        private void Awake()
        {
            foreach (StateBehaviour behaviour in GetComponents<StateBehaviour>())
            {
                behaviour.enabled = false;
            }
        }

        private void Start()
        {
            CurrentState = m_initialBehaviour;
        }
    }
}