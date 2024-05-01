using System;
using System.Collections.Generic;
using Dialogue.InvokableResponses;
using UI.QuestioningUI;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Dialogue/Questions/PlayerQuestion", order = 0)]
    public class PlayerQuestion : ScriptableObject
    {
        [SerializeField]
        public List<DialogueRequirement> Requirements = new List<DialogueRequirement>();

        [SerializeField]
        public List<NpcResponse> Responses = new List<NpcResponse>();

        public string ButtonText = String.Empty;
        public string MessageText = String.Empty;

        public virtual void PreInvokeAction(QuestioningPanel panel){}
        public virtual void PostInvokeAction(QuestioningPanel panel){}
    }
}