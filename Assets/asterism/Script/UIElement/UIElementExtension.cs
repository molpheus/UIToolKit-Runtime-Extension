using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    public static class UIElementExtension
    {
        public static VisualElement SearchElement(this VisualElement visualElement, string[] elementNamePathList)
        {
            VisualElement element = null;
            foreach (var tagName in elementNamePathList)
            {
                element = element == null
                        ? visualElement.Q<VisualElement>(tagName)
                        : element.Q<VisualElement>(tagName);
            }
            return element;
        }

        public static T SearchElement<T>(this VisualElement visualElement, string[] tagNameList, out VisualElement selectElement)
            where T : VisualElement
        {
            selectElement = visualElement.SearchElement(tagNameList);
            return selectElement.Q<T>();
        }
    }
}
