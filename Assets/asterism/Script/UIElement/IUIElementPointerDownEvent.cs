using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    public abstract class IUIElementPointerDownEvent
    {
        protected abstract void Subscribe<PointerDownEvent> (PointerDownEvent eventData);
        protected abstract void UnSubscribe<PointerDownEvent>(PointerDownEvent eventData);
    }
}
