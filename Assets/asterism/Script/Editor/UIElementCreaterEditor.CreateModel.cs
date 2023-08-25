using System.IO;
using UnityEditor;

namespace Asterism.UI.UIElements
{
    public partial class UIElementCreaterEditor : EditorWindow
    {
        private readonly string _model_file_GUID = "7667376d8d222a94e8a8ce5c28d447c0";
        private readonly string _model_file_format = "{0}.model.cs";
        private void CreateModel()
        {
            var templateFile = File.ReadAllText(
                AssetDatabase.GUIDToAssetPath(_model_file_GUID)
            );
            CreateFile(
                _model_file_format,
                templateFile,
                ".model.ファイルの生成",
                "既に生成済みのため生成をスキップします",
                true
            );
        }
    }
}
