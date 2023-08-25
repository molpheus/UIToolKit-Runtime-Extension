// Auto-generated file
using Asterism.UI;
using Asterism.UI.UIElements;
using UnityEngine.UIElements;

namespace Test.UI 
{
public partial class TestTemplate : UIBehaviour
{
    private UIElement testButton1 = new() {
        TagNameList = new[] { "TestButton1" }
    };
    private UIElement testButton2 = new() {
        TagNameList = new[] { "TextButton2" }
    };

    private void Awake()
    {
        var uiDocument = GetComponent<UIDocument>();
        var visualElement = uiDocument.rootVisualElement;

        initialize(visualElement);
    }

    public void initialize(VisualElement visualElement) {
        testButton1.Initialize(visualElement);
        testButton2.Initialize(visualElement);

    }

}
}
