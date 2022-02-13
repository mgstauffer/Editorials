using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DrawingType
{
    Wire = 0,
    Mesh = 1
}

public enum DrawingMesh
{
    Sphere = 0,
    Cube = 1
}
public class EditorialGizmo : MonoBehaviour
{
    [Range(1f, 40f)]
    public float size;
    public Transform drawPoint;
    public Color color;
    public DrawingType drawType;
    public DrawingMesh drawMesh;

    public void Start()
    {

    }

    public void Update()
    {

    }
    public void OnDrawGizmosSelected()
    {
        color.a = 255;
        Gizmos.color = color;
        if (drawType == DrawingType.Mesh)
        {
            if (drawMesh == DrawingMesh.Cube)
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
            if (drawMesh == DrawingMesh.Cube)
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
