using UnityEngine;
using System.Collections;

public class MovingState : FSMState {
	
	public InputControllerScript IS;
	
	
	public override void Enter (FSMState lastState)
	{
	  
	  
	   
	}
	
	public override FSMEvent Process ()
	{
	   if (!IS.TOUCHINTERFACEDETECTED)
		{		
		 if (Input.GetMouseButton(1)) // Middle mouse button
			  Messenger.Broadcast(Globals.InputEvents.MovingEvent);	
			  	
		 else
			return new FSMEvent("EndMoving");
	  
		} // Else Touch
		else
		{
		  switch (Input.touchCount)
		     { 
				case 2:
			         Messenger.Broadcast(Globals.InputEvents.MovingEvent);	
				     break;
				
		        default :
			       return new FSMEvent("EndMoving");
		     }	
		}
		return null;
	}
	
	public override void Leave (FSMState nextState)
	{
		
	}
}
