using UnityEngine;
using System.Collections;

public class ActionKillObject : Action {

	public GameObject Target;
	
	public override void ActionPerformed ()
	{
	   if(Target!=null)
		   GameObject.Destroy(Target);
	}
}
