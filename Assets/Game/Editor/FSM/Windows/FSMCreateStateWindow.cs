using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection.Emit;
using System.Threading;
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Linq;

public class FSMCreateStateWindow : EditorWindow {

    #region Window Invocation
    static FSMCreateStateWindow window;
    public static void Init(FSMEditor FSMEditor)
    {
        if (window)
            window.Close();

        window = EditorWindow.GetWindow<FSMCreateStateWindow>();
        window.MyFSMEditor = FSMEditor;
        window.Start();
    }
    #endregion

    

    public FSMEditor MyFSMEditor;

    private string stateName;
    private string newScriptName;
    private int selectedScriptCreationOptionIndex = 0;
    private int selectedExistingScriptIndex = 0;
    private string [] scriptList; 
    private bool isScriptCompiling = false;


    void Start()
    {
        scriptList = FSMScriptManager.GetAllFSMStateScripts().ToArray();
        Array.Sort<string>(scriptList);
        
    }

    void CreateState(string scriptName)
    {
        GameObject newStateGameObject = new GameObject(stateName);
        try
        {
            var component = newStateGameObject.AddComponent(scriptName);
            if (component == null)
                throw new Exception("Type " + scriptName + " not found");

            component.transform.parent = MyFSMEditor.MyFSM.transform;
            isScriptCompiling = false;

            Debug.Log("State created");
            FSMEditor.Init(MyFSMEditor.MyFSM);
            this.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    void ValidateCreatedNewScript()
    {
        if (FSMScriptManager.ScriptExists(newScriptName))
        {
            isScriptCompiling = false;
            CreateState(newScriptName);
        }
    }
    void ProcessCreateState()
    {
        
        if (selectedScriptCreationOptionIndex == 0)
        {
            if (!FSMScriptManager.ScriptExists(newScriptName))
            {
                FSMScriptManager.CreateFSMScript(newScriptName,MyFSMEditor.MyFSM.DefaultScriptPath,MyFSMEditor.MyFSM.BaseStateScriptClass);
                AssetDatabase.Refresh();
                isScriptCompiling = true;
            }
            else
                Debug.LogError("Script " + stateName + " already exists!!!");
            
        }
        else if (selectedScriptCreationOptionIndex == 1)
        {
            string existingScriptName = scriptList[selectedExistingScriptIndex];
            CreateState(existingScriptName);
        }
    }

    void Update()
    {
        if (isScriptCompiling)
           ValidateCreatedNewScript();
    }


    void OnGUI()
    {
       
        GUILayout.Label("Create a new state", EditorStyles.boldLabel);
        MyFSMEditor.MyFSM.DefaultScriptPath = EditorGUILayout.TextField("Script Path", MyFSMEditor.MyFSM.DefaultScriptPath);
        MyFSMEditor.MyFSM.BaseStateScriptClass = EditorGUILayout.TextField("Base Class", MyFSMEditor.MyFSM.BaseStateScriptClass);
        stateName = EditorGUILayout.TextField("State Name", stateName);
        selectedScriptCreationOptionIndex = EditorGUILayout.Popup("Script Selection", selectedScriptCreationOptionIndex,
                                                                   new string[] {"New Script", "Use Existing Script"});
        if(selectedScriptCreationOptionIndex==0)
          newScriptName = EditorGUILayout.TextField("Script Name", newScriptName);
        else
          selectedExistingScriptIndex = EditorGUILayout.Popup("Script", selectedExistingScriptIndex, scriptList);


        if (!isScriptCompiling)
        {
            if (GUILayout.Button("Create"))
                  ProcessCreateState();
        }
        else
            GUILayout.Label("Please wait while the script compiles :)");

    }

  

}
