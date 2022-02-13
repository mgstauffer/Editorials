using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityToolbarExtender;


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
        Selection.activeGameObject = prevSelection;
    }
    void OnSelectionChange()
    {
        prevSelection = currentSelection;
        currentSelection = Selection.activeGameObject;
        
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

    
}
