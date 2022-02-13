using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EditorialGizmo : MonoBehaviour
{
    [Range(1f, 40f)]
    public float size;
    public Transform drawPoint;
    public Color color;
    public bool mesh;
    public bool cube;
    public void OnDrawGizmos()
    {
        Gizmos.color = color;
        if (mesh)
        {
            if (cube)
            {
                Gizmos.DrawCube(drawPoint.position, new Vector3(size,size,size));
            }
            else
            {
                Gizmos.DrawSphere(drawPoint.position, size);
            }
        }
        else
        {
            if (cube)
            {
                Gizmos.DrawWireCube(drawPoint.position, new Vector3(size, size, size));
            }
            else
            {
                Gizmos.DrawWireSphere(drawPoint.position, size);
            }
        }
    }
    
}
