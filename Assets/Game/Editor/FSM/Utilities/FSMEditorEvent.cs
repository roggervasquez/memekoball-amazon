using UnityEngine;
using System.Collections;
using System;


public enum FSMEditorEventType{MouseDown,MouseUp,KeyDown,KeyUp,None}

[Serializable()]
public class FSMEditorEvent
{
	
	#region singleton
	static FSMEditorEvent instance;
	
	public static FSMEditorEvent Instance
	{
		get
		{
		   if(instance==null) 
		      instance = new FSMEditorEvent(); 
		   return instance;
		}
	}
	
	private FSMEditorEvent () {}
    #endregion
	
    public FSMEditorEventType EditorEventType;
	public Vector2 MousePosition;
	    
	public void CatchEvent()
	{
		  EditorEventType = FSMEditorEventType.None;
		
		  if(Event.current.type == UnityEngine.EventType.MouseDown)
		     EditorEventType = FSMEditorEventType.MouseDown;
		  if(Event.current.type == UnityEngine.EventType.MouseUp)
		     EditorEventType = FSMEditorEventType.MouseUp;
		  
		//  Debug.LogWarning(Event.current.mousePosition);
		  MousePosition = Event.current.mousePosition;
	}
	
}
