using System;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public struct NameIntPair
    {
        [SerializeField]
        public string Name;
        
        [SerializeField]
        public int Value;
    }
}