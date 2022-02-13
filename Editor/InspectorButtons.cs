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

    List<string> methods;
    Vector2 view;
    
    [MenuItem("Tools/Editorials/Method Invoker")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<InspectorButtons>("Method Invoker");
        
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
                if (method.GetParameters().Length <= 0)
                {
                    if (GUILayout.Button(method.Name))
                    {
                        object instance = Activator.CreateInstance(method.DeclaringType);
                        method.Invoke(instance, null);

                    }
                }
            }
            EditorGUILayout.EndScrollView();
        }

        
        //if (GUILayout.Button("Reset"))
        //{

        //}
        //if(source != null)
        //{
        //    if (GUILayout.Button("Generate"))
        //    {
        //        CreateClass cc = new CreateClass();
        //        cc.Create(source.name);

        //    }
        //}
    }


}
