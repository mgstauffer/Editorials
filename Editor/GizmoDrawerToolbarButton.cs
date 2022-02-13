using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolbarExtender;
using UnityEditor;

[InitializeOnLoad]
public static class GizmoDrawerToolbarButton
{
    static GizmoDrawerToolbarButton()
    {
        ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
    }

    static void OnToolbarGUI()
    {
        if (GUILayout.Button("GM", GUILayout.Width(30), GUILayout.Height(25)))
        {
            GizmoDrawer.ShowWindow();
        }
    }
}
