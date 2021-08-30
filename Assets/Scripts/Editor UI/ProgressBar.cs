using System;
using UnityEngine;

namespace Ziggurat.UI
{
    [Serializable]
    public class ProgressBar
    {
        private float _value = 0f;

        public string ProductName { set; get; } = "Юнит";
        public float Value
        {
            set => _value = Mathf.Clamp01(value);
            get => _value;
        }
    }
}