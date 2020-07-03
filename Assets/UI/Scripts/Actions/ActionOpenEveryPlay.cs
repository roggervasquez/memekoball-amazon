using UnityEngine;
using System.Collections;

public class ActionOpenEveryPlay : Action
{

	
	
	public override void ActionPerformed ()
	{
        Debug.Log("Show EveryPlay");
        if(Everyplay.SharedInstance != null) 
             Everyplay.SharedInstance.Show();
	}
}
