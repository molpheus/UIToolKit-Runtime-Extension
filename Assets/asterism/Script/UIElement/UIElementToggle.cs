using System;

using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    [Serializable]
    public sealed class UIElementToggle : UIElement
    {
        private Toggle _toggle;

        public string Label { get => _toggle.label; set => _toggle.label = value; }
        public bool Value { get => _toggle.value; set => _toggle.value = value; }

        public UnityEvent<bool> ValueChanged;

        public override void Initialize(VisualElement visualElement, string[] tagNameList = null)
        {
            base.Initialize(visualElement, tagNameList);
            _toggle.RegisterValueChangedCallback(HandleCallback);
        }

        private void HandleCallback(ChangeEvent<bool> evt)
        {
            ValueChanged?.Invoke(evt.newValue);
        }

        protected override void Dispose()
        {
            base.Dispose();
            _toggle.UnregisterValueChangedCallback(HandleCallback);
        }
    }
}
