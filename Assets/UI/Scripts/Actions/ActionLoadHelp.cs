using UnityEngine;
using System.Collections;

public class ActionLoadHelp : Action
{

	public string SceneName;
	public string NextScene;
    public bool CheckFirsTime = true;
	
	public override void ActionPerformed ()
	{
		if(SceneName==null ||SceneName=="")
			Debug.LogError("SceneName is not specified");

        if (NextScene == null || NextScene == "")
		     	Debug.LogError("Next SceneName is not specified");

	        Managers.Game.CheckHelpFirstTime = CheckFirsTime;
		   	Managers.Game.NextSceneToLoad=NextScene;

            Application.LoadLevel(SceneName);
     	
	}
	
}
