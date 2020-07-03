using UnityEngine;
using System.Collections;

public class StartPanningState : FSMState {
	
	public InputControllerScript IS;
	
	
	public override void Enter (FSMState lastState)
	{
	  

	}
	
	public override FSMEvent Process ()
	{
		  IS.possibleTap = true;
		  IS.possibleLongTAP = true;
		  IS.startedMovingPanning = false;
		  return new FSMEvent("Panning");
	  
		
		
	}
	
	public override void Leave (FSMState nextState)
	{
		
	}
}
