using System;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public struct ResponseResult
    {
        [SerializeField]
        public ResponseLevelSetMode SetMode;
        [SerializeField]
        public string Name;
        [SerializeField]
        public int Value;
    }

    public enum ResponseLevelSetMode
    {
        OVERWRITE,
        ADD,
        LIMIT,
    }
}