using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ClassCreater;


namespace ClassCreater.Window
{
    
    public class ClassCreaterWindow : EditorWindow
    {
        string className;
        string classInheritance;
        string copypath = "Assets/Scripts/";
        bool tog = true;
        [MenuItem("Tools/Editorials/ClassCreater")]
        public static void ShowWindow()
        {
            EditorWindow win = EditorWindow.GetWindow<ClassCreaterWindow>("Class Creater");
            win.maxSize = new Vector2(300, 100);
            win.minSize = new Vector2(300, 100);
        }

        void OnGUI()
        {
            
            tog = EditorGUILayout.Toggle("Inherit From MonoBehaviour", tog);
            if (tog)
            {
                className = EditorGUILayout.TextField("Name", className);
                classInheritance = "MonoBehaviour";
            }
            else
            {
                className = EditorGUILayout.TextField("Name", className);
                classInheritance = EditorGUILayout.TextField("Inheritance", classInheritance);
            }
            copypath = EditorGUILayout.TextField("Path", copypath);
            if(copypath == null)
            {
                copypath = "Assets/Scripts/";
            }
            if (GUILayout.Button("Create") && className.Length > 0 && classInheritance.Length > 0)
            {
                if (copypath[copypath.Length -1].ToString() != "/")
                {
                    copypath += "/";
                }
                CreateClass.Create(className, classInheritance, copypath);

            }
        }
    }

}
