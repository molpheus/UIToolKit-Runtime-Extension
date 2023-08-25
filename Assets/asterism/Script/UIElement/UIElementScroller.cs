using System;

using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    [Serializable]
    public sealed class UIElementScroller : UIElement
    {
        private Scroller _scroller => Element as Scroller;

        public float LowValue { get => _scroller.lowValue; set => _scroller.lowValue = value; }
        public float HighValue { get => _scroller.highValue; set => _scroller.highValue = value; }
        public float Value { get => _scroller.value; set => _scroller.value = value; }

        public UnityEvent<float> ValueChanged;

        public override void Initialize(VisualElement visualElement, string[] tagNameList = null)
        {
            base.Initialize(visualElement, tagNameList);
            _scroller.valueChanged += _scroller_valueChanged;
        }

        private void _scroller_valueChanged(float value)
        {
            ValueChanged?.Invoke(value);
        }

        protected override void Dispose()
        {
            base.Dispose();
            _scroller.valueChanged -= _scroller_valueChanged;
        }
    }
}
