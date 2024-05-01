using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
        private Transform[] m_questionHostHosts = null!;
        private int m_currentQuestionHostHost = 0;

        [SerializeField]
        private Transform m_conversationHost = null!;
        [SerializeField]
        private ScrollRect m_conversationScrollView = null!;

        private readonly List<PlayerQuestionHost> m_questionHosts = new List<PlayerQuestionHost>();

        public RequirementsManager RequirementsManager { get; } = new RequirementsManager();

        [SerializeField]
        private Dialogue.Dialogue? m_initialDialogue;

        private void Start()
        {
            if (m_initialDialogue != null)
            {
                PopulatePanel(m_initialDialogue);
            }
            else
            {
                PopulatePanel(DialogueManager.Instance.SelectRandomDialogue());
            }
        }

        public void PopulatePanel(Dialogue.Dialogue dialogue)
        {
            dialogue.StartingValues.ForEach(RequirementsManager.SetValue);
            
            foreach (PlayerQuestion question in dialogue.Questions)
            {
                PlayerQuestionHost questionHost = Instantiate(m_questionHostPrefab, m_questionHostHosts[m_currentQuestionHostHost]).GetComponent<PlayerQuestionHost>();
                questionHost.Init(this, question);
                m_questionHosts.Add(questionHost);

                m_currentQuestionHostHost++;
                if (m_currentQuestionHostHost >= m_questionHostHosts.Length)
                    m_currentQuestionHostHost = 0;
            }

            if (dialogue.PotentialStartingText.Length > 0)
            {
                string text = dialogue.PotentialStartingText.GetRandomElement();
                ReportConversationText(text, ConversationSide.NPC);
            }
        }

        public void OnQuestionClicked(PlayerQuestion question)
        {
            // get a random response that meets its requirements
            NpcResponse? validResponse;
            NpcResponse[] responses = question.Responses.Where(r => RequirementsManager.CheckMeetsRequirements(r.Requirements).All()).ToArray();
            if (responses.Length == 0)
            {
                validResponse = null;
            }
            else
            {
                // get random from valid
                validResponse = responses.GetRandomElement();
            }

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

            // force scroll to bottom for this and the next few frames
            // ensures that everything works with the layout jank at the end of ReportConversationText
            m_conversationScrollView.normalizedPosition = Vector2.zero;
            this.WaitFramesThenExecute(1, () => m_conversationScrollView.normalizedPosition = Vector2.zero);
            this.WaitFramesThenExecute(2, () => m_conversationScrollView.normalizedPosition = Vector2.zero);
            this.WaitFramesThenExecute(3, () => m_conversationScrollView.normalizedPosition = Vector2.zero);
            this.WaitFramesThenExecute(4, () => m_conversationScrollView.normalizedPosition = Vector2.zero);
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
            instance.GetComponent<ConversationEntry>().BodyText.text = text;

            // for some reason the layout fucks up unless this is done like this
            // the 2 frame delay is deliberate
            // 1 and 0 frame delays do nothing
            this.WaitFramesThenExecute(2, () => LayoutRebuilder.MarkLayoutForRebuild((instance.transform as RectTransform)!));
        }
    }

    public enum ConversationSide
    {
        PLAYER,
        NPC,
    }
}