using System;
using BeanCore.Unity.ReferenceResolver;
using BeanCore.Unity.ReferenceResolver.Attributes;
using JetBrains.Annotations;
using UnityEngine;

namespace Pathing.States
{
    [RequireComponent(typeof(StateBehaviour))]
    public abstract class StateBehaviour : ReferenceResolvedBehaviour
    {
        [BindComponent] public StateMachine StateMachine;

        private void Awake()
        {
            enabled = false;
        }

        public abstract void OnSwitchAway([CanBeNull] StateBehaviour newBehaviour);
        public abstract void OnSwitchTo([CanBeNull] StateBehaviour oldBehaviour);
    }
}