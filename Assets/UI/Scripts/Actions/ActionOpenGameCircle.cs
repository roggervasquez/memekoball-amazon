using UnityEngine;
using System.Collections;

public class ActionOpenGameCircle : Action
{

	
	
	public override void ActionPerformed ()
	{
        Managers.GameCircleAmazon.ShowGameCircle();
	}
}
