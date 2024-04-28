using System;
using System.Collections.Generic;
using System.Linq;
using Dialogue;
using TMPro;
using UnityEngine;

namespace UI.QuestioningUI
{
    public class QuestioningPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_npcConversationTextPrefab = null!;

        [SerializeField]
        private GameObject m_playerConversationTextPrefab = null!;

        [SerializeField]
        private GameObject m_questionHostPrefab = null!;

        [SerializeField]
        private Transform m_questionHostHost = null!;

        [SerializeField]
        private Transform m_conversationHost = null!;

        private readonly List<PlayerQuestionHost> m_questionHosts = new List<PlayerQuestionHost>();

        public RequirementsManager RequirementsManager { get; } = new RequirementsManager();

        public void PopulatePanel(Dialogue.Dialogue dialogue)
        {
            foreach (PlayerQuestion question in dialogue.Questions)
            {
                PlayerQuestionHost questionHost = Instantiate(m_questionHostPrefab, m_questionHostHost).GetComponent<PlayerQuestionHost>();
                questionHost.Init(this, question);
                m_questionHosts.Add(questionHost);
            }
        }

        public void OnQuestionClicked(PlayerQuestion question)
        {
            // get the first response that meets its requirements
            NpcResponse? validResponse = question.Responses.FirstOrDefault(r => RequirementsManager.CheckMeetsRequirements(r.Requirements));

            // pre question action
            question.PreInvokeAction();

            // report player question text
            ReportConversationText(question.MessageText, ConversationSide.PLAYER);

            // if no valid response was found then let the player know?
            // not sure what should happen here
            if (validResponse == null)
            {
                ReportConversationText("No responses to the question passed validation. This NPC cannot provide a response", ConversationSide.NPC);
            }
            else
            {
                // report npc response text
                ReportConversationText(validResponse.Text, ConversationSide.NPC);

                // apply requirement changes
                validResponse.Results.ForEach(RequirementsManager.ReportRequirement);
            }

            // post question action
            question.PostInvokeAction();

            // refresh all question buttons
            m_questionHosts.ForEach((h) => h.Refresh());
        }

        private void ReportConversationText(string text, ConversationSide conversationSide)
        {
            GameObject prefab;
            switch (conversationSide)
            {
                case ConversationSide.PLAYER:
                    prefab = m_playerConversationTextPrefab;
                    break;
                case ConversationSide.NPC:
                    prefab = m_npcConversationTextPrefab;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(conversationSide), conversationSide, null);
            }

            GameObject instance = Instantiate(prefab, m_conversationHost);
            instance.GetComponent<TextMeshProUGUI>().text = text;
        }
    }

    public enum ConversationSide
    {
        PLAYER,
        NPC,
    }
}