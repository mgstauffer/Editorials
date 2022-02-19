using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Website))]
public class WebsiteEditor : Editor
{
    public override void OnInspectorGUI()
    {
          
        Website website = (Website)target;
        if(GUILayout.Button("Nix Studio"))
        {
            website.Open();
        }
    }
}
