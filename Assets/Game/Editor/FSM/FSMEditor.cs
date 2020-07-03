using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;


public class FSMEditor: EditorWindow{
	
	#region Window Invocation
	static FSMEditor window;
    public static void Init(FSM currentFSMEdited)
    {
	   if(window)
			window.Close();

       window = EditorWindow.GetWindow<FSMEditor>();
	   window.MyFSM = currentFSMEdited;
	   window.Start();
	   
    }
	#endregion
	

	public FSM MyFSM;
	public FSMEditorState [] EditorStates;
    public Dictionary<string,FSMEditorState> EditorStatesIndex;
    FSMStatesControl FSMStatesControl;
    FSMCreateStateControl FSMCreateStateControl;
	
	
	public void Start()
	{
		
		this.title = "Xtudio 16 FSM Editor";
		FSMState [] states = MyFSM.GetComponentsInChildren<FSMState>();
		EditorStates = new FSMEditorState[states.Length];
		EditorStatesIndex = new Dictionary<string, FSMEditorState>();
		int id=0;
		foreach(var state in states)
		{
			
			var editorState = new FSMEditorState(state,id);
			EditorStates[id] = editorState;
			EditorStatesIndex.Add(state.StateName,editorState);
			id++;
		}
        FSMStatesControl = new FSMStatesControl(this);
        FSMCreateStateControl = new FSMCreateStateControl(this);
	}

    bool ValidateIntegrity()
    {
        if (EditorStates == null || FSMCreateStateControl==null|| FSMStatesControl==null)
        {   
            if(this.MyFSM!=null)
              FSMEditor.Init(this.MyFSM);
            else
              this.Close();
              
            return false;
        }
        return true;
    }
 
	void OnGUI()
	{
	      
        if (!ValidateIntegrity()) return;
        FSMEditorEvent.Instance.CatchEvent();
	    FSMCreateStateControl.Render();
        FSMStatesControl.Render();
      
    }
}
	
