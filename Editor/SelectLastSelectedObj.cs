using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityToolbarExtender;
using System.IO;


public class SelectLastSelectedObj : EditorWindow
{
    public static GameObject currentSelection;
    public static GameObject prevSelection;

    [MenuItem("Tools/Editorials/Last Selecter")]
    public static void ShowWindow()
    {
        EditorWindow win = EditorWindow.GetWindow<SelectLastSelectedObj>("Last Selecter");
        win.maxSize = new Vector2(250, 100);
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
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Select Last Selected Game Object - Alt+`(Reverse Tick)", EditorStyles.boldLabel);
        if (GUILayout.Button("Clear"))
        {
            prevSelection = null;
            currentSelection = null;
        }
        if(currentSelection != null)
            EditorGUILayout.LabelField("Current: " + currentSelection.name, EditorStyles.boldLabel);
        
        if(prevSelection != null)
            EditorGUILayout.LabelField("Previous: " + prevSelection.name,EditorStyles.boldLabel);
        
        
    }

    public void Save()
    {
        LastSelectionData dat = new LastSelectionData();
        dat.previous = prevSelection.name;
        dat.current = currentSelection.name;
        string dataAsJson = JsonUtility.ToJson(dat, true);
        string directory = ("Assets/Editorials/SelectData");

        string FilePath = directory + "/" + "ObjLastSelect" + ".select";
        DirectoryInfo dir = new DirectoryInfo(FilePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
        }



        File.WriteAllText(FilePath, dataAsJson);

    }
}
