using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class StackSelector : EditorWindow
{
    Vector2 view;
    static List<string> stack = new List<string>();
    static int selectionIndex = 1;
   
    static private string SettingsFilePath
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

    [MenuItem("Tools/Editorials/Stack Selector %&`")]
    public static void ShowWindow()
    {
        EditorWindow win = EditorWindow.GetWindow<StackSelector>("Stack Selector");
        EditorWindow winman = EditorWindow.GetWindow<Manager>("Editorials");
        win.minSize = new Vector2(200, 200);

        if (File.Exists(SettingsFilePath))
            stack = Load().objs;
    }
    public static void HideWindow()
    {
        EditorWindow win = EditorWindow.GetWindow<StackSelector>("Method Invoker");
        win.Close();

    }

    void OnGUI()
    {
        if (File.Exists(SettingsFilePath))
            stack = Load().objs;
        else
        {
            EditorGUILayout.LabelField("No selection data saved, select objects in the hierarchy to have them appear here.");
            return;
        }
            
        //GUILayout.FlexibleSpace();

        var style = new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleCenter };
        var styleLabel = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
        var styleLabelBold = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontSize = 20};
        var styleBold = new GUIStyle(GUI.skin.button) { alignment = TextAnchor.MiddleCenter, fontSize = 20};
        
        EditorGUILayout.LabelField("Stack Selector", styleLabelBold, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("______________", styleLabelBold, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("              ", styleLabelBold, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Alt+Shift+`(reverse tick) - Cycle Up", styleLabel, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Shift+`(reverse tick) - Cycle Down", styleLabel, GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("`(reverse tick) - Select", styleLabel, GUILayout.ExpandWidth(true));
        view = EditorGUILayout.BeginScrollView(view);
        for (int i = 0; i < stack.Count; i++)
        {
            if(i == selectionIndex - 1)
            {

                //EditorGUILayout.LabelField(stack[i], styleBold, GUILayout.ExpandWidth(true));
                if (GUILayout.Button(stack[i], styleBold, GUILayout.ExpandWidth(true)))
                {
                    Selection.activeGameObject = GameObject.Find(stack[i]);
                    HideWindow();
                }
            }
            else
            {
                 //EditorGUILayout.LabelField(stack[i],style, GUILayout.ExpandWidth(true));
                if (GUILayout.Button(stack[i], style, GUILayout.ExpandWidth(true)))
                {
                    Selection.activeGameObject = GameObject.Find(stack[i]);
                    HideWindow();
                }
            }
            Repaint();

        }
        EditorGUILayout.EndScrollView();
        if (GUILayout.Button("Clear"))
        {
            StackSelectionData dat = new StackSelectionData();
            dat.objs = new List<string>();
            string dataAsJson = JsonUtility.ToJson(dat, true);
            DirectoryInfo dir = new DirectoryInfo(SettingsFilePath);
            if (File.Exists(SettingsFilePath))
            {
                File.Delete(SettingsFilePath);
            }

            File.WriteAllText(SettingsFilePath, dataAsJson);
        }
    }


    
    public static StackSelectionData Load()
    {

        string loadedLevel = File.ReadAllText(SettingsFilePath);
        StackSelectionData data = JsonUtility.FromJson<StackSelectionData>(loadedLevel);

        return data;

    }

    [MenuItem("Tools/Editorials/Stack/Cycle Up &#`")]
    public static void CycleUp()
    {

        if (selectionIndex > 1)
            selectionIndex--;
        else
            selectionIndex = stack.Count;


    }

    [MenuItem("Tools/Editorials/Stack/Cycle Down #`")]
    public static void CycleDown()
    {
        
        if (selectionIndex < stack.Count)
            selectionIndex++;
        else
            selectionIndex = 1;
        
    }

    [MenuItem("Tools/Editorials/Stack/Select _`")]
    public static void Select()
    {
        
        Selection.activeGameObject = GameObject.Find(stack[selectionIndex - 1]);
        
        selectionIndex = 1;
        HideWindow();
    }
}
