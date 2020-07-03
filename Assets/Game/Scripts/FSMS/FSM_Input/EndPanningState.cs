using UnityEngine;
using System.Collections;

public class EndPanningState : FSMState {
	
	public InputControllerScript IS;
	
	
	public override void Enter (FSMState lastState)
	{
	  
	  
	   
	}
	
	public override FSMEvent Process ()
	{
		Messenger.Broadcast<InputData>(Globals.InputEvents.EndPanEvent_InputData,
			                           new InputData(IS.PanTouchEndPos,IS.PanTouchEndTime)  );
	   
		return new FSMEvent("None");
	  
	
	}
	
	public override void Leave (FSMState nextState)
	{
	
	}
}
