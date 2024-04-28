using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Dialogue/Npc/NpcResponse")]
    public class NpcResponse : ScriptableObject
    {
        [SerializeField]
        public List<ResponseResult> Results = new List<ResponseResult>();

        public List<DialogueRequirement> Requirements = new List<DialogueRequirement>();

        public string Text;
    }
}