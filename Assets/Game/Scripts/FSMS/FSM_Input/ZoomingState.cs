using UnityEngine;
using System.Collections;

public class ZoomingState : FSMState {
	
	public InputControllerScript IS;
	
	
	public override void Enter (FSMState lastState)
	{
	  
	  
	   
	}
	
	public override FSMEvent Process ()
	{
	  
		 if(IS.TOUCHINTERFACEDETECTED==false)
		{	
		  float deltaValue= Input.GetAxis("Mouse ScrollWheel");
		  if (deltaValue<0||deltaValue>0)
			{
			    if(deltaValue<0 )
			    {
					Messenger.Broadcast<float>(Globals.InputEvents.ZoomingEvent_float,
						                       IS.zoomDelta);
					
			    }
			    else 
			    { 
				   	Messenger.Broadcast<float>(Globals.InputEvents.ZoomingEvent_float,
						                       -IS.zoomDelta);
					
			    }
			}
		    else
			    return new FSMEvent("EndZooming");
		
		} // Else Touch interfase detected
		{
		    if (Input.touchCount==2) //  zooming 
				      {
					      // update touch positions
					      IS.theTouch0=Input.GetTouch(0);
					      IS.theTouch1=Input.GetTouch(1);
						  IS.lastZoomDistance=Vector2.Distance(IS.theTouch0.position,IS.theTouch1.position);
					      
					      float diferencia = IS.lastZoomDistance - IS.initialDistanceZoom;
				          IS.initialDistanceZoom = IS.lastZoomDistance;
					      
					  
						  float signo=0F ;
					      if (diferencia>0) 
						      signo=-1F;
					      else
						     if (diferencia<0)
							   signo=1F;
					
				
						   if (Mathf.Abs(diferencia)>IS.minZoomFingerDistance)
				 	       {  
						        Messenger.Broadcast<float>(Globals.InputEvents.ZoomingEvent_float,
						                       IS.zoomDelta*signo);
				           }
						   else 
					       {
				                	Messenger.Broadcast<float>(Globals.InputEvents.ZoomingEvent_float,0F);
				
						   }
				      }
				      else 
					      return new FSMEvent("EndZooming");
		}
		
		
		return null;
	}
	
	public override void Leave (FSMState nextState)
	{
		
	}
}
