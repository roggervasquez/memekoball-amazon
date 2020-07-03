using UnityEngine;
using System.Collections;

public class DetectZoomingState : FSMState {
	
	public InputControllerScript IS;
	
	
	public override void Enter (FSMState lastState)
	{
	   
	  IS.initialDistanceZoom = Vector2.Distance(IS.theTouch0.position,IS.theTouch1.position);
	   
	}
	
	public override FSMEvent Process ()
	{
		 IS.initialDistanceZoom = Vector2.Distance(IS.theTouch0.position,IS.theTouch1.position);
	    return new FSMEvent("ZoomingTransition");
	}
	
	public override void Leave (FSMState nextState)
	{
		
	}
}
