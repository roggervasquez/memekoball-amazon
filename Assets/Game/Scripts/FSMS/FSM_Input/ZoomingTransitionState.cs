using UnityEngine;
using System.Collections;

public class ZoomingTransitionState : FSMState {
	
	public InputControllerScript IS;
	
	
	public override void Enter (FSMState lastState)
	{
	 
	   
	}
	
	public override FSMEvent Process ()
	{
		 if (Input.touchCount==2) //  zooming 
				      {
					
					  
					      IS.theTouch0=Input.GetTouch(0);
					      IS.theTouch1=Input.GetTouch(1);
						  IS.lastZoomDistance=Vector2.Distance(IS.theTouch0.position,IS.theTouch1.position);
					      
					      float diferencia = IS.lastZoomDistance - IS.initialDistanceZoom;
				          IS.initialDistanceZoom = IS.lastZoomDistance;
					      return new FSMEvent("StartZooming"); 
				                 
					   
					  }
				     
	    return null; 
	}
	
	public override void Leave (FSMState nextState)
	{
		
	}
}
