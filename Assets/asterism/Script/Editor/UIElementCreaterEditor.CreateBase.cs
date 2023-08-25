using System.IO;
using UnityEditor;

namespace Asterism.UI.UIElements
{
    public partial class UIElementCreaterEditor : EditorWindow
    {
        private readonly string _base_file_GUID = "871ae93b29f3baa46922bd1b23884bc1";
        private readonly string _base_file_format = "{0}.cs";
        private void CreateBase()
        {
            var templateFile = File.ReadAllText(
                AssetDatabase.GUIDToAssetPath(_base_file_GUID)
            );
            CreateFile(
                _base_file_format,
                templateFile,
                "メインファイルの生成",
                "既に生成済みのため生成をスキップします",
                true
            );
        }
    }
}
