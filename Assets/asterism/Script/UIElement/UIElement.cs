using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    public class UIElement : IUIElementAttribute
    {
        public string[] TagNameList { get => _tagNameList; set => _tagNameList = value; }
        [SerializeField]
        protected string[] _tagNameList;
        protected VisualElement Element { get; set; }

        public virtual void Initialize(VisualElement visualElement, string[] tagNameList = null)
        {
            if (tagNameList is not null)
                _tagNameList = tagNameList;

            Element = visualElement.SearchElement(_tagNameList);

            Assert.IsNotNull(Element);
        }

        protected virtual void Dispose()
        { }

        protected void Register<TEventType>(EventCallback<TEventType> callback) where TEventType : EventBase<TEventType>, new()
        {
            Element.RegisterCallback(callback);

            //if (_eventList.ContainsKey(typeof(TEventType)))
            //{
            //    (_eventList[typeof(TEventType)] as List<EventCallback<TEventType>>).Add(callback);
            //}
            //else
            //{
            //    var list = new List<EventCallback<TEventType>>() {
            //        callback
            //    };
            //    _eventList.Add(typeof(TEventType), list);
            //}

            //List<TEventType> types;
        }

        protected void UnRegister<TEventType>(EventCallback<TEventType> callback) where TEventType : EventBase<TEventType>, new()
        {
            //if ( !_eventList.ContainsKey(typeof(TEventType)) )
            //{
            //    return;
            //}

            //var list = (_eventList[typeof(TEventType)] as List<EventCallback<TEventType>>);
            //foreach(var item in list)
            //{
            //    if (item == callback)
                    Element.UnregisterCallback(callback);
            //}

            //_eventList.Remove(typeof(TEventType));
        }


        /// <summary>
        /// Event sent after an element is added to an element that is a descendent of a panel.
        /// </summary>
        public void RegisterAttachToPanel(EventCallback<AttachToPanelEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent after an element is added to an element that is a descendent of a panel.
        /// </summary>
        public void UnRegisterAttachToPanel(EventCallback<AttachToPanelEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Event sent immediately, after an element has lost foucs.
        /// This event trickles down,
        /// it does not bubble up, and it cannnot be canceled.
        /// </summary>
        public void RegisterBlurEvent(EventCallback<BlurEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent immediately, after an element has lost foucs.
        /// This event trickles down,
        /// it does not bubble up, and it cannnot be canceled.
        /// </summary>
        public void UnRegisterBlurEvent(EventCallback<BlurEvent> callback)
            => Register(callback);

        /// <summary>
        /// This event is sent when the left mouse button is clicked.
        /// </summary>
        public void RegisterClick(EventCallback<ClickEvent> callback)
            => Register(callback);
        /// <summary>
        /// This event is sent when the left mouse button is clicked.
        /// </summary>
        public void UnRegisterClick(EventCallback<ClickEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// The event sent when clicking the right mouse button.
        /// </summary>
        public void RegisterContextClick(EventCallback<ClickEvent> callback)
            => Register(callback);
        /// <summary>
        /// The event sent when clicking the right mouse button.
        /// </summary>
        public void UnRegisterContextClick(EventCallback<ClickEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// The event sent when a contextual menu reguires menu items.
        /// </summary>
        public void RegisterContextualMenuPopulate(EventCallback<ContextualMenuPopulateEvent> callback)
            => Register(callback);
        /// <summary>
        /// The event sent when a contextual menu reguires menu items.
        /// </summary>
        public void UnRegisterContextualMenuPopulate(EventCallback<ContextualMenuPopulateEvent> callback)
               => UnRegister(callback);

        /// <summary>
        /// Event sent after the custom style properties of a VisualElement have been resolved.
        /// </summary>
        public void RegisterCustomStyleResolved(EventCallback<CustomStyleResolvedEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent after the custom style properties of a VisualElement have been resolved.
        /// </summary>
        public void OnRegisterCustomStyleResolved(EventCallback<CustomStyleResolvedEvent> callback)
            => Register(callback);

        /// <summary>
        /// Event sent immediately after an element has gained focus.
        /// This event trickles down, if does not bubble up, and if cannot be cancelled.
        /// </summary>
        public void RegisterFocus(EventCallback<FocusEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent immediately after an element has gained focus.
        /// This event trickles down, if does not bubble up, and if cannot be cancelled.
        /// </summary>
        public void UnRegisterFocus(EventCallback<FocusEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Event sent immediately before an element gains focus.
        /// This event trickles down and bubbles up. This event cannot be cancelled
        /// </summary>
        public void RegisterFocusIn(EventCallback<FocusInEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent immediately before an element gains focus.
        /// This event trickles down and bubbles up. This event cannot be cancelled
        /// </summary>
        public void UnRegisterFouceIn(EventCallback<FocusInEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Event sent immediately before an element loses focus.
        /// This event trickles down and bubbles up. This event cannot be cancelled.
        /// </summary>
        public void RegisterFocusOut(EventCallback<FocusOutEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent immediately before an element loses focus.
        /// This event trickles down and bubbles up. This event cannot be cancelled.
        /// </summary>
        public void UnRegisterFocusOut(EventCallback<FocusOutEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// This event is sent after layout calculations,
        /// when the position or the dimension of an element changes.
        /// </summary>
        public void RegisterGeometryChanged(EventCallback<GeometryChangedEvent> callback)
            => Register(callback);
        /// <summary>
        /// This event is sent after layout calculations,
        /// when the position or the dimension of an element changes.
        /// </summary>
        public void UnRegisterGeometryChanged(EventCallback<GeometryChangedEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Class used to send a IMGUI event that has no equivalent UIElements event.
        /// </summary>
        public void RegisterIMGUI(EventCallback<IMGUIEvent> callback)
            => Register(callback);
        /// <summary>
        /// Class used to send a IMGUI event that has no equivalent UIElements event.
        /// </summary>
        public void UnRegisterIMGUI(EventCallback<IMGUIEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Sends an event when text from a TextField changes.
        /// </summary>
        public void RegisterInput(EventCallback<InputEvent> callback)
            => Register(callback);
        /// <summary>
        /// Sends an event when text from a TextField changes.
        /// </summary>
        public void UnRegisterInput(EventCallback<InputEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// This event is sent when a key is pressed.
        /// </summary>
        public void RegisterKeyDown(EventCallback<KeyDownEvent> callback)
            => Register(callback);
        /// <summary>
        /// This event is sent when a key is pressed.
        /// </summary>
        public void UnRegisterKeyDown(EventCallback<KeyDownEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// This event is sent when a pressed key is released.
        /// </summary>
        public void RegisterKeyUp(EventCallback<KeyUpEvent> callback)
            => Register(callback);
        /// <summary>
        /// This event is sent when a pressed key is released.
        /// </summary>
        public void UnRegisterKeyUp(EventCallback<KeyUpEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Event sent after a handler starts capturing the mouse.
        /// </summary>
        public void RegisterMouseCapture(EventCallback<MouseCaptureEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent after a handler starts capturing the mouse.
        /// </summary>
        public void UnRegisterMouseCapture(EventCallback<MouseCaptureEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Event sent before a handler stops capturing the mouse.
        /// </summary>
        public void RegisterMouseCaptureOut(EventCallback<MouseCaptureEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent before a handler stops capturing the mouse.
        /// </summary>
        public void UnRegisterMouseCaptureOut(EventCallback<MouseCaptureOutEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Mouse down event.
        /// </summary>
        public void RegisterMouseDown(EventCallback<MouseDownEvent> callback)
            => Register(callback);
        /// <summary>
        /// Mouse down event.
        /// </summary>
        public void UnRegisterMouseDown(EventCallback<MouseDownEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Event sent when the mouse pointer enters an element or one of its descendent elements.
        /// The event is cancellable, it does not trickle down, and it does not bubble up.
        /// </summary>
        public void RegisterMouseEnter(EventCallback<MouseEnterEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent when the mouse pointer enters an element or one of its descendent elements.
        /// The event is cancellable, it does not trickle down, and it does not bubble up.
        /// </summary>
        public void UnRegisterMouseEnter(EventCallback<MouseEnterEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Event sent when the mouse pointer enters a window.
        /// The event is cancellable, it does not trickle down, and it does not bubble up.
        /// </summary>
        public void RegisterMouseEnterWindow(EventCallback<MouseEnterWindowEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent when the mouse pointer enters a window.
        /// The event is cancellable, it does not trickle down, and it does not bubble up.
        /// </summary>
        public void UnRegisterMouseEnterWindow(EventCallback<MouseEnterWindowEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Event sent when the mouse pointer exits an element and all its descendent elements.
        /// The event is cancellable, it does not trickle down, and it does not bubble up.
        /// </summary>
        public void RegisterMouseLeave(EventCallback<MouseLeaveEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent when the mouse pointer exits an element and all its descendent elements.
        /// The event is cancellable, it does not trickle down, and it does not bubble up.
        /// </summary>
        public void UnRegisterMouseLeave(EventCallback<MouseLeaveEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Event sent when the mouse pointer exits a window.
        /// The event is cancellable, it does not trickle down, and it does not bubble up.
        /// </summary>
        public void RegisterMouseLeaveWindow(EventCallback<MouseLeaveWindowEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent when the mouse pointer exits a window.
        /// The event is cancellable, it does not trickle down, and it does not bubble up.
        /// </summary>
        public void UnRegisterMouseLeaveWindow(EventCallback<MouseLeaveWindowEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Mouse move event.
        /// </summary>
        public void RegisterMouseMove(EventCallback<MouseMoveEvent> callback)
            => Register(callback);
        /// <summary>
        /// Mouse move event.
        /// </summary>
        public void UnRegisterMouseMove(EventCallback<MouseMoveEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Event sent when the mouse pointer exits an element.
        /// The event trickles down, it bubbles up, and it is cancellable.
        /// </summary>
        public void RegisterMouseOut(EventCallback<MouseOutEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent when the mouse pointer exits an element.
        /// The event trickles down, it bubbles up, and it is cancellable.
        /// </summary>
        public void UnRegisterMouseOut(EventCallback<MouseOutEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Event sent when the mouse pointer enters an element.
        /// The event trickles down, it bubbles up, and it is cancellable.
        /// </summary>
        public void RegisterMouseOver(EventCallback<MouseOverEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent when the mouse pointer enters an element.
        /// The event trickles down, it bubbles up, and it is cancellable.
        /// </summary>
        public void UnRegisterMouseOver(EventCallback<MouseOverEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Mouse up event.
        /// </summary>
        public void RegisterMouseUp(EventCallback<MouseUpEvent> callback)
            => Register(callback);
        /// <summary>
        /// Mouse up event.
        /// </summary>
        public void UnRegisterMouseUp(EventCallback<MouseUpEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Event sent when the user presses the cancel button.
        /// </summary>
        public void RegisterNavigationCancel(EventCallback<NavigationCancelEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent when the user presses the cancel button.
        /// </summary>
        public void UnRegisterNavigationCancel(EventCallback<NavigationCancelEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Event typically sent when the user presses the D-pad,
        /// moves a joystick or presses the arrow keys.
        /// </summary>
        public void RegisterNavigationMove(EventCallback<NavigationMoveEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event typically sent when the user presses the D-pad,
        /// moves a joystick or presses the arrow keys.
        /// </summary>
        public void UnRegisterNavigationMove(EventCallback<NavigationMoveEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Event sent when the user presses the submit button.
        /// </summary>
        public void RegisterNavigationSubmit(EventCallback<NavigationSubmitEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent when the user presses the submit button.
        /// </summary>
        public void UnRegisterNavigationSubmit(EventCallback<NavigationSubmitEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// This event is sent when pointer interaction is cancelled.
        /// </summary>
        public void RegisterPointerCancel(EventCallback<PointerCaptureEvent> callback)
            => Register(callback);
        /// <summary>
        /// This event is sent when pointer interaction is cancelled.
        /// </summary>
        public void UnRegisterPointerCancel(EventCallback<PointerCaptureEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Event sent when a pointer is captured by a VisualElement.
        /// </summary>
        public void RegisterPointerCapture(EventCallback<PointerCaptureEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent when a pointer is captured by a VisualElement.
        /// </summary>
        public void UnRegisterPointerCapture(EventCallback<PointerCaptureEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// Event sent when a VisualElement releases a pointer.
        /// </summary>
        public void RegisterPointerCaptureOut(EventCallback<PointerCaptureEvent> callback)
            => Register(callback);
        /// <summary>
        /// Event sent when a VisualElement releases a pointer.
        /// </summary>
        public void UnRegisterPointerCaptureOut(EventCallback<PointerCaptureEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// This event is sent when a pointer is pressed.
        /// </summary>
        public void RegisterPointerDown(EventCallback<PointerDownEvent> callback)
            => Register(callback);
        /// <summary>
        /// This event is sent when a pointer is pressed.
        /// </summary>
        public void UnRegisterPointerDown(EventCallback<PointerDownEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// This event is sent when a pointer enters a VisualElement or one of its descendants.
        /// </summary>
        public void RegisterPointerEnter(EventCallback<PointerEnterEvent> callback)
            => Register(callback);
        /// <summary>
        /// This event is sent when a pointer enters a VisualElement or one of its descendants.
        /// </summary>
        public void UnRegisterPointerEnter(EventCallback<PointerEnterEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// This event is sent when a pointer exits an element and all of its descendants.
        /// </summary>
        public void RegisterPointerLeave(EventCallback<PointerLeaveEvent> callback)
            => Register(callback);
        /// <summary>
        /// This event is sent when a pointer exits an element and all of its descendants.
        /// </summary>
        public void UnRegisterPointerLeave(EventCallback<PointerLeaveEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// This event is sent when a pointer changes state.
        /// </summary>
        public void RegisterPointerMove(EventCallback<PointerMoveEvent> callback)
            => Register(callback);
        /// <summary>
        /// This event is sent when a pointer changes state.
        /// </summary>
        public void UnRegisterPointerMove(EventCallback<PointerMoveEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// This event is sent when a pointer exits an element.
        /// </summary>
        public void RegisterPointerOut(EventCallback<PointerOutEvent> callback)
            => Register(callback);
        /// <summary>
        /// This event is sent when a pointer exits an element.
        /// </summary>
        public void UnRegisterPointerOut(EventCallback<PointerOutEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// This event is sent when a pointer enters an element.
        /// </summary>
        public void RegisterPointerOver(EventCallback<PointerOverEvent> callback)
            => Register(callback);
        /// <summary>
        /// This event is sent when a pointer enters an element.
        /// </summary>
        public void UnRegisterPointerOver(EventCallback<PointerOverEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// This event is sent when a pointer does not change for a set amount of time,
        /// determined by the operating system.
        /// </summary>
        public void RegisterPointerStationary(EventCallback<PointerStationaryEvent> callback)
            => Register(callback);
        /// <summary>
        /// This event is sent when a pointer does not change for a set amount of time,
        /// determined by the operating system.
        /// </summary>
        public void UnRegisterPointerStationary(EventCallback<PointerStationaryEvent> callback)
            => UnRegister(callback);

        /// <summary>
        /// This event is sent when a pointer's last pressed button is released.
        /// </summary>
        public void OnRegisterPointerUp(EventCallback<PointerUpEvent> callback)
            => Register(callback);
        /// <summary>
        /// This event is sent when a pointer's last pressed button is released.
        /// </summary>
        public void OnUnRegisterPointerUp(EventCallback<PointerUpEvent> callback)
            => UnRegister(callback);

        

        

    }
}
