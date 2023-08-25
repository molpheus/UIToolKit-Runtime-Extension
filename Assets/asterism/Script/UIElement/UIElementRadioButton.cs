using System;

using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    public class UIElementRadioButton : UIElement
    {
        private RadioButton _radioButton => Element as RadioButton;

        public UnityEvent<bool> ValueChanged;


        public override void Initialize(VisualElement visualElement, string[] tagNameList = null)
        {
            base.Initialize(visualElement, tagNameList);
            _radioButton.RegisterValueChangedCallback(HandleCallback);
        }


        protected override void Dispose()
        {
            base.Dispose();
            _radioButton.UnregisterValueChangedCallback(HandleCallback);
        }


        private void HandleCallback(ChangeEvent<bool> evt)
        {
            ValueChanged?.Invoke(evt.newValue);
        }
    }
}
