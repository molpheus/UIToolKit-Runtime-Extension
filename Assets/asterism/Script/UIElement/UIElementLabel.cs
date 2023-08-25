using System;

using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    [Serializable]
    public sealed class UIElementLabel : UIElement
    {
        private Label _label => Element as Label;

        public string Text { get => _label.text; set => _label.text = value; }

        public override void Initialize(VisualElement visualElement, string[] tagNameList = null)
        {
            base.Initialize(visualElement, tagNameList);
        }

        protected override void Dispose()
        {
            
        }
    }
}
