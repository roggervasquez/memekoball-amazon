using UnityEngine;
using System.Collections;
using System;

[Serializable()]
public class FSMStateTransition  {
	
	public string EventName;
	public string  ToState;
	
	
	public FSMStateTransition()
	{
		EventName ="";
		ToState="";
		
	}
	
}
