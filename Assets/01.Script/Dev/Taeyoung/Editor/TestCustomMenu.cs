using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
public class NameEditor : EditorWindow
{
    #region º¯¼öµé
    public GameObject[] targetObjects;
    string name, removeKey, replaceKey, tragetReplaceKey, linerKeyR, linerKeyL;
    Transform trans;
    NameSettingPreset preset;
    #endregion
    [MenuItem("Window/CustomWindow/NameManager")]
    static void open()
    {
        var window = GetWindow<NameEditor>();
    }
    private void OnEnable()
    {

    }
    private void OnGUI()
    {
        GUILayout.Space(10);
        GUILayout.Label("NameManager", EditorStyles.boldLabel);
        GUILayout.Space(5);
        GUILayout.Space(5);
        preset = (NameSettingPreset)EditorGUILayout.EnumPopup("Primitive to create:", preset);
        GUILayout.Space(5);
        switch (preset)
        {
            case NameSettingPreset.JustSetNameDirection:
            case NameSettingPreset.SetNameByIndex:
            case NameSettingPreset.SetNameByParentChild:
                name = EditorGUILayout.TextField("Name:", name);
                break;
            case NameSettingPreset.Replace:
                replaceKey = EditorGUILayout.TextField("ReplaceKey:", replaceKey);
                tragetReplaceKey = EditorGUILayout.TextField("To:", tragetReplaceKey);
                break;
            case NameSettingPreset.Remove:
                removeKey = EditorGUILayout.TextField("RemoveKey:", removeKey);
                break;
            case NameSettingPreset.AddLiner:
                linerKeyL = EditorGUILayout.TextField("LinerKeyLeft :", linerKeyL);
                linerKeyR = EditorGUILayout.TextField("LinerKeyRight:", linerKeyR);
                break;
            default:
                break;
        }
        if (GUILayout.Button("Generate"))
        {
            int index = 1;
            foreach (GameObject obj in targetObjects)
            {
                switch (preset)
                {
                    case NameSettingPreset.JustSetNameDirection:
                        obj.name = name;
                        break;
                    case NameSettingPreset.SetNameByIndex:
                        obj.name = $"{name} {index}";
                        break;
                    case NameSettingPreset.SetNameByParentChild:
                        if (obj.transform.parent != null)
                        {
                            if (obj.transform.parent != trans)
                            {
                                index = 1;
                            }
                            trans = obj.transform.parent;
                            obj.name = $"Child : {obj.transform.parent.name} {index}";
                        }
                        else
                        {
                            obj.name = name;
                        }
                        break;
                    case NameSettingPreset.Replace:
                        obj.name = obj.name.Replace(replaceKey, tragetReplaceKey);
                        break;
                    case NameSettingPreset.Remove:
                        obj.name = obj.name.Replace(removeKey, "");
                        break;
                    case NameSettingPreset.Up:
                        obj.name = obj.name.ToUpper();
                        break;
                    case NameSettingPreset.AddLiner:
                        if (linerKeyR == "")
                        {
                            linerKeyR = linerKeyL;
                        }
                        obj.name = $"{linerKeyL}{obj.name}{linerKeyR}";
                        break;
                    default:
                        break;
                }
                index++;
            }
        }
        if (GUILayout.Button("Reset"))
        {
            targetObjects = new GameObject[0];
        }
        #region List
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("targetObjects");

        EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
        so.ApplyModifiedProperties(); // Remember to apply modified properties
        #endregion
    }
    enum NameSettingPreset
    {
        JustSetNameDirection,
        SetNameByIndex,
        SetNameByParentChild,
        Replace,
        Remove,
        Up,
        AddLiner,
    }
}