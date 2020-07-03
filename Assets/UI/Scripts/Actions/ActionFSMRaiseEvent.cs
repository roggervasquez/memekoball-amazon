using UnityEngine;
using System.Collections;

public class ActionFSMRaiseEvent : Action {
	
	public FSM MyFSM;
	
	public string EventName;
	
	
	public override void ActionPerformed ()
	{
		MyFSM.RaiseEvent(EventName);
	}
	
	
	
	
}
