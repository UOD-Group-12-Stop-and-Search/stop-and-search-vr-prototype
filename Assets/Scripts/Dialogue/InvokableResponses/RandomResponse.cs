using System;
using UnityEngine;

namespace Dialogue.InvokableResponses
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Dialogue/Npc/RandomNpcResponse")]
    public class RandomResponse : NpcResponse
    {
        [SerializeField]
        private string[] m_textPossibilities = Array.Empty<string>();

        public override string Text => m_textPossibilities.GetRandomElement();
    }
}