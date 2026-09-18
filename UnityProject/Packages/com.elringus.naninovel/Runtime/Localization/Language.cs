using System;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    public struct Language
    {
        public string Tag => tag;
        public string Name => name;

        [SerializeField] private string tag;
        [SerializeField] private string name;

        public Language (string tag, string name)
        {
            this.tag = tag;
            this.name = name;
        }
    }
}
