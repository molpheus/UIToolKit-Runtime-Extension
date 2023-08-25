using System;

using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    public class UIElementFoldout : UIElement
    {
        private Foldout _foldout => Element as Foldout;
        public UnityEvent<bool> ValueChanged;

        public override void Initialize(VisualElement visualElement, string[] tagNameList = null)
        {
            base.Initialize(visualElement, tagNameList);
            _foldout.RegisterValueChangedCallback(HandleCallback);
        }

        private void HandleCallback(ChangeEvent<bool> evt)
        {
            ValueChanged?.Invoke(evt.newValue);
        }


        protected override void Dispose()
        {
            base.Dispose();
            _foldout.UnregisterValueChangedCallback(HandleCallback);
        }
    }
}
