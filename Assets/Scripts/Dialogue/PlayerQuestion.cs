using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Dialogue/PlayerQuestion")]
    public class PlayerQuestion : ScriptableObject
    {
        [SerializeField]
        public List<DialogueRequirement> Requirements = new List<DialogueRequirement>();

        [SerializeField]
        public List<NpcResponse> Responses = new List<NpcResponse>();

        public string ButtonText = String.Empty;
        public string MessageText = String.Empty;

        public virtual void PreInvokeAction(){}
        public virtual void PostInvokeAction(){}
    }
}