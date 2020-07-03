using UnityEngine;
using System.Collections;

public class EndMovingState : FSMState {
	
	public InputControllerScript IS;
	
	
	public override void Enter (FSMState lastState)
	{
	  
	  
	   
	}
	
	public override FSMEvent Process ()
	{
		Messenger.Broadcast(Globals.InputEvents.EndMovingEvent);
	    //IS.EndMovingEvent();
		return new FSMEvent("None");
	  
	
	}
	
	public override void Leave (FSMState nextState)
	{
	
	}
}
