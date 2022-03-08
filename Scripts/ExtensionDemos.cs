using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtensionDemos : MonoBehaviour
{
    public void Start()
    {
        gameObject.CreateObject("bobby", Vector3.zero, Quaternion.identity, typeof(Website), typeof(EditorialGizmo), typeof(Example));
    }
}
