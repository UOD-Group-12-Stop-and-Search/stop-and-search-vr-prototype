using System;
using TMPro;
using UnityEngine;

namespace UI.QuestioningUI
{
    public class ConversationEntry : MonoBehaviour
    {
        [field: SerializeField]
        public TextMeshProUGUI BodyText { get; private set; } = null!;

        [field: SerializeField]
        public TextMeshProUGUI NameText { get; private set; } = null!;
    }
}