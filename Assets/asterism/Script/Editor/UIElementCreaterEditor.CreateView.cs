using System.IO;
using UnityEditor;

namespace Asterism.UI.UIElements
{
    public partial class UIElementCreaterEditor : EditorWindow
    {
        private readonly string _view_file_GUID = "e2c64a67a752695418bc66319f2238e0";
        private readonly string _view_file_format = "{0}.view.cs";
        private void CreateView()
        {
            var templateFile = File.ReadAllText(
                AssetDatabase.GUIDToAssetPath(_view_file_GUID)
            );
            CreateFile(
                _view_file_format,
                templateFile,
                ".view.ファイルの生成",
                "既に生成済みのため生成をスキップします",
                true
            );
        }
    }
}