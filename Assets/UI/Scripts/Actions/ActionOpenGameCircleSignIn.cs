using UnityEngine;
using System.Collections;

public class ActionOpenGameCircleSignIn : Action
{

	
	
	public override void ActionPerformed ()
	{
        Managers.GameCircleAmazon.ShowSignInPage();
	}
}
