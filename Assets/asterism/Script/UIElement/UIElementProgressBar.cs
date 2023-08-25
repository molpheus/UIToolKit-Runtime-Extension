using System;

using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    [Serializable]
    public sealed class UIElementProgressBar : UIElement
    {
        private ProgressBar _progressBar => Element as ProgressBar;

        public float Value { get => _progressBar.value; set => _progressBar.value = value; }
        public float HighValue { get => _progressBar.highValue; set => _progressBar.highValue = value; }
        public float LowValue { get => _progressBar.lowValue; set => _progressBar.lowValue = value; }

        public UnityEvent<float> ValueChanged;


        public override void Initialize(VisualElement visualElement, string[] tagNameList = null)
        {
            base.Initialize(visualElement, tagNameList);
            _progressBar.RegisterValueChangedCallback(HandleCallback);
        }


        private void HandleCallback(ChangeEvent<float> evt)
        {
            ValueChanged?.Invoke(evt.newValue);
        }


        protected override void Dispose()
        {
            base.Dispose();
            _progressBar.UnregisterValueChangedCallback(HandleCallback);
        }

    }
}
