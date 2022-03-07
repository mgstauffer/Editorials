using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityToolbarExtender;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class DisplayScenes
{
    static string[] scenes;
    static int currentaridx = -1;
    static int aridx = 0;
    static DisplayScenes()
    {
        ToolbarExtender.RightToolbarGUI.Add(OnToolBarGUI);
        
    }

    static void OnToolBarGUI()
    {
        GUILayout.FlexibleSpace();
        
        if(ReadNames(true).Length > 0)
        {
            EditorGUI.BeginChangeCheck();
            aridx = EditorGUILayout.Popup(aridx, ReadNames(false));
            if (EditorGUI.EndChangeCheck())
            {
                //do stuff
                if (aridx != currentaridx && ReadNames(true).Length > 0 && EditorApplication.isPlaying == false)
                {
                    EditorSceneManager.OpenScene(ReadNames(true)[aridx]);
                    currentaridx = aridx;
                }
            }
        }
        else
        {
            string[] dummyEntries = {"<No Scenes Active In Build>"};
            EditorGUILayout.Popup(0, dummyEntries);
        }
        
        
    }

    
    static string[] ReadNames(bool full)
    {
        List<string> names = new List<string>();

        foreach (EditorBuildSettingsScene S in EditorBuildSettings.scenes)
        {
            if (S.enabled)
            {
                
                string name = S.path;
                if (!full)
                {
                    name = S.path.Substring(S.path.LastIndexOf('/') + 1);
                    name = name.Substring(0, name.Length - 6);
                }
                names.Add(name);
            }
        }


        return names.ToArray();
    }

    
}
