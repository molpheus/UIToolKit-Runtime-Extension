using System;

using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    public class UIElementEnum : UIElement
    {
        private EnumField _enumField => Element as EnumField;
        public UnityEvent<Enum> ValueChanged;

        public override void Initialize(VisualElement visualElement, string[] tagNameList = null)
        {
            base.Initialize(visualElement, tagNameList);
            _enumField.RegisterValueChangedCallback(HandleCallback);
        }

        private void HandleCallback(ChangeEvent<Enum> evt)
        {
            ValueChanged?.Invoke(evt.newValue);
        }


        protected override void Dispose()
        {
            base.Dispose();
            _enumField.UnregisterValueChangedCallback(HandleCallback);
        }
    }
}
