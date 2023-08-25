using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    public partial class UIElementCreaterEditor : EditorWindow
    {
        private readonly string _tabSpace = "    ";
        private readonly string _templateFileGUID = "a8eaad29498f4a341a5edee3d084e19d";
        private readonly string _export_binding_file_format = "{0}.binding.cs";

        /// <summary>
        /// Binding�t�@�C���𐶐�����
        /// </summary>
        private void CreateBindingFile()
        {
            var templateFile = File.ReadAllText(
                AssetDatabase.GUIDToAssetPath(_templateFileGUID)
            );

            // �ݒ�ʂɃf�[�^�𕪂���
            Dictionary<string, List<CheckItemListContent>> contentList = new();
            foreach( var item in _saveData.checkList)
            {
                if (contentList.ContainsKey(item.structName))
                {
                    contentList[item.structName].Add(item);
                }
                else
                {
                    var list = new List<CheckItemListContent>() {
                        item
                    };
                    contentList.Add(item.structName, list);
                }
            }

            StringBuilder builder = new();
            StringBuilder initializeBuilder = new();
            foreach (var key in contentList.Keys.OrderBy(x => x))
            {
                if (string.IsNullOrEmpty(key))
                {
                    foreach(var item in contentList[key])
                    {
                        var variableName = CreateVariable(1, item, ref builder);
                        
                        initializeBuilder.AppendLine(
                            CreateTagString(
                                2,
                                $"{variableName}.Initialize(visualElement);"
                            )
                        );
                    }
                }
                else
                {
                    var pascalStr = ToPascal(key);
                    var camelStr = ToCamel(key);
                    builder.AppendLine(
                        CreateTagString(1, $"public class {pascalStr} {{")
                    );
                    StringBuilder classInitializeBuilder = new();
                    classInitializeBuilder
                        .AppendLine(
                            CreateTagString(2, $"public void Initialize(VisualElement visualElement) {{")
                        );
                    foreach (var item in contentList[key])
                    {
                        var variableName = CreateVariable(2, item, ref builder);

                        classInitializeBuilder
                            .AppendLine(
                                CreateTagString(
                                    3,
                                    $"{variableName}.Initialize(visualElement);"
                                )
                            );
                    }
                    classInitializeBuilder
                        .AppendLine(
                            CreateTagString(2, $"}}")
                        );
                    builder.AppendLine(classInitializeBuilder.ToString());

                    builder.AppendLine(
                        CreateTagString(1, $"}}")
                    ).AppendLine(
                        CreateTagString(1, $"protected {pascalStr} {camelStr};")
                    );

                    initializeBuilder.AppendLine(
                        CreateTagString(
                            2,
                            $"{camelStr}.Initialize(visualElement);"
                        )
                    );
                }
                builder.AppendLine("");
            }

            builder.AppendLine(@"    private void Awake()
    {
        var uiDocument = GetComponent<UIDocument>();
        var visualElement = uiDocument.rootVisualElement;

        initialize(visualElement);
    }");

            builder.AppendLine("");

            builder
                .AppendLine(
                    CreateTagString(1, $"public void initialize(VisualElement visualElement) {{")
                ).AppendLine(
                    initializeBuilder.ToString()
                ).AppendLine(
                    CreateTagString(1, $"}}")
                );

            var contentStr = builder.ToString();
            templateFile = templateFile.Replace("#CONTENT#", contentStr);

            // �t�@�C���ɏ������݂��s��
            CreateFile(_export_binding_file_format, templateFile);
            CreateView();
            CreateModel();
            CreateBase();

            // �v���n�u���쐬����
            var assetPath = _outputItemField.value.Replace(Application.dataPath, "Assets");
            var prefabPath = Path.Combine( assetPath, string.Format("{0}.prefab", _outputFileNameField.value) );
            try
            {
                var prefab = PrefabUtility.LoadPrefabContents(prefabPath);
                PrefabUtility.UnloadPrefabContents(prefab);
                AttachScript();
            }
            catch
            {
                var generateAssetPath = AssetDatabase.GenerateUniqueAssetPath(prefabPath);
                var prefab = new GameObject(_outputFileNameField.value);

                var visualTree = AssetDatabase.LoadAssetAtPath(_selectItemField.value, typeof(VisualTreeAsset)) as VisualTreeAsset;

                var uiDocument = prefab.AddComponent<UIDocument>();
                uiDocument.visualTreeAsset = visualTree;

                PrefabUtility.SaveAsPrefabAssetAndConnect(prefab, generateAssetPath, InteractionMode.AutomatedAction);
            }
            ExportSave();

            AssetDatabase.Refresh();

            var detail = @"
�P�D����ȍ~�͈ꏏ�ɓf���o����Ă���.save.�𗘗p���ăE�B���h�E���J�����ƂŁA�ݒ�𕜌����邱�Ƃ��\�ł��B
�Q�D�������ꂽPrefab��UIDocument��Panel Settings�ɃQ�[���Ŏg�p����Panel Settings��ݒ肵�Ă��������B
�R�D���������t�@�C���́A�ʂŃt�@�C�����A�^�b�`���邩�E�ēxSave����J�����ƂŃt�@�C�����v���n�u�ɃA�^�b�`���邱�Ƃ��\�ł��B
";
            EditorUtility.DisplayDialog("", detail, "OK");

            Close();
        }

        private string CreateVariable(int baseSpace, CheckItemListContent content, ref StringBuilder builder)
        {
            var camelStr = ToCamel(content.variable);
            var variable = GetVariableType(content.variableType);
            var variableName = CreateUIElement(content.element)?.GetType().Name;

            builder
                .AppendLine(
                    CreateTagString(baseSpace, $"{variable} {variableName} {camelStr} = new() {{")
                ).AppendLine(
                    CreateTagString(baseSpace + 1, $"TagNameList = new[] {{ {StringArrWithOpen(content.pathList)} }}")
                ).AppendLine(
                    CreateTagString(baseSpace, $"}};")
                );

            return camelStr;
        }

        private string CreateTagString(int tab, string value)
        {
            var content = new StringBuilder();

            for (int i = 0; i < tab; i++)
                content.Append(_tabSpace);

            content.Append(value);

            return content.ToString();
        }







    }
}