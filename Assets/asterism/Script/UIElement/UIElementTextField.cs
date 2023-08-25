using System;

using UnityEngine.Assertions;
using UnityEngine.UIElements;
using UnityEngine.Events;

namespace Asterism.UI.UIElements
{
    [Serializable]
    public class UIElementTextField : UIElement
    {
        private TextField _textField => Element as TextField;

        public string Label { get => _textField.label; set => _textField.label = value; }
        public string tooltip { get => _textField.tooltip; set => _textField.tooltip = value; }

        public UnityEvent<string> ValueChanged;

        public override void Initialize(VisualElement visualElement, string[] tagNameList = null)
        {
            base.Initialize(visualElement, tagNameList);
            _textField.RegisterValueChangedCallback(HandleCallback);
        }

        private void HandleCallback(ChangeEvent<string> evt)
        {
            ValueChanged?.Invoke(evt.newValue);
        }

        protected override void Dispose()
        {
            base.Dispose();
            _textField.UnregisterValueChangedCallback(HandleCallback);
        }

    }
}
