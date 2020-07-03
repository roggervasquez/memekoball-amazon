using UnityEngine;
using System.Collections;

public class NoneState : FSMState {
	
	public InputControllerScript IS;
	
	
	public override void Enter (FSMState lastState)
	{
	   
	  
	   
	}
	
	public override FSMEvent Process ()
	{
		 //If not enable the input controller, dont process the FSM
	  if(!IS.enableInputController) return null;
		
     if (!IS.TOUCHINTERFACEDETECTED)
	 {		
			 if (Input.GetMouseButtonDown(0))
			  {
						if(IS.nGuiCamera!=null)  //Exists a Ngui Camera
				          {
				             Ray ray = IS.nGuiCamera.ScreenPointToRay(Input.mousePosition);
				             RaycastHit hit	;
					       	 
				              if (Physics.Raycast(ray, out hit, Mathf.Infinity))
							   {
                                  
					              if(LayerMask.LayerToName(hit.transform.gameObject.layer)=="UILayer")
							          return null; // Do not process and leave, let NGUI process it
								}
				           }
						
					     
				       	 IS.PanTouchInitialPos = Input.mousePosition;
					     IS.PanTouchInitialTime = Time.fixedTime;
					
					     return new FSMEvent("StartPanning");   
			  }
					
			
		      if(Input.GetMouseButtonDown(1)) //  moving Middle
	             {
			      
		            
		            return new FSMEvent("StartMoving");  
	              }
			  else
			    {
				   float deltaScroll= Input.GetAxis("Mouse ScrollWheel");
				   if (deltaScroll<0||deltaScroll>0)
		            {
				 	
		              return new FSMEvent("StartZooming");   
		            }
			    }	
		} // else Touch 
		else
		{
			 switch (Input.touchCount)
				   { 
				         // This could start a TAP, a LongTAP or Init Pan Swipe
				         case 1:
					
					           IS.theTouch0 = Input.GetTouch(0);
					           
					           if(IS.nGuiCamera!=null)  //Exists a Ngui Camera
					          {
					             Ray ray = IS.nGuiCamera.ScreenPointToRay(IS.theTouch0.position);
					             RaycastHit hit	;
						       	 
					              if (Physics.Raycast(ray, out hit, Mathf.Infinity))
								   {
						              if(LayerMask.LayerToName(hit.transform.gameObject.layer)=="UILayer")
								          return null; // Do not process and leave, let NGUI process it
									}
					           }
					    
					           
					           if (IS.theTouch0.phase == TouchPhase.Began)
					           {
					                IS.PanTouchInitialPos = IS.theTouch0.position;
					                IS.PanTouchInitialTime = Time.fixedTime;
					                return new FSMEvent("StartPanning");   
					            }
					              
					     break;
						 case 2: 
					            
					            IS.theTouch0 = Input.GetTouch(0);
					            IS.theTouch1 = Input.GetTouch(1);
				                return new FSMEvent("DetectZooming");   
						             	
          			           // break;
						 case 3: // Not needed at this time 
							         
					            break;
					   
						default : break;
					
				   }
			
			
		}
		
		
		
		return null;
	}
	
	public override void Leave (FSMState nextState)
	{
		
	}
}
