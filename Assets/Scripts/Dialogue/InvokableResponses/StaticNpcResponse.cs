using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Dialogue.InvokableResponses
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Dialogue/Npc/NpcResponse", order = 0)]
    public class StaticNpcResponse : NpcResponse
    {
        [FormerlySerializedAs("Text")]
        [SerializeField]
        private string m_text = String.Empty;

        public override string Text { get => m_text; }
    }
}