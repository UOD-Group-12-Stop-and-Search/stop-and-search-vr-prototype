using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public abstract class NpcResponse : ScriptableObject
    {
        [SerializeField]
        public List<ResponseResult> Results = new List<ResponseResult>();

        [SerializeField]
        public List<DialogueRequirement> Requirements = new List<DialogueRequirement>();

        public abstract string Text { get; }
    }
}