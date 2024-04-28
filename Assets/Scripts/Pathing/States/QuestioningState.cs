using BeanCore.Unity.ReferenceResolver.Attributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Pathing.States
{
    public class QuestioningState : StateBehaviour
    {
        private GameObject? m_questioningUiInstance = null;

        [BindComponent]
        private NavMeshAgent m_agent = null!;

        [FormerlySerializedAs("m_questioningUi")] [SerializeField]
        private GameObject m_questioningUIPrefab = null!;

        [SerializeField]
        private Transform m_questioningUiCanvas = null!;

        public override void OnSwitchAway(StateBehaviour newBehaviour)
        {
            if (m_questioningUiInstance != null)
                Destroy(m_questioningUiInstance);
        }

        public override void OnSwitchTo(StateBehaviour oldBehaviour)
        {
            // look at ignoring pitch and roll
            Vector3 cameraPos = Camera.main!.transform.position;
            transform.LookAt(new Vector3(cameraPos.x, transform.position.y, cameraPos.z));
            m_questioningUiInstance = Instantiate(m_questioningUIPrefab, m_questioningUiCanvas);
        }
    }
}