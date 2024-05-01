using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Dialogue/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public List<PlayerQuestion> Questions = new List<PlayerQuestion>();

        public string[] PotentialStartingText = Array.Empty<string>();

        public List<NameIntPair> StartingValues = new List<NameIntPair>();
    }
}