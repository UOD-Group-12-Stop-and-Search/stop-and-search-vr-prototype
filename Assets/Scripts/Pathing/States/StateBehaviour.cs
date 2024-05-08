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
        [HideInInspector]
        [BindComponent] public StateMachine StateMachine = null!;

        private void Awake()
        {
            enabled = false;
        }

        public abstract void OnSwitchAway(StateBehaviour? newBehaviour);
        public abstract void OnSwitchTo(StateBehaviour? oldBehaviour);
    }
}