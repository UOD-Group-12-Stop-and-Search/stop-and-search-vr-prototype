using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dialogue
{
    public class DialogueManager : SingletonBehaviour<DialogueManager>
    {
        [SerializeField]
        private List<Dialogue> m_dialogues = new List<Dialogue>();

        public Dialogue SelectRandom()
        {
            return m_dialogues.GetRandomElement();
        }
    }
}