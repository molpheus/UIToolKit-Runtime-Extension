using System;

using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    [Serializable]
    public class UIElementSlider : UIElement
    {
        private Slider _slider => Element as Slider;

        public float Value { get => _slider.value; set => _slider.value = value; }
        public float HighValue { get => _slider.highValue; set => _slider.highValue = value; }
        public float LowValue { get => _slider.lowValue; set => _slider.lowValue = value; }

        public UnityEvent<float> ValueChanged;

        public override void Initialize(VisualElement visualElement, string[] tagNameList = null)
        {
            base.Initialize(visualElement, tagNameList);
            _slider.RegisterValueChangedCallback(HandleCallback);
        }

        private void HandleCallback(ChangeEvent<float> evt)
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
