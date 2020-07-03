using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(FSM))]
class FSMGraphEditor : Editor {
 
	
	public override void OnInspectorGUI() {
	 
    var fsm = (FSM)target;
	
	if(fsm.CurrentState)	
	   GUILayout.Label("Current State:"+fsm.CurrentState.StateName);	
		
    if(GUILayout.Button("Launch FSM Editor"))
        FSMEditor.Init(fsm);
  }
}
