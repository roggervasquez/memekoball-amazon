using UnityEngine;
using System.Collections;

public class TapState: FSMState {
	
	public InputControllerScript IS;
	
	
	public override void Enter (FSMState lastState)
	{
	  
	  
	   
	}
	
	public override FSMEvent Process ()
	{
		 Ray ray = Camera.main.ScreenPointToRay(IS.PanTouchInitialPos);
         RaycastHit hit;
		 string Taghit="";
	   	 if (Physics.Raycast(ray, out hit, Mathf.Infinity))
		  {
			 Taghit=hit.transform.tag;
										
		  }
	
		Messenger.Broadcast<InputData>(Globals.InputEvents.TapEvent_InputData,
			                 new InputData(IS.PanTouchEndPos,IS.PanTouchEndTime,Taghit));
			
		return new FSMEvent("None");
	  
		
	}
	
	public override void Leave (FSMState nextState)
	{
		
	}
}
