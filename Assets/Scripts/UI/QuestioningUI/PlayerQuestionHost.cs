using Dialogue;
using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.QuestioningUI
{
    public class PlayerQuestionHost : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_questionText = null!;

        [SerializeField]
        private Button m_button = null!;

        private QuestioningPanel Host { get; set; } = null!;
        private PlayerQuestion Question { get; set; } = null!;

        public void OnClick()
        {
            Host.OnQuestionClicked(Question);
        }

        public void Init(QuestioningPanel host, PlayerQuestion question)
        {
            Host = host;
            Question = question;
            m_questionText.text = question.ButtonText;
            m_button.onClick.AddListener(OnClick);
            Refresh();
        }

        public void Refresh()
        {
            (bool meetsHardRequirements, bool meetsSoftRequirements) = Host.RequirementsManager.CheckMeetsRequirements(Question.Requirements);
            m_button.gameObject.SetActive(meetsHardRequirements);
            m_button.interactable = meetsSoftRequirements;
        }
    }
}