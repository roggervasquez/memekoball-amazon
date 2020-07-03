using UnityEngine;
using System.Collections;

public class ActionOpenGameCircleLeaderBoards : Action
{

	
	
	public override void ActionPerformed ()
	{
        Managers.GameCircleAmazon.ShowLeaderBoardPage();
	}
}
