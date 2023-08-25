using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.IO;
using System;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Asterism.UI.UIElements
{
    public partial class UIElementCreaterEditor : EditorWindow
    {
        private readonly string _editor_ui_guid = "257e15a813d4cb845aeb7ede61febf93";
        
        private const string _export_save_file = "{0}.save.txt";


        /// <summary>
        /// ウィンドウを開く
        /// </summary>
        [MenuItem("Assets/Asterism/CreateUIElementScript")]
        private static void OpenWindow()
        {
            var window = GetWindow<UIElementCreaterEditor>();
            window.Show();
        }

        /// <summary>
        /// 変数のアクセス設定
        /// </summary>
        public enum VariableType
        {
            PUBLIC,
            PRIVATE,
            PROTECTED,
            PRIVATE_SerializeField,
            PROTECTED_SerializeField,
            PUBLIC_READONLY,
            PRIVATE_READONLY,
            PROTECTED_READONLY,
        }

        /// <summary>
        /// 保存するデータのチェック用
        /// </summary>
        [Serializable]
        public class CheckItemListContent
        {
            public bool check;
            public string path;
            public string type;
            public string structName;
            public string variable;
            public bool add;

            public bool isDisable;

            public VariableType variableType;

            public string[] pathList;
            public VisualElement element { get; set; }


            public CheckItemListContent(string path, VisualElement element, string[] pathList)
            {
                this.check = true;
                this.path = path;
                this.variable = "";
                this.pathList = pathList;
                this.element = element;
                this.type = element.GetType().Name;

                this.add = true;
                this.isDisable = false;

                this.structName = "";
            }

            public void UpdateText(ChangeEvent<string> value)
            {
                variable = value.newValue;
            }

            public void UpdateVariavleType(ChangeEvent<Enum> changeType)
            {
                if (changeType.newValue is VariableType value)
                {
                    variableType = value;
                }
            }

            public void UpdateAddCheck(ChangeEvent<bool> value)
            {
                add = value.newValue;
            }

            public void UpdateStructName(ChangeEvent<string> value)
            {
                structName = value.newValue;
            }
        }

        /// <summary>
        /// 保存機能
        /// </summary>
        [Serializable]
        public class Save
        {
            public string SelectItemPath;
            public string ExportDirectory;
            public string ExportFileName;

            public string NameSpaceName;

            public List<CheckItemListContent> checkList;

            public void UpdateSelectItemPath(string value)
                => SelectItemPath = value;

            public void UpdateExportDIrectoryPath(string value)
                => ExportDirectory = value;

            public void UpdateExportFileName(string value)
                => ExportFileName = value;

            public void UpdateNameSpaceName(string value)
                => NameSpaceName = value;
        }

        /// <summary> セーブ用データ </summary>
        private Save _saveData;
        /// <summary> 表示用UI </summary>
        VisualTreeAsset _visualTree;

        VisualElement _selectWindow;

        TextField _selectItemField;
        TextField _outputItemField;
        TextField _outputFileNameField;
        Label _outputExportFileName;

        TextField _nameSpaceFIeld;

        Dictionary<string[], VisualElement> _elementList;

        private void OnEnable()
        {
            var filePath = AssetDatabase.GUIDToAssetPath(_editor_ui_guid);
            _visualTree = AssetDatabase.LoadAssetAtPath(filePath, typeof(VisualTreeAsset)) as VisualTreeAsset;
            _visualTree.CloneTree(rootVisualElement);

            _selectItemField = rootVisualElement.Q<TextField>("SelectItemField");
            _outputItemField = rootVisualElement.Q<TextField>("OutputFilePathField");
            _outputFileNameField = rootVisualElement.Q<TextField>("OutputFileNameField");
            _selectWindow = rootVisualElement.Q("SelectWindow");
            _nameSpaceFIeld = rootVisualElement.Q<TextField>("NameSpace");
            
            // 確認ボタンが押されたときの処理
            rootVisualElement.Q<Button>("CheckButton").clicked += CreateElement;
            // フォルダ選択ボタンが押されたときの処理
            rootVisualElement.Q<Button>("OutputFilePathButton").clicked += () => {
                var path = EditorUtility.OpenFolderPanel("Select Folder", Application.dataPath, string.Empty);
                _outputItemField.value = path;
                _saveData.ExportDirectory = path;
            };
            // 吐き出しボタンが押されたときの処理
            rootVisualElement.Q<Button>("ExportButton").clicked += CreateBindingFile;
            // スクリプトのアタッチボタンが押されたときの処理
            rootVisualElement.Q<Button>("AttachButton").clicked += AttachScript;

            try
            {
                var selectObject = Selection.objects[0];
                if (selectObject is TextAsset textFile)
                {
                    LoadSave(textFile.text);
                }
                else if (selectObject is VisualTreeAsset)
                {
                    var objectPath = AssetDatabase.GetAssetPath(selectObject);
                    _saveData = new();
                    _saveData.checkList = new();
                    _selectItemField.value = AssetDatabase.GetAssetPath(selectObject);
                    _outputItemField.value = Application.dataPath;
                    _outputFileNameField.value = Path.GetFileNameWithoutExtension(objectPath);
                    _saveData.SelectItemPath = _selectItemField.value;
                    _saveData.ExportDirectory = _outputItemField.value;
                    _saveData.ExportFileName = _outputFileNameField.value;
                }
                else
                {
                    throw new Exception();
                }

                _selectItemField.RegisterValueChangedCallback(_ => { _saveData.SelectItemPath = _.newValue; });
                _outputItemField.RegisterValueChangedCallback(_ => { _saveData.ExportDirectory = _.newValue; });
                _outputFileNameField.RegisterValueChangedCallback(_ => {
                    _saveData.ExportFileName = _.newValue;
                    _outputExportFileName.text = string.Format(_export_binding_file_format, _.newValue);
                });
                _nameSpaceFIeld.RegisterValueChangedCallback(value => { _saveData.NameSpaceName = value.newValue; });

                // 出力する予定のファイル名を表示するラベル
                _outputExportFileName = rootVisualElement.Q<Label>("CheckOutputFileNameLabel");
                _outputExportFileName.text = string.Format(_export_binding_file_format, _outputFileNameField.value);
            }
            catch(Exception e)
            {

                EditorUtility.DisplayDialog(e.Message, "オブジェクト(uxml, saveファイル)が選択されていないため終了します", "OK");
            }
        }

        /// <summary>
        /// MultiListに表示するアイテムを生成する
        /// </summary>
        private void CreateElement()
        {
            VisualElement baseElement = new VisualElement();
            var visualTree = AssetDatabase.LoadAssetAtPath(_selectItemField.value, typeof(VisualTreeAsset)) as VisualTreeAsset;
            visualTree.CloneTree(baseElement);

            Dictionary<string[], VisualElement> elementList = new Dictionary<string[], VisualElement>();
            Search(baseElement, ref elementList);
            _elementList = elementList;

            ViewCheckItems();
        }

        /// <summary>
        /// UXMLの内容を検索する再帰処理
        /// </summary>
        /// <param name="baseElement"> Parentとなるエレメント </param>
        /// <param name="elementList"> エレメントのリスト </param>
        private void Search(VisualElement baseElement,　ref Dictionary<string[], VisualElement> elementList)
        {
            foreach (var element in baseElement.Children())
            {
                if (GetElement(element, ref elementList))
                {
                    Search(element, ref elementList);
                }
            }
        }

        /// <summary>
        /// 現在選択されたエレメントが上げているコンテンツに引っかかるかのチェック
        /// </summary>
        /// <param name="element"> 確認対象のエレメント </param>
        /// <param name="elementList"> エレメントのリスト </param>
        /// <returns></returns>
        private bool GetElement(VisualElement element, ref Dictionary<string[], VisualElement> elementList)
        {
            var parent = element.parent;
            var pathList = new List<string>();
            if (!string.IsNullOrEmpty(element.name))
            {
                bool isEnd = false;
                pathList.Add(element.name);
                while (!isEnd)
                {
                    if (parent is not null)
                    {
                        pathList.Add(parent.name);
                        parent = parent.parent;
                    }

                    isEnd = parent == null;
                }
                pathList.Reverse();

                elementList.Add(pathList.ToArray(), element);
            }

            return element switch {
                Label => false,
                Button => false,
                Toggle => false,
                Scroller => false,
                TextField => false,
                Slider => false,
                SliderInt => false,
                MinMaxSlider => false,
                ProgressBar => false,
                DropdownField => false,
                EnumField => false,
                RadioButton => false,
                Foldout => true,
                RadioButtonGroup => true,
                _ => true
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private UIElement CreateUIElement(VisualElement element)
        {
            return element switch
            {
                Label => new UIElementLabel(),
                Button => new UIElementButton(),
                Toggle => new UIElementToggle(),
                Scroller => new UIElementScroller(),
                TextField => new UIElementTextField(),
                //Foldout => ,
                Slider => new UIElementSlider(),
                SliderInt => new UIElementSliderInt(),
                MinMaxSlider => new UIElementMinMaxSlider(),
                ProgressBar => new UIElementProgressBar(),
                DropdownField => new UIElementDropdown(),
                EnumField => new UIElementEnum(),
                RadioButton => new UIElementRadioButton(),
                RadioButtonGroup => new UIElementRadioButtonGroup(),
                _ => new UIElement(),
            };
        }

        private string GetVariableType(VariableType type)
        {
            return type switch {
                VariableType.PUBLIC => "public",
                VariableType.PRIVATE => "private",
                VariableType.PROTECTED => "protected",
                VariableType.PRIVATE_SerializeField => "[UnityEngine.SerializeField] private",
                VariableType.PROTECTED_SerializeField => "[UnityEngine.SerializeField] protected",
                VariableType.PUBLIC_READONLY => "public readonly",
                VariableType.PRIVATE_READONLY => "private readonly",
                VariableType.PROTECTED_READONLY => "protected readonly",
                _ => ""
            };
        }

        

        static public string ToPascal(string text)
        {
            return Regex.Replace(
                text.Replace("_", " "),
                @"\b[a-z]",
                match => match.Value.ToUpper()).Replace(" ", "");
        }

        static public string ToCamel(string text)
        {
            return Regex.Replace(
                text.Replace("_", " "),
                @"\b[A-Z]",
                match => match.Value.ToLower()).Replace(" ", "");
        }

        private string StringArrWithOpen(string[] list)
        {
            var str = "";
            foreach(var item in list)
            {
                if (string.IsNullOrEmpty(item)) continue;

                if (!string.IsNullOrEmpty(str))
                    str += ",";

                str += $"\"{item}\"";
            }
            return str;
        }


        private void AttachScript()
        {
            try
            {
                var assetPath = _outputItemField.value.Replace(Application.dataPath, "Assets");
                var prefabPath = Path.Combine( assetPath, string.Format("{0}.prefab", _outputFileNameField.value) );
                if (!System.IO.File.Exists(prefabPath))
                {
                    
                    throw new Exception("対象になるプレハブが生成されていません");
                }

                var scriptPath = Path.Combine( assetPath, string.Format(_export_binding_file_format, _outputFileNameField.value) );
                if (!System.IO.File.Exists(scriptPath))
                {
                    throw new Exception("対象になるスクリプトが生成されていません");
                }

                var getType = Type.GetType($"{_outputFileNameField.value}, Assembly-CSharp");
                if (getType is null)
                {
                    throw new Exception("Domainのリロードをお待ち下さい");
                }

                var prefab = PrefabUtility.LoadPrefabContents(prefabPath);

                if (!prefab.TryGetComponent(getType, out var component))
                {
                    prefab.AddComponent(getType);
                }

                PrefabUtility.SaveAsPrefabAsset(prefab, prefabPath);
            }
            catch(Exception ex)
            {
                EditorUtility.DisplayDialog("", ex.Message, "OK");
                Close();
            }
        }

        private void ExportSave()
        {
            CreateFile(_export_save_file, JsonUtility.ToJson(_saveData));
        }
        
        private void LoadSave(string text)
        {
            _saveData = JsonUtility.FromJson<Save>(text);
            _selectItemField.value = _saveData.SelectItemPath;
            _outputItemField.value = _saveData.ExportDirectory;
            _outputFileNameField.value = _saveData.ExportFileName;
            _nameSpaceFIeld.value = _saveData.NameSpaceName;

            CreateElement();
        }


        public void CreateFile(string format, string fileText, string title = "", string detail = "", bool isNewCreateOnly = false)
        {
            var filePath = Path.Combine( _outputItemField.value, string.Format(format, _outputFileNameField.value) );

            // ファイルクラス名を指定する
            fileText = fileText.Replace("#CLASSNAME#", _outputFileNameField.value);

            // namespaceの設定確認
            fileText = fileText.Replace("#ROOTNAMESPACEBEGIN#", string.IsNullOrEmpty(_nameSpaceFIeld.value) ? "" : $"namespace {_nameSpaceFIeld.value} \n{{");
            fileText = fileText.Replace("#ROOTNAMESPACEEND#", string.IsNullOrEmpty(_nameSpaceFIeld.value) ? "" : "}");

            if (isNewCreateOnly && File.Exists(filePath)) {
                EditorUtility.DisplayDialog(title, detail, "OK");
                return;
            }

            File.WriteAllText(filePath, fileText);
        }
    }
}
