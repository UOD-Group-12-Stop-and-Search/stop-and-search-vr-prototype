using System;
using BeanCore.Unity.ReferenceResolver.Attributes;
using UI.QuestioningUI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Pathing.States
{
    public class QuestioningState : StateBehaviour
    {
        private GameObject? m_questioningUiInstance = null;
        private GameObject? m_avoidMeInstance = null;

        [BindComponent]
        private NavMeshAgent m_agent = null!;

        [FormerlySerializedAs("m_questioningUi")] [SerializeField]
        private GameObject m_questioningUIPrefab = null!;

        [SerializeField]
        private Transform m_questioningUiCanvas = null!;

        [SerializeField]
        private GameObject m_avoidMeAreaPrefab = null!;

        public override void OnSwitchAway(StateBehaviour newBehaviour)
        {
            if (m_questioningUiInstance != null)
                Destroy(m_questioningUiInstance);

            if (m_avoidMeInstance != null)
                Destroy(m_avoidMeInstance);

            m_agent.enabled = true;
        }

        public override void OnSwitchTo(StateBehaviour oldBehaviour)
        {
            RunResolve();

            m_questioningUiInstance = Instantiate(m_questioningUIPrefab, m_questioningUiCanvas);
            m_questioningUiInstance.GetComponentInChildren<QuestioningPanel>().QuestioningEnding.AddListener(OnQuestioningEnding);

            // create the avoidMe
            // we don't want it to be parented to this object as it will keep refreshing the navmesh if we do
            m_avoidMeInstance = Instantiate(m_avoidMeAreaPrefab);
            m_avoidMeInstance.transform.position = transform.position;

            // disable interaction with the avoidMe
            m_agent.enabled = false;
        }

        private void OnQuestioningEnding()
        {
            StateMachine.CurrentState = GetComponent<UnbotheredWalkingState>();
        }

        private void Update()
        {
            // look at ignoring pitch and roll
            Vector3 cameraPos = Camera.main!.transform.position;
            transform.LookAt(new Vector3(cameraPos.x, transform.position.y, cameraPos.z));
        }
    }
}