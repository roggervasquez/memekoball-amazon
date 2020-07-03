using UnityEngine;
using System;
using System.Collections;


public class Managers : MonoBehaviour
{
	private static GameManager gameManager;
	public static GameManager Game
	{
		get { Validate();return gameManager; }
	}
	
	private static AudioManager audioManager;
	public static AudioManager Audio
	{
		get {Validate(); return audioManager; }
	}
	
	private static EffectsManager effectsManager;
	public static EffectsManager Effects
	{
		get { Validate();return effectsManager; }
	}
	
	private static DataManager dataManager;
	public static DataManager Data
	{
		get {Validate(); return dataManager; }
	}

    private static PlatformManager platformManager;
    public static PlatformManager Platform
    {
        get { Validate(); return platformManager; }
    }

    private static GameDataManager gameDataManager;
    public static GameDataManager GameData
    {
        get { Validate(); return gameDataManager; }
    }

    private static AchievementManager achievementManager;
    public static AchievementManager Achievements
    {
        get { Validate(); return achievementManager; }
    }
    
    private static AmazonCloudManager amazonCloudManager;
    public static AmazonCloudManager GameCircleAmazon
    {
        get  { Validate(); return amazonCloudManager; } 
    }

	static void Validate()
	{
		if(gameManager==null || audioManager ==null  || effectsManager==null
               || dataManager == null || platformManager == null || gameDataManager == null
               || achievementManager ==null)
		{
		      Application.LoadLevel("Xtudio16StartScene");
		    
		}
	}
	
	
	void Start ()
	{
	    
		
		//Find the references
		gameManager = GetComponent<GameManager>();
		audioManager = GetComponent<AudioManager>();
		dataManager = GetComponent<DataManager>();
		effectsManager = GetComponent<EffectsManager>();
	    platformManager = GetComponent<PlatformManager>();
	    gameDataManager = GetComponent<GameDataManager>();
	    achievementManager = GetComponent<AchievementManager>();
	    amazonCloudManager = GetComponent<AmazonCloudManager>();
		DontDestroyOnLoad(this);
	
		
    }





}