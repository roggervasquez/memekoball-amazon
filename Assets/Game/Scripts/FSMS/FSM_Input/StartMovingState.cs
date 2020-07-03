using UnityEngine;
using System.Collections;

public class StartMovingState : FSMState {
	
	public InputControllerScript IS;
	
	
	public override void Enter (FSMState lastState)
	{
	
	  
	   
	}
	
	public override FSMEvent Process ()
	{
		Messenger.Broadcast(Globals.InputEvents.InitMovingEvent);
	    return new FSMEvent("Moving");
	}
	
	public override void Leave (FSMState nextState)
	{
		
	}
}
