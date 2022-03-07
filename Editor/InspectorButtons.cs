using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ClassCreater;
using System.Reflection;
using System;



public class InspectorButtons : EditorWindow
{
    
    public MonoScript source;
    List<MethodInfo> methodsToAdd = new List<MethodInfo>();
    List<string> methods;
    Vector2 view;
    
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
        UnityEngine.Object obj = EditorGUILayout.ObjectField(source, typeof(MonoScript), true);
        source = (MonoScript)obj;
        EditorGUILayout.EndHorizontal();
        if(source != null)
        {
            List<MethodInfo> me = new List<MethodInfo>();
            List<MethodInfo> baseme = new List<MethodInfo>();
            me.AddRange(source.GetClass().GetMethods());
            baseme.AddRange(source.GetType().GetMethods());
            
            

            view = EditorGUILayout.BeginScrollView(view);
            foreach (MethodInfo method in me)
            {
                if(method.GetParameters().Length <= 0)
                {
                    if (GUILayout.Button(method.Name))
                    {
                        methodsToAdd.Add(method);

                    }
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
