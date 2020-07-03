using System;
using UnityEngine;
using System.Collections;

public class LevelButton : MonoBehaviour
{

    public int LevelNumber;
    public UILabel BestLevelScore;

	void Start ()
	{
	    try
	    {
            int score = Managers.GameData.GetLevelTopScore(Managers.Game.Preferences.CurrentPackName,
                                                        LevelNumber, Managers.Game.Preferences.GameType);

            BestLevelScore.text = System.Convert.ToInt32(score).ToString();
	
	    }
	    catch (Exception ex)
	    {
	        Debug.Log(ex.Message);
	    }
	 }
	
	// Update is called once per frame
	void Update () {
	
	}
}
