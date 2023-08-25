using System;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    [Serializable]
    public sealed class UIElementButton : UIElement
    {
        private Button _button => Element as Button;

        public string Text { get => _button.text; set => _button.text = value; }

        public UnityEngine.Events.UnityEvent OnClick;

        public override void Initialize(VisualElement visualElement, string[] tagNameList = null)
        {
            base.Initialize(visualElement, tagNameList);
            _button.clicked += ButtOnClicked;
        }

        private void ButtOnClicked()
        {
            OnClick?.Invoke();
        }

        protected override void Dispose()
        {
            base.Dispose();
            _button.clicked -= ButtOnClicked;
        }
    }
}