using UnityEngine;
using System.Collections;

public class EveryPlaySupported : MonoBehaviour
{

    public UIButton botonEveryPlay;
    public bool CheckPreferences=false;
	// Use this for initialization
	void Start () {
	    if (Everyplay.SharedInstance != null)
	    {

	        if (Everyplay.SharedInstance.IsRecordingSupported())
	        {
	            if (CheckPreferences)
	            {
                      if (Managers.Game.Preferences.EnableReplay)
                          botonEveryPlay.isEnabled = true;  
                      else
                          botonEveryPlay.isEnabled = false;  
	            }
	            else
	            {
                    botonEveryPlay.isEnabled = true;  
	            }
	            
	        }
	        else
	        {
	            if (CheckPreferences)
	            {
	                botonEveryPlay.isEnabled = true;
	                NGUITools.SetActive(botonEveryPlay.gameObject, false);
	            }

	        }
	    }
	    else
	    {
	         botonEveryPlay.isEnabled = false;
	          NGUITools.SetActive(botonEveryPlay.gameObject, false);
	      
	    }
	}
	
	
}
