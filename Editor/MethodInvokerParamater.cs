using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

public class MethodInvokerParamater : EditorWindow
{
    static MethodInfo method;
    static ParameterInfo[] parameters;
    Vector2 view;
    static object[] parametersToReturn;

    public static void ShowWindow(MethodInfo m)
    {
        EditorWindow win = EditorWindow.GetWindow<MethodInvokerParamater>("Method Invoker");
        win.minSize = new Vector2(300, 400);
        method = m;
        parameters = m.GetParameters();
        parametersToReturn = new object[parameters.Length] ;
    }
    public void HideWindow()
    {
        EditorWindow win = EditorWindow.GetWindow<MethodInvokerParamater>("Method Invoker");
        win.Close();
       
    }

    public void OnGUI()
    {
        
        view = EditorGUILayout.BeginScrollView(view);
        if (parameters.Length > 0)
        {
            for (int i = 0; i < parameters.Length; i++)
            {

                if (parameters[i].ParameterType == typeof(string))
                {
                    parametersToReturn[i] = parametersToReturn[i] == null ? "Input Here" : parametersToReturn[i];
                    parametersToReturn[i] = EditorGUILayout.TextField(parameters[i].Name, (string)parametersToReturn[i]);

                }
                else if (parameters[i].ParameterType == typeof(int))
                {
                    
                    parametersToReturn[i] = parametersToReturn[i] == null? 0 : parametersToReturn[i];
                    parametersToReturn[i] = EditorGUILayout.IntField(parameters[i].Name, (int)parametersToReturn[i]);

                }
                else if (parameters[i].ParameterType == typeof(double))
                {
                    parametersToReturn[i] = parametersToReturn[i] == null ? 0 : parametersToReturn[i];
                    parametersToReturn[i] = EditorGUILayout.DoubleField(parameters[i].Name, (double)parametersToReturn[i]);

                }
                else if (parameters[i].ParameterType == typeof(bool))
                {
                    parametersToReturn[i] = parametersToReturn[i] == null ? false : parametersToReturn[i];
                    parametersToReturn[i] = EditorGUILayout.Toggle(parameters[i].Name, (bool)parametersToReturn[i]);

                }
                else
                {
                    Type t = parameters[i].ParameterType;

                    parametersToReturn[i] = EditorGUILayout.ObjectField(parameters[i].Name, (UnityEngine.Object)parametersToReturn[i], t, true);
                    parametersToReturn[i] = Convert.ChangeType(parametersToReturn[i], t);

                }
            }
        }
        else
        {
            EditorGUILayout.LabelField("No Parameters! If you want to invoke a method that uses refrences, make sure they are set as paramaters.");
        }
        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Invoke"))
        {
            object instance = Activator.CreateInstance(method.DeclaringType);
            if (parameters.Length > 0)
            {
                method.Invoke(instance, parametersToReturn);
                HideWindow();
            }
            else
            {
                method.Invoke(instance, null);
                HideWindow();
            }
        }
        EditorGUILayout.LabelField(method.Name);
    }

    public T ConvertObject<T>(object input)
    {
        return (T)Convert.ChangeType(input, typeof(T));
    }
}
