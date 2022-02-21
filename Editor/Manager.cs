using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Manager : EditorWindow
{
    public static GameObject currentSelection;
    public static GameObject prevSelection;
    static List<string> stack = new List<string>();
    Vector2 view;
    static private string SettingsFilePathStack
    {
        get
        {
            string directory = ("Assets/Editorials/SelectData");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return directory + "/" + "StackSelect" + ".sav";
        }
    }

    [MenuItem("Tools/Editorials/Manager")]
    public static void ShowWindow()
    {
        EditorWindow win = EditorWindow.GetWindow<Manager>("Editorials");
        win.minSize = new Vector2(200, 200);
    }

    [MenuItem("Tools/Editorials/Select Last &`")]
    static void SelectLast()
    {
        //string loadedLevel = File.ReadAllText("Assets/Editorials/SelectData/ObjLastSelect.select");
        //LastSelectionData data = JsonUtility.FromJson<LastSelectionData>(loadedLevel);
        Selection.activeGameObject = prevSelection;
        
    }
    void OnSelectionChange()
    {
        prevSelection = currentSelection;
        currentSelection = Selection.activeGameObject;
        //Save();
        Repaint();
    }

    void OnInspectorUpdate()
    {
        string sel = null;
        if (Selection.activeGameObject != null)
        {
            sel = Selection.activeGameObject.name;
        }

        if (!stack.Contains(sel))
        {
            stack.Insert(0, sel);
        }
        else
        {
            if (stack[0] != sel)
            {
                int index = stack.FindIndex(0, (v) => v == sel);
                stack.RemoveAt(index);
                stack.Insert(0, sel);
                
            }
        }
        if (stack.Count > 0)
        {
            SaveStack();
        }
    }

    public void SaveStack()
    {
        StackSelectionData dat = new StackSelectionData();

        if (stack.Count > 1)
        {

            foreach (string s in stack)
            {
                if(s != null)
                    dat.objs.Add(s);
            }

            string dataAsJson = JsonUtility.ToJson(dat, true);
            DirectoryInfo dir = new DirectoryInfo(SettingsFilePathStack);
            if (File.Exists(SettingsFilePathStack))
            {
                File.Delete(SettingsFilePathStack);
            }

            File.WriteAllText(SettingsFilePathStack, dataAsJson);

        }


    }

    public void OnGUI()
    {
        view = EditorGUILayout.BeginScrollView(view);
        var styleLabelBold = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 20 };
        var styleLabel = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
        EditorGUILayout.LabelField("Last Selector", styleLabelBold, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Select Last Selected Game Object - Alt+`(Reverse Tick)", EditorStyles.boldLabel);
        if (GUILayout.Button("Clear"))
        {
            prevSelection = null;
            currentSelection = null;
        }
        if (currentSelection != null)
            EditorGUILayout.LabelField("Current: " + currentSelection.name, EditorStyles.boldLabel);

        if (prevSelection != null)
            EditorGUILayout.LabelField("Previous: " + prevSelection.name, EditorStyles.boldLabel);

        
        EditorGUILayout.LabelField("              ", styleLabelBold, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Stack Selector", styleLabelBold, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("______________", styleLabelBold, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("              ", styleLabelBold, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Alt+Shift+`(reverse tick) - Cycle Up", styleLabel, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Shift+`(reverse tick) - Cycle Down", styleLabel, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("`(reverse tick) - Select", styleLabel, GUILayout.ExpandWidth(true));
        if (GUILayout.Button("Open Stack Selector"))
        {
            StackSelector.ShowWindow();
        }
        EditorGUILayout.EndScrollView();
    }

    

}
