using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dialogue;
using Dialogue.InvokableResponses;
using Pathing;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.QuestioningUI
{
    public class QuestioningPanel : MonoBehaviour
    {
        private bool m_hasExited = false;

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

        public CrowdAgent Host { get; private set; } = null!;

        [SerializeField]
        private Dialogue.Dialogue? m_initialDialogue;

        public UnityEvent QuestioningEnding = new UnityEvent();

        private void Start()
        {
            Host = GetComponentInParent<CrowdAgent>();

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
            if (m_hasExited)
                return;

            ClearPanel();
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

            foreach (TargetedText targetedText in dialogue.StartingText)
            {
                ReportConversationText(targetedText.PossibleTextEntries.GetRandomElement(), targetedText.ConversationSide);
            }

            // force rebuild the ui after a couple frames
            this.WaitFramesThenExecute(2, () =>
            {
                foreach (Transform questionHostHost in m_questionHostHosts)
                {
                    LayoutRebuilder.MarkLayoutForRebuild((questionHostHost as RectTransform)!);
                }
            });
        }

        public void ClearPanel()
        {
            if (m_hasExited)
                return;

            // clear existing panel
            m_questionHosts.ForEach(h => Destroy(h.gameObject));
            m_questionHosts.Clear();

            // set next question host to default
            m_currentQuestionHostHost = 0;

            for (int i = 0; i < m_conversationHost.childCount; i++)
            {
                Destroy(m_conversationHost.GetChild(i).gameObject);
            }
        }

        public void ExitQuestioning()
        {
            m_hasExited = true;
            QuestioningEnding.Invoke();
        }

        public void OnQuestionClicked(PlayerQuestion question)
        {
            // get a random response that meets its requirements
            NpcResponse? validResponse;
            NpcResponse[] responses = question.Responses.Where(r => RequirementsManager.CheckMeetsRequirements(r.Requirements)).ToArray();
            if (responses.Length == 0)
            {
                validResponse = null;
            }
            else
            {
                // get first from valid
                validResponse = responses.First();
            }

            // pre question action
            question.PreInvokeAction(this);

            if (m_hasExited)
                return;

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
            question.PostInvokeAction(this);

            if (m_hasExited)
                return;

            // refresh all question buttons
            m_questionHosts.ForEach((h) => h.Refresh());

            // force scroll to bottom after a short delay
            // ensures that everything works with the layout jank at the end of ReportConversationText
            this.WaitFramesThenExecute(4, () =>
            {
                if (m_conversationScrollView != null)
                    m_conversationScrollView.normalizedPosition = Vector2.zero;
            });
        }

        private void ReportConversationText(string text, ConversationSide conversationSide)
        {
            if (m_hasExited)
                return;

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
            this.WaitFramesThenExecute(2, () =>
            {
                if (instance == null)
                    return;
                LayoutRebuilder.MarkLayoutForRebuild((RectTransform)instance.transform);
            });
        }
    }
}