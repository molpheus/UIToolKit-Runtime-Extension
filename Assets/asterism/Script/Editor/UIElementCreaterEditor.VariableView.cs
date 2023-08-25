using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;

namespace Asterism.UI.UIElements
{
    public partial class UIElementCreaterEditor : EditorWindow
    {
        private void ViewCheckItems()
        {
            _selectWindow.visible = _elementList.Count > 0;

            if (_selectWindow.visible)
            {
                var selectContent = rootVisualElement.Q<MultiColumnListView>("SelectContent");

                selectContent.Clear();

                UpdateSaveDataCheck();

                selectContent.itemsSource = _saveData.checkList;
                var checkboxColumn = selectContent.columns["check"];
                var pathColumn = selectContent.columns["path"];
                var typeColumn = selectContent.columns["type"];
                var structColumn = selectContent.columns["struct"];
                var variableTypeColumn = selectContent.columns["variable_type"];
                var variableColumn = selectContent.columns["variable"];
                var addColumn = selectContent.columns["add"];

                // レイアウトを作成する処理
                checkboxColumn.makeCell = () => new Toggle();
                pathColumn.makeCell = () => new Label();
                typeColumn.makeCell = () => new Label();
                structColumn.makeCell = () => new TextField();
                variableTypeColumn.makeCell = () => new EnumField(VariableType.PUBLIC);
                variableColumn.makeCell = () => new TextField();
                addColumn.makeCell = () => new Toggle();

                // 内容を設定する処理
                checkboxColumn.bindCell = (e, i) => (e as Toggle).value = _saveData.checkList[i].check;
                pathColumn.bindCell = (e, i) => (e as Label).text = _saveData.checkList[i].path;
                typeColumn.bindCell = (e, i) => (e as Label).text = _saveData.checkList[i].type;

                structColumn.bindCell = (e, i) => {
                    if (e is TextField field)
                    {
                        field.value = _saveData.checkList[i].structName;
                        field.RegisterValueChangedCallback(_saveData.checkList[i].UpdateStructName);
                    }
                };
                structColumn.unbindCell = (e, i) => {
                    if (e is TextField field)
                    {
                        field.UnregisterValueChangedCallback(_saveData.checkList[i].UpdateStructName);
                    }
                };

                variableTypeColumn.bindCell = (e, i) => {
                    (e as EnumField).value = _saveData.checkList[i].variableType;
                    (e as EnumField).RegisterValueChangedCallback(_saveData.checkList[i].UpdateVariavleType);
                };
                variableTypeColumn.unbindCell = (e, i) => {
                    (e as EnumField).UnregisterValueChangedCallback(_saveData.checkList[i].UpdateVariavleType);
                };

                variableColumn.bindCell = (e, i) => {
                    (e as TextField).value = _saveData.checkList[i].variable;
                    (e as TextField).RegisterValueChangedCallback(_saveData.checkList[i].UpdateText);
                };
                variableColumn.unbindCell = (e, i) => {
                    (e as TextField).UnregisterValueChangedCallback(_saveData.checkList[i].UpdateText);
                };

                addColumn.bindCell = (e, i) => {
                    if (e is Toggle toggle)
                    {
                        toggle.value = _saveData.checkList[i].add;
                        toggle.RegisterValueChangedCallback(_saveData.checkList[i].UpdateAddCheck);
                    }
                };
                addColumn.unbindCell = (e, i) => {
                    if (e is Toggle toggle)
                    {
                        toggle.UnregisterValueChangedCallback(_saveData.checkList[i].UpdateAddCheck);
                    }
                };

                checkboxColumn.visible = false;
            }
        }

        /// <summary>
        /// セーブデータの状態を更新する
        /// </summary>
        private void UpdateSaveDataCheck()
        {
            var saveList = new List<CheckItemListContent>(_saveData.checkList);

            _saveData.checkList.Clear();
            foreach (var e in _elementList)
            {
                var label = "";
                foreach (var e2 in e.Key)
                {
                    if (string.IsNullOrEmpty(e2)) continue;
                    if (!string.IsNullOrEmpty(label))
                        label += " < ";
                    label += e2;
                }

                CheckItemListContent content = null;

                var obj = saveList.FirstOrDefault(e => label == e.path);

                if (obj is CheckItemListContent c)
                {
                    content = c;
                    content.element = e.Value;
                    saveList.Remove(c);
                }
                else
                {
                    content = new CheckItemListContent(label, e.Value, e.Key);
                    content.variable = e.Value.name;
                }

                _saveData.checkList.Add(content);
            }

            // 同じパスのデータが無いものは、非追加として後ろに追加しておく
            foreach (var e in saveList)
            {
                e.add = false;
                e.isDisable = true;
                _saveData.checkList.Add(e);
            }
        }







    }
}
