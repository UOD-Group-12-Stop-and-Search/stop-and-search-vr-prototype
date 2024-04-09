using BeanCore.Unity.ReferenceResolver.Attributes;
using UnityEngine.AI;

namespace Pathing.States
{
    public class QuestioningState : StateBehaviour
    {
        [BindComponent]
        private NavMeshAgent m_agent;
        
        public override void OnSwitchAway(StateBehaviour newBehaviour)
        {
            
        }

        public override void OnSwitchTo(StateBehaviour oldBehaviour)
        {
            
        }
    }
}