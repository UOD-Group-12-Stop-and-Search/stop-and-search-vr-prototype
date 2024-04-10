using System;
using BeanCore.Unity.ReferenceResolver;
using JetBrains.Annotations;
using UnityEngine;

namespace Pathing.States
{
    [RequireComponent(typeof(StateBehaviour))]
    public abstract class StateBehaviour : ReferenceResolvedBehaviour
    {
        private void Awake()
        {
            enabled = false;
        }
        
        public abstract void OnSwitchAway([CanBeNull] StateBehaviour newBehaviour);
        public abstract void OnSwitchTo([CanBeNull] StateBehaviour oldBehaviour);
    }
}