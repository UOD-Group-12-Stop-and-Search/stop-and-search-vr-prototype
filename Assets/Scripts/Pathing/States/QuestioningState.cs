using BeanCore.Unity.ReferenceResolver.Attributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Pathing.States
{
    public class QuestioningState : StateBehaviour
    {
        [BindComponent]
        private NavMeshAgent m_agent;

        [FormerlySerializedAs("m_questioningUi")] [SerializeField]
        private GameObject m_questioningUIPrefab;

        public override void OnSwitchAway(StateBehaviour newBehaviour)
        {

        }

        public override void OnSwitchTo(StateBehaviour oldBehaviour)
        {
            // look at ignoring pitch and roll
            Vector3 cameraPos = Camera.main!.transform.position;
            transform.LookAt(new Vector3(cameraPos.x, transform.position.y, cameraPos.z));
            Instantiate(m_questioningUIPrefab, transform);
        }
    }
}