using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ObjectSelectorWindow : EditorWindow
{
    GameObject g1, g2, g3, g4, g5;
    
    [MenuItem("Tools/Editorials/Object HotKey Selector Window")]
    public static void ShowWindow()
    {
        EditorWindow win = EditorWindow.GetWindow<ObjectSelectorWindow>("Object HotKey Selector Window");
        win.maxSize = new Vector2(300, 110);
        win.maxSize = win.maxSize;
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Object 1 - Alt+Shift+1");
        UnityEngine.Object obj = EditorGUILayout.ObjectField(g1, typeof(GameObject), true);
        g1 = (GameObject)obj;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Object 2 - Alt+Shift+2");
        UnityEngine.Object obj2 = EditorGUILayout.ObjectField(g2, typeof(GameObject), true);
        g2 = (GameObject)obj2;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Object 3 - Alt+Shift+3");
        UnityEngine.Object obj3 = EditorGUILayout.ObjectField(g3, typeof(GameObject), true);
        g3 = (GameObject)obj3;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Object 4 - Alt+Shift+4");
        UnityEngine.Object obj4 = EditorGUILayout.ObjectField(g4, typeof(GameObject), true);
        g4 = (GameObject)obj4;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Object 5 - Alt+Shift+5");
        UnityEngine.Object obj5 = EditorGUILayout.ObjectField(g5, typeof(GameObject), true);
        g5 = (GameObject)obj5;
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Save"))
        {
            Save();
        }

    }

    public void Save()
    {
        SelectionData dat = new SelectionData();
        

        
        dat.objs[0] = g1 != null ? g1.name : "empty";   
        dat.objs[1] = g2 != null ? g2.name : "empty";
        dat.objs[2] = g3 != null ? g3.name : "empty";
        dat.objs[3] = g4 != null ? g4.name : "empty";
        dat.objs[4] = g5 != null ? g5.name : "empty";

        string dataAsJson = JsonUtility.ToJson(dat, true);
        string directory = ("Assets/Editorials/SelectData");

        string FilePath = directory + "/" + "ObjSelect" + ".select";
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

    [MenuItem("Tools/Editorials/ObjSelect/1 &#1")]
    public static void Select1()
    {
        if (Select().objs[0] != "empty")
        {
            Selection.activeGameObject = GameObject.Find(Select().objs[0]);
        }
        else
        {
            Debug.LogWarning("object 1 is empty");
        }
        
    }
    [MenuItem("Tools/Editorials/ObjSelect/2 &#2")]
    public static void Select2()
    {
        if (Select().objs[1] != "empty")
        {
            Selection.activeGameObject = GameObject.Find(Select().objs[1]);
        }
        else
        {
            Debug.LogWarning("object 2 is empty");
        }
    }
    [MenuItem("Tools/Editorials/ObjSelect/3 &#3")]
    public static void Select3()
    {
        if (Select().objs[2] != "empty")
        {
            Selection.activeGameObject = GameObject.Find(Select().objs[2]);
        }
        else
        {
            Debug.LogWarning("object 3 is empty");
        }
    }

    [MenuItem("Tools/Editorials/ObjSelect/4 &#4")]
    public static void Select4()
    {
        if (Select().objs[3] != "empty")
        {
            Selection.activeGameObject = GameObject.Find(Select().objs[3]);
        }
        else
        {
            Debug.LogWarning("object 4 is empty");
        }
    }

    [MenuItem("Tools/Editorials/ObjSelect/5 &#5")]
    public static void Select5()
    {
        if (Select().objs[4] != "empty")
        {
            Selection.activeGameObject = GameObject.Find(Select().objs[4]);
        }
        else
        {
            Debug.LogWarning("object 5 is empty");
        }
    }

    static SelectionData Select()
    {
        string loadedLevel = File.ReadAllText("Assets/Editorials/SelectData/ObjSelect.select");
        SelectionData data = JsonUtility.FromJson<SelectionData>(loadedLevel);
        
        return data;
    }
}
