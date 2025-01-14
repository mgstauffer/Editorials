﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Linq;
using System.Linq.Expressions;

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
                    parametersToReturn[i] = EditorGUILayout.TextField(parameters[i].Name + "("+parameters[i].ParameterType+")", (string)parametersToReturn[i]);

                }
                else if (parameters[i].ParameterType == typeof(int))
                {
                    
                    parametersToReturn[i] = parametersToReturn[i] == null? 0 : parametersToReturn[i];
                    parametersToReturn[i] = EditorGUILayout.IntField(parameters[i].Name + "(" + parameters[i].ParameterType + ")", (int)parametersToReturn[i]);

                }
                else if (parameters[i].ParameterType == typeof(double))
                {
                    parametersToReturn[i] = parametersToReturn[i] == null ? 0 : parametersToReturn[i];
                    parametersToReturn[i] = EditorGUILayout.DoubleField(parameters[i].Name + "(" + parameters[i].ParameterType + ")", (double)parametersToReturn[i]);

                }
                else if (parameters[i].ParameterType == typeof(bool))
                {
                    parametersToReturn[i] = parametersToReturn[i] == null ? false : parametersToReturn[i];
                    parametersToReturn[i] = EditorGUILayout.Toggle(parameters[i].Name + "(" + parameters[i].ParameterType + ")", (bool)parametersToReturn[i]);

                }
                else
                {
                    Type t = parameters[i].ParameterType;

                    parametersToReturn[i] = EditorGUILayout.ObjectField(parameters[i].Name + "(" + parameters[i].ParameterType + ")", (UnityEngine.Object)parametersToReturn[i], t, true);
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

    
    public static MethodInfo GetMethodInfo(LambdaExpression expression)
    {
        MethodCallExpression outermostExpression = expression.Body as MethodCallExpression;

        if (outermostExpression == null)
        {
            throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");
        }

        return outermostExpression.Method;
    }

    public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression)
    {
        return GetMethodInfo((LambdaExpression)expression);
    }
}
