using UnityEngine;
using System.Collections;

public class StartZoomingState : FSMState {
	
	public InputControllerScript IS;
	
	
	public override void Enter (FSMState lastState)
	{
	   
	  
	   
	}
	
	public override FSMEvent Process ()
	{
		Messenger.Broadcast(Globals.InputEvents.InitZoomingEvent);
		
	    return new FSMEvent("Zooming");
	}
	
	public override void Leave (FSMState nextState)
	{
		
	}
}
