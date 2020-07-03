using UnityEngine;
using System.Collections;

public class InitActionLoadLevel : InitAction {
	
	public string SceneName;
	public string LoadingSceneName;
	public bool UseLoadingScene=false;
	
	
	public override void ActionPerformed ()
	{
		if(SceneName==null ||SceneName=="")
			Debug.LogError("SceneName is not specified");
		
		if (UseLoadingScene)
		{
			if(LoadingSceneName==null ||LoadingSceneName=="")
		     	Debug.LogError("Loading SceneName is not specified");
		
		   	Managers.Game.NextSceneToLoad=SceneName;
			Application.LoadLevel(LoadingSceneName);
     	}
		else
		{	
		    Application.LoadLevel(SceneName);
		} 
	}
	
}
