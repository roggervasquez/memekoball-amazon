using UnityEngine;
using System.Collections;


public class ActionActive:Action
{
	public bool Active=true;
    public GameObject Target;	
	
	public override void ActionPerformed ()
	{
		if(Target!=null)
			Target.SetActive(Active);
		else
		   gameObject.SetActive(Active);
	}
}

