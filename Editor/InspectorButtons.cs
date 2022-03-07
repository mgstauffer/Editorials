using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ClassCreater;
using System.Reflection;
using System;
using System.Linq;



public class InspectorButtons : EditorWindow
{
    
    public MonoScript source;
    List<MethodInfo> methodsToAdd = new List<MethodInfo>();
    List<MethodInfo> me = new List<MethodInfo>();
    List<string> methods;
    Vector2 view;
    bool hasInit = false;
    
    [MenuItem("Tools/Editorials/Method Invoker")]
    public static void ShowWindow()
    {
        EditorWindow win = EditorWindow.GetWindow<InspectorButtons>("Method Invoker");
        win.minSize = new Vector2(100, 100);
        
    }

    private void OnGUI()
    {
       
        if(methods == null)
        {
            methods = new List<string>();
        }
        GUILayout.Label("Add Buttons To An Object", EditorStyles.boldLabel);
        
        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginChangeCheck();
        UnityEngine.Object obj = EditorGUILayout.ObjectField(source, typeof(MonoScript), true);
        source = (MonoScript)obj;
        if (EditorGUI.EndChangeCheck())
        {
            hasInit = false;
        }
        if (!hasInit && source != null)
        {
            me = (source.GetClass().GetMethods().ToList<MethodInfo>());
            methodsToAdd.Clear();
            hasInit = true;
        }
        EditorGUILayout.EndHorizontal();
        if(source != null)
        {
            view = EditorGUILayout.BeginScrollView(view);
            foreach (MethodInfo method in me)
            {
                
                    if (GUILayout.Button(method.Name))
                    {
                        methodsToAdd.Add(method);
                        me.Remove(method);
                        
                        break;
                    }
                
            }
            EditorGUILayout.EndScrollView();
        }


        EditorGUILayout.LabelField("");
        
        
        //if (GUILayout.Button("Reset"))
        //{

        //}
        if (source != null)
        {
            if (GUILayout.Button("Generate"))
            {
                CreateCustomEditor.Create(source.name, null, methodsToAdd.ToArray(), source);

            }
        }

        if (methodsToAdd.Count > 0)
        {
            foreach (MethodInfo method in methodsToAdd)
            {
                EditorGUILayout.LabelField(method.Name);
            }
        }
    }


}
