using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolbarExtender;
using ClassCreater;
using ClassCreater.Window;
using UnityEditor;

[InitializeOnLoad]
public static class CreateClassToolbarItem
{
    
    static CreateClassToolbarItem()
    {
        ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
    }

    static void OnToolbarGUI()
    {
        GUILayout.FlexibleSpace();
        

        if(GUILayout.Button("Create Script"))
        {
            ClassCreaterWindow.ShowWindow();
        }
    }
}
