using System;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    public class UIElementMinMaxSlider : UIElement
    {
        private MinMaxSlider _slider => Element as MinMaxSlider;

        public float LowerValue => _slider.value.x;
        public float HigherValue => _slider.value.y;
        public Vector2 Value { get => _slider.value; set => _slider.value = value; }
        public float MaxValue { get => _slider.maxValue; set => _slider.maxValue = value; }
        public float minValue { get => _slider.minValue; set => _slider.minValue = value; }

        public UnityEvent<Vector2> ValueChanged;

        public override void Initialize(VisualElement visualElement, string[] tagNameList = null)
        {
            base.Initialize(visualElement, tagNameList);
            _slider.RegisterValueChangedCallback(HandleCallback);
        }

        private void HandleCallback(ChangeEvent<Vector2> evt)
        {
            ValueChanged?.Invoke(evt.newValue);
        }

        protected override void Dispose()
        {
            base.Dispose();
            _slider.UnregisterValueChangedCallback(HandleCallback);
        }
    }
}
