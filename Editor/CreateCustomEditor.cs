using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.IO;
using System.Text;
using System;


namespace ClassCreater
{
    public class CreateCustomEditor
    {

        public static void Create(string className, string path, MethodInfo[] methods, MonoScript mono)
        {
            StreamWriter outFile = null;
            Type t = mono.GetClass();
            // remove whitespace and minus
            string name = className + "Editor";
            if (path == null)
            {
                path = "Assets/Editor/";
            }
            string copyPath = path + name + ".cs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Debug.Log("Creating Classfile: " + copyPath);
            
                using (outFile = new StreamWriter(copyPath))
                {
                    
                    outFile.WriteLine("using UnityEngine;");
                    outFile.WriteLine("using UnityEditor;");
                    outFile.WriteLine("using System.Collections;");
                    outFile.WriteLine("using System.Reflection;");
                outFile.WriteLine("using System.Linq;");
                outFile.WriteLine("using System.Linq.Expressions;");
                outFile.WriteLine("");
                    outFile.WriteLine("[CustomEditor(typeof(" + t + "))]");
                    outFile.WriteLine("public class " + name + " : " + "Editor" + " {");
                    outFile.WriteLine(" ");
                    outFile.WriteLine(" ");
                    outFile.WriteLine(" // Use this for initialization");
                    outFile.WriteLine(" void Start () {");
                    outFile.WriteLine(" ");
                    outFile.WriteLine(" }");
                    outFile.WriteLine(" ");
                    outFile.WriteLine(" ");
                    outFile.WriteLine(" // Update is called once per frame");
                    outFile.WriteLine(" void Update () {");
                    outFile.WriteLine(" ");
                    outFile.WriteLine(" }");
                    outFile.WriteLine("public override void OnInspectorGUI(){");
                    outFile.WriteLine("base.OnInspectorGUI(); ");
                    outFile.WriteLine(t + " tar = (" + t + ")target;");
                    foreach (MethodInfo method in methods)
                    {
                        outFile.WriteLine(" ");
                        outFile.WriteLine(" if(GUILayout.Button(" + "\"" + method.Name + "\"" +  "))");
                        outFile.WriteLine(" {");
                        //outFile.WriteLine("   tar."+method.Name+"();");
                        outFile.WriteLine("MethodInfo methodInf = MethodInvokerParamater.GetMethodInfo<"+t+">(ta => ta." + method.Name + "("+ GetParamNulls(method) +"));");
                        outFile.WriteLine("MethodInvokerParamater.ShowWindow(methodInf);");
                        outFile.WriteLine(" }");
                    }
                    outFile.WriteLine("}");

                    outFile.WriteLine("}");
                    
                }//File written
                
            
            AssetDatabase.Refresh();

        }
        static object GetParamNulls(MethodInfo met)
        {
            string s = "";
            for (int i = 0; i < met.GetParameters().Length; i++)
            {
                if(i != met.GetParameters().Length - 1)
                {
                    if(met.GetParameters()[i].ParameterType == typeof(int))
                    {
                        s += "0,";
                    }
                    else if(met.GetParameters()[i].ParameterType == typeof(bool))
                    {
                        s += "false,";
                    }
                    else
                    {
                        s += "null,";
                    }
                    
                }
                else
                {
                    if (met.GetParameters()[i].ParameterType == typeof(int))
                    {
                        s += "0";
                    }
                    else if (met.GetParameters()[i].ParameterType == typeof(bool))
                    {
                        s += "false";
                    }
                    else
                    {
                        s += "null";
                    }
                    
                }
                
            }
            
            return s;
        }
    }

    
}