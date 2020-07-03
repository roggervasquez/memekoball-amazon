using UnityEngine;
using System.Collections;

public class ActionLoadScene : Action {

	public string SceneName;
	public string LoadingSceneName;
	public bool UseLoadingScene=false;
    public bool UseNextSceneGameManager = false;
	public override void ActionPerformed ()
	{
        if (UseNextSceneGameManager)
	    {
	        SceneName = Managers.Game.NextSceneToLoad;
	    }
		if(SceneName==null ||SceneName=="")
			Debug.LogError("SceneName is not specified");
		
		if (UseLoadingScene)
		{
			if(LoadingSceneName==null ||LoadingSceneName=="")
		     	Debug.LogError("Loading SceneName is not specified");
		
		   	Managers.Game.NextSceneToLoad=SceneName;
			Debug.Log(LoadingSceneName);
			Application.LoadLevel(LoadingSceneName);
     	}
		else
		{	
		    Application.LoadLevel(SceneName);
		} 
	}
	
}
