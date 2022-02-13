using UnityEditor;
using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;


namespace ClassCreater
{
    public class CreateClass
    {
        
        public static void Create(string className, string inheritance, string path)
        {

            // remove whitespace and minus
            string name = className;
            if(path == null)
            {
                path = "Assets/Scripts/";
            }
            string copyPath = path + name + ".cs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Debug.Log("Creating Classfile: " + copyPath);
            if (File.Exists(copyPath) == false)
            { // do not overwrite
                using (StreamWriter outfile =
                    new StreamWriter(copyPath))
                {
                    outfile.WriteLine("using UnityEngine;");
                    outfile.WriteLine("using UnityEditor;");
                    outfile.WriteLine("using System.Collections;");
                    outfile.WriteLine("");
                    outfile.WriteLine("public class " + name + " : " + inheritance + " {");
                    outfile.WriteLine(" ");
                    outfile.WriteLine(" ");
                    outfile.WriteLine(" // Use this for initialization");
                    outfile.WriteLine(" void Start () {");
                    outfile.WriteLine(" ");
                    outfile.WriteLine(" }");
                    outfile.WriteLine(" ");
                    outfile.WriteLine(" ");
                    outfile.WriteLine(" // Update is called once per frame");
                    outfile.WriteLine(" void Update () {");
                    outfile.WriteLine(" ");
                    outfile.WriteLine(" }");
                    outfile.WriteLine("}");
                }//File written
            }
            AssetDatabase.Refresh();

        }
    }
}