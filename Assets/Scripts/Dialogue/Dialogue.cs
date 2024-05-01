using System;
using System.Collections.Generic;
using UI.QuestioningUI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Dialogue/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public List<PlayerQuestion> Questions = new List<PlayerQuestion>();

        public TargetedText[] StartingText = Array.Empty<TargetedText>();

        public List<NameIntPair> StartingValues = new List<NameIntPair>();
    }

    [Serializable]
    public struct TargetedText
    {
        [SerializeField] public ConversationSide ConversationSide;
        [SerializeField] public string[] PossibleTextEntries;
    }
}