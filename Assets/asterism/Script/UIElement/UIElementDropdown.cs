using System;
using System.Collections.Generic;

using Cysharp.Threading.Tasks.Triggers;

using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    [Serializable]
    public class UIElementDropdown : UIElement
    {
        private DropdownField _dropDown => Element as DropdownField;

        public string Value => _dropDown.value;
        public int Index => _dropDown.index;
        public List<string> Choice => _dropDown.choices;

        public UnityEvent<string> ValueChanged;

        public override void Initialize(VisualElement visualElement, string[] tagNameList = null)
        {
            base.Initialize(visualElement, tagNameList);

            _dropDown.RegisterValueChangedCallback(HandleCallback);
        }

        private void HandleCallback(ChangeEvent<string> evt)
        {
            ValueChanged?.Invoke(evt.newValue);
        }

        protected override void Dispose()
        {
            base.Dispose();

            _dropDown.UnregisterValueChangedCallback(HandleCallback);
        }
    }
}
