using UnityEngine;
using System.Collections;

public class CheckEndPackScript : MonoBehaviour
{

    public UIButton ButtonNext;
	// Use this for initialization
	void Start () {
	    if (Managers.Game.CurrentLevelXmlInfo.LevelNumber == Globals.Constants.LevelsPerPack)
	    {
	        ButtonNext.isEnabled = false;
	    }
	    else
	    {
            ButtonNext.isEnabled = true;

	    }
	}
	
}
