using System;

using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    public class UIElementRadioButtonGroup : UIElement
    {
        private RadioButtonGroup _group => Element as RadioButtonGroup;

        public override void Initialize(VisualElement visualElement, string[] tagNameList = null)
        {
            base.Initialize(visualElement, tagNameList);
        }

        protected override void Dispose()
        {
            base.Dispose();
        }
    }
}
