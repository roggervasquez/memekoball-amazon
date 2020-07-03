using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Collections;
using System;
using System.IO;


public class FSMScriptManager  {


    public static void CreateFSMScript(string className,string path="Assets/",string baseClass="FSMState")
    {

        string classCode = 
@"
using UnityEngine;
using System.Collections;
public class " +className+" : "+baseClass+ @"{
 
    public override void Enter(FSMState lastState)
    {
        base.Enter(lastState);
    }

    public override FSMEvent Process ()
	{
		return null;	
	}

    public override void Leave(FSMState nextState)
    {
        base.Leave(nextState);
    }

}";

        if(baseClass!="FSMState" && !FSMScriptManager.ScriptExists(baseClass))
            FSMScriptManager.CreateFSMScript(baseClass,path);

        string copyPath = path+className+".cs";
        Debug.Log("Creating Classfile: " + copyPath);
 
        if( File.Exists(copyPath) == false ){ // do not overwrite
            using (StreamWriter outfile = 
                new StreamWriter(copyPath))
                {
                    outfile.WriteLine(classCode);
                  
            }//File written
        }   
   
    }

    public static bool ScriptExists(string className)
    {
        GameObject dummy = new GameObject("Hadaikiri");
        var component = dummy.AddComponent(className);
        bool exists = component != null;
        GameObject.DestroyImmediate(dummy);
        return exists;
    }

    static bool IsValidFSMScript(MonoScript monoScriptAsset )
    {
       
        var monoScriptType = monoScriptAsset.GetClass();
        if (monoScriptType == null) return false;

        var parentType = monoScriptType.BaseType;
        while (parentType != null)
        {
            if (parentType.Name == "FSMState")
                return true;
            parentType = parentType.BaseType;
        }

        return false;

    }

    public static List<String> GetAllFSMStateScripts()
    {
        var scriptList = new List<string>();

        var scriptPaths = Directory.GetFiles("Assets", "*.cs", SearchOption.AllDirectories);
        foreach (var scriptPath in scriptPaths)
        {
            var scriptAsset = AssetDatabase.LoadAssetAtPath(scriptPath, typeof (UnityEngine.Object));
            if (!(scriptAsset is MonoScript)) continue;
            var monoScriptAsset = (MonoScript) scriptAsset;
            if (IsValidFSMScript(monoScriptAsset))
                scriptList.Add(monoScriptAsset.GetClass().Name);
        }
        return scriptList;
    }

}



