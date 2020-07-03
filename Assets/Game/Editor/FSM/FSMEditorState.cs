using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

[Serializable()]
public class FSMEditorState  
{
	
	
	
	public FSMState State; 
	public int Id;
	static int width=200;
    //public bool MouseOver=false;
	
	
	public FSMEditorState(FSMState state,int id)
	{
		State = state;
		Id =id;
		if(state.Frame.width==0 && state.Frame.height==0)
			state.Frame = new Rect(0,0,width,50);
		if(state.Transitions ==null)
			state.Transitions = new List<FSMStateTransition> ();
		
	}
	
	float GetEventRectYOffset(int index)
	{
		return 45+index*21;
	}
	
	public Rect GetEventRect(int index,FSMEditorState destinationState)
	{
        if(destinationState!=null && destinationState.State.Frame.x<State.Frame.x)
         return new Rect(State.Frame.x-5, State.Frame.y + GetEventRectYOffset(index), 5, 20);
        else
		return  new Rect(State.Frame.x,State.Frame.y+GetEventRectYOffset(index),width,20);
	}

    public Rect GetHeaderRect(FSMEditorState sourceState)
    {
        if(sourceState.State.Frame.x < State.Frame.x)
           return new Rect(State.Frame.x, State.Frame.y, width, 20);
        else
            return new Rect(State.Frame.x+State.Frame.width, State.Frame.y, 5, 20);
    }

    public Rect GetTransitionRect(int index)
    {
        return new Rect(1, GetEventRectYOffset(index), width - 25, 20);
    }
	
	void RenderWindow(int windowId)
	{
		State.Frame.height = 30;
		State.Frame.width = width;
		
	    GUI.Box(new Rect(0,15,width-1,25),GUIContent.none); 
		
		GUI.Label(new Rect(5,20,width,20),"Events:");
		
		if(GUI.Button(new Rect(width -25, 20, 20, 15),new GUIContent("+","Add a new event")))
		{
			State.Transitions.Add(new FSMStateTransition());
		}
		
		State.Frame.height += State.Transitions.Count*21+18;
		
		for(int i=0;i<State.Transitions.Count;i++)
		{
		   var transition = State.Transitions[i];
		   var transitionRect = GetTransitionRect(i);
		   transition.EventName = GUI.TextField(transitionRect,transition.EventName);
		   if( GUI.Button(new Rect(width-25,GetEventRectYOffset(i),20,20), new GUIContent("-","Remove Event")))
		   { 
				State.Transitions.RemoveAt(i);
				return;
		   }
		   
		}
        GUI.DragWindow();
		
	}
	
	public void Render()
	{
		
		State.Frame = GUI.Window(Id,State.Frame,RenderWindow,State.StateName);
	}
	
}
