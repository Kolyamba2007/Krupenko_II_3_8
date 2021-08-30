using System;
using UnityEngine;

namespace Ziggurat.UI
{
    [Serializable]
    public class ProgressBar
    {
        private float _value = 0f;

        public string ProductName { set; get; } = "Юнит";
        public float Value => _value;

        public void SetValue(float value)
        {
            _value = Mathf.Clamp01(value);
        }
    }
}