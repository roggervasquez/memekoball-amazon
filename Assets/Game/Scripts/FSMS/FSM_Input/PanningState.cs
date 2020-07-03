using UnityEngine;
using System.Collections;

public class PanningState : FSMState {
	
	public InputControllerScript IS;
	
	
	public override void Enter (FSMState lastState)
	{

      
	   
	}
	
	public override FSMEvent Process ()
	{
	  
	   if (!IS.TOUCHINTERFACEDETECTED)
			
			 if (Input.GetMouseButton(0))  // left mouse button
				 {
					  // Mouse click is very fast, that is why we test delta time 
					  float distancia = Vector3.Distance(IS.PanTouchInitialPos, Input.mousePosition);
				      if(Time.fixedTime > (IS.PanTouchInitialTime + IS.deltaMouseLONGTAP)
						|| distancia > 0 )  // it cannot be a tap or long tap, must be panning
						
					    {
					         IS.possibleTap=false;
						     IS.possibleLongTAP=false;
						      
						     if (!IS.startedMovingPanning)
						     {
					
					             Ray ray = Camera.main.ScreenPointToRay(IS.PanTouchInitialPos);
        						 RaycastHit hit;
					             string Taghit="";
	   							 if (Physics.Raycast(ray, out hit, Mathf.Infinity))
									{
			                           Taghit=hit.transform.tag;
										
									}
                               
					             Messenger.Broadcast<InputData>(Globals.InputEvents.InitPanEvent_InputData,
						               new InputData(IS.PanTouchInitialPos,IS.PanTouchInitialTime,Taghit));
							    
					             IS.startedMovingPanning = true;
							  
						     }
						     else
						     {
						        IS.PanTouchEndPos = Input.mousePosition;
						        IS.PanTouchEndTime = Time.fixedTime;
					            Messenger.Broadcast<InputData>(Globals.InputEvents.MiddlePanEvent_InputData,
						                              new InputData(IS.PanTouchEndPos,IS.PanTouchEndTime));
							
							  
						     }
						
					    }
					    return null;  //stay in the panning state
						
					}
					else // possible  Tap or long tap or end panning
					{
			
					  
					     if(Time.fixedTime <= (IS.PanTouchInitialTime + IS.deltaMouseTAP))
					     {
						   	IS.PanTouchEndPos=Input.mousePosition;
							IS.PanTouchEndTime = Time.fixedTime;
				            return new FSMEvent("Tap"); 
					     }
					     else
					     {
						   float dist = Vector3.Distance(Input.mousePosition,IS.PanTouchInitialPos);
         			       if (Time.fixedTime <= (IS.PanTouchInitialTime + IS.deltaMouseLONGTAP)
							      &&dist<=0.01)
						       {
							          
							           IS.PanTouchEndTime = Time.fixedTime;
							           IS.PanTouchEndPos = Input.mousePosition;
					                    return new FSMEvent("LongTap"); 
						       }
						   else  //End of panning
						     {
							        
							          IS.PanTouchEndTime = Time.fixedTime;
							          IS.PanTouchEndPos = Input.mousePosition;
					                   return new FSMEvent("EndPanning"); 
					
						     }
						  }
				    }  
			      else  // touch interface detected
		         {
			          if (Input.touchCount==1) 
				     {
					   IS.theTouch0 = Input.GetTouch(0);
				      // print("Delta:"+IS.theTouch0.deltaPosition.sqrMagnitude + IS.theTouch0.phase );
					   switch(IS.theTouch0.phase)   
					    {
					       case TouchPhase.Ended :
						   case TouchPhase.Canceled:
						       //  TAP, Long TAP or END panning
						       if (IS.possibleTap==false&&IS.possibleLongTAP==false) 
						       {	
							      IS.PanTouchEndPos = IS.theTouch0.position;
							      IS.PanTouchEndTime = Time.fixedTime;
							    }   
						      else  // Tap
						        if (Time.fixedTime <= IS.PanTouchInitialTime + IS.deltaTAP)
						        {
							        IS.PanTouchEndPos=IS.theTouch0.position;
							        IS.PanTouchEndTime = Time.fixedTime;
						            return new FSMEvent("Tap"); 
						        }
						        else  // long TAP
						         {
							         
							           IS.PanTouchEndTime = Time.fixedTime;
							           IS.PanTouchEndPos = IS.theTouch0.position;
						               return new FSMEvent("LongTap"); 
						         }
						   break;
					
						   case TouchPhase.Moved :
						     
					         if ((IS.possibleTap==true&&IS.possibleLongTAP==true)) 
					         {  
						        if (IS.theTouch0.deltaPosition.sqrMagnitude<IS.deltaPanVsLongTap)
						           return null; // Maybe a Tap or longtap
					         } 
						     IS.possibleTap=false;
						     IS.possibleLongTAP=false;
						      
						     if (!IS.startedMovingPanning)
						     {
						
						         Ray ray = Camera.main.ScreenPointToRay(IS.PanTouchInitialPos);
        						 RaycastHit hit;
					             string Taghit="";
	   							 if (Physics.Raycast(ray, out hit, Mathf.Infinity))
									{
			                           Taghit=hit.transform.tag;
										
									}
						
						       Messenger.Broadcast<InputData>(Globals.InputEvents.InitPanEvent_InputData,
						               new InputData(IS.PanTouchInitialPos,IS.PanTouchInitialTime,Taghit));
							 
							    IS.startedMovingPanning = true;
						     }
						     else
						     {
						        IS.PanTouchEndPos = IS.theTouch0.position;
						        IS.PanTouchEndTime = Time.fixedTime;
						        Messenger.Broadcast<InputData>(Globals.InputEvents.MiddlePanEvent_InputData,
						                              new InputData(IS.PanTouchEndPos,IS.PanTouchEndTime));
							
						       
						     }
							 
					         
						   break;
					       
					    }
					
				     }
				     else  
				     {
					      return new FSMEvent("EndPanning"); 
					 }
			
			
		          }
		
		return null;
	}
	
	public override void Leave (FSMState nextState)
	{
		
	}
}
