using System;
using BeanCore.Unity.ReferenceResolver;
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
        
        public abstract void OnSwitchAway(StateBehaviour newBehaviour);
        public abstract void OnSwitchTo(StateBehaviour oldBehaviour);
    }
}