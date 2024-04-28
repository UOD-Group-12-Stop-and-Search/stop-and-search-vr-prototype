using System;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public struct DialogueRequirement
    {
        [SerializeField]
        public RequirementLevel RequirementLevel;

        [SerializeField]
        public RequirementMode RequirementMode;

        [SerializeField]
        public string Name;

        [SerializeField]
        public int Value;
    }

    public enum RequirementLevel
    {
        /// <summary>
        /// All hard requirements must be met for the requirement to be valid.
        /// </summary>
        HARD,

        /// <summary>
        /// At least one soft requirement must be met for the requirement to be valid <br/>
        /// All hard requirements must also be set.
        /// </summary>
        SOFT,
    }
}