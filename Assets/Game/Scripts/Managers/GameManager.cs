using System;
using UnityEngine;
using System.Collections;
using System.IO;





public  class GameManager : MonoBehaviour {
	
	
	
	public Preferences Preferences = new Preferences();
	
	public LevelInfo CurrentLevelXmlInfo = new LevelInfo();
	
	
    public bool AreLevelFilesEncrypted=false;
    public bool EncryptedLocalScores = true;

	public string NextSceneToLoad="";
    public bool CheckHelpFirstTime = true;
	public bool TouchInterfaceDetected = true;
    public int HelpPageToLoad = 0;
    public string AchievementMessage = "";
    public string CurrentAchievementSelected = "";
	
	
	void Start()
	{
     
	  Time.timeScale = 1.0f;
       Preferences.LoadPreferences();
	   TouchInterfaceDetected = Managers.Platform.IsTouchSupported();
     
	}

   
	
	
}
	