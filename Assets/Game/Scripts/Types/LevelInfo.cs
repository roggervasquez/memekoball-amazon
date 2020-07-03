using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class LevelInfo  {

	public string CurrentLevelName;
	public string NextLevelName;
	public string PackName;
	public int LevelNumber;
	public int ChallengeTime;
    public float CameraSize;
    public float MemekoForce;
    public int Maxturns;
	
	public LevelInfo()
	{
	  
		CurrentLevelName=String.Empty;
		NextLevelName = String.Empty;
	    PackName = String.Empty;
	    LevelNumber = 0;
	    ChallengeTime = 0;
	    CameraSize = 9.3F;
	    MemekoForce = 40F;
	    Maxturns = 1;
	}
}
