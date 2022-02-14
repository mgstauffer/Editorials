using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


    public enum DrawType
    {
        Wire = 0,
        Mesh = 1
    }

    public enum DrawMesh
    {
        Sphere = 0,
        Cube = 1
    }


    public class GizmoDrawer : EditorWindow
    {
        public DrawType drawType;
        public DrawMesh drawMesh;
        float size;
        GameObject todraw;
        Transform drawpoint;
        Color color;

        [MenuItem("Tools/Editorials/Draw Gizmo")]
        public static void ShowWindow()
        {
            EditorWindow win = GetWindow<GizmoDrawer>("Draw Gizmo");
            win.maxSize = new Vector2(300, 185);
            win.minSize = win.maxSize;
        }

        void OnGUI()
        {

            EditorGUILayout.LabelField("Object To Place The Gizmo On", EditorStyles.boldLabel);
            Object obj = EditorGUILayout.ObjectField(todraw, typeof(GameObject), true);
            todraw = obj as GameObject;
            EditorGUILayout.LabelField("Gizmo Point Of Origin", EditorStyles.boldLabel);
            Object transf = EditorGUILayout.ObjectField(drawpoint, typeof(Transform), true);
            drawpoint = transf as Transform;
            drawType = (DrawType)EditorGUILayout.EnumPopup("Look Of The Gizmo: ", drawType);
            drawMesh = (DrawMesh)EditorGUILayout.EnumPopup("Shape Of The Gizmo: ", drawMesh);
            color = EditorGUILayout.ColorField("Gizmo Color: ", color);
            size = EditorGUILayout.Slider("Gizmo Size: ", size, 1, 40);
            if (GUILayout.Button("Draw Gizmo"))
            {
                if (todraw != null && drawpoint != null)
                {
                    DrawGizmoOnObject(drawType, drawMesh);
                }
                else
                {
                    Debug.LogError("Please Fill Both The DrawPoint And DrawObject Fields");
                    return;
                }
            }
        }

        void DrawGizmoOnObject(DrawType dt, DrawMesh dm)
        {
            EditorialGizmo giz;
            if(!todraw.GetComponent<EditorialGizmo>())
            {
                giz = todraw.AddComponent<EditorialGizmo>();
            }
            else
            {
                Debug.LogError("Could not add gizmo, -" + todraw.name + "- already has one");
                return;
            }
            

            switch (dt)
            {
                case DrawType.Mesh:
                    giz.drawType = DrawingType.Mesh;
                    break;
                case DrawType.Wire:
                    giz.drawType = DrawingType.Wire;
                    break;
                default:
                    Debug.LogError("Invalid draw type (Did you add a custom draw type and not set it up properly?)");
                    break;
            }
            switch (dm)
            {
                case DrawMesh.Cube:
                    giz.drawMesh = DrawingMesh.Cube;
                    break;
                case DrawMesh.Sphere:
                    giz.drawMesh = DrawingMesh.Sphere;
                    break;
                default:
                    Debug.LogError("Invalid mesh type (Did you add a custom mesh type and not set it up properly?)");
                    break;
            }
            giz.color = color;
            giz.size = size;
            giz.drawPoint = drawpoint;

        }
    }

