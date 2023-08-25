using System;

using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    [Serializable]
    public class UIElementSliderInt : UIElement
    {
        private SliderInt _slider => Element as SliderInt;

        public int Value { get => _slider.value; set => _slider.value = value; }
        public int HighValue { get => _slider.highValue; set => _slider.highValue = value; }
        public int LowValue { get => _slider.lowValue; set => _slider.lowValue = value; }

        public UnityEvent<int> ValueChanged;

        public override void Initialize(VisualElement visualElement, string[] tagNameList = null)
        {
            base.Initialize(visualElement, tagNameList);
            _slider.RegisterValueChangedCallback(HandleCallback);
        }

        private void HandleCallback(ChangeEvent<int> evt)
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
