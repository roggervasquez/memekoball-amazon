using UnityEngine;
using System.Collections;

public class ActionFSMSendMessage : Action {
	
	public FSM MyFSM;
	public string MethodName;
	public string [] Arguments; 
	public override void ActionPerformed ()
	{
		MyFSM.SendMessageToCurrentState(MethodName,Arguments);
	}
	
}
