using UnityEngine;
using System.Collections;

public class ActionOpenGameCircleAchievements : Action
{

	
	
	public override void ActionPerformed ()
	{
        Managers.GameCircleAmazon.ShowAchievementsPage();
	}
}
