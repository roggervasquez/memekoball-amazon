using UnityEngine;
using System.Collections;
using System;


[Serializable]
public class Preferences
{


  
    public RuntimePlatform RuntimePlatform;
    public GameType GameType;
    public bool EnableMusic;
    public bool EnableSound;
    public bool EnableFx;
    public bool EnableReplay;
    public string CurrentPackName;
    public CameraType CameraType;
    public string CurrentLevelName;
    public string CurrentPlayerName;
    public string CurrentLeaderBoard;
    public bool MinHelp;
    public bool MaxHelp;
    public bool TimeHelp;
    

    public float MusicVolume;
    public float SoundVolume;
    public InputMode CurrentInputMode;


    public Preferences()
    {
      
    }

    public void LoadPreferences()
    {
        GameType = GameType.MINHITS;
        EnableFx = true;
        CameraType = CameraType.FIXED;
        CurrentPackName = "PACK-1";
        CurrentLevelName = "LEVEL-1";
        CurrentPlayerName = "MyPlayer";
        CurrentLeaderBoard = Globals.Constants.GlobalLeaderBoard;
        MusicVolume = 1F;
        SoundVolume = 1F;
        EnableMusic = true;
        //Sound
        if (PlayerPrefs.HasKey("EnableSound"))
        {
            if (PlayerPrefs.GetString("EnableSound") == "ON")
                EnableSound = true;
            else
                EnableSound = false;
        }
        else
            EnableSound = true;
        // Replay
        if (PlayerPrefs.HasKey("EnableReplay"))
        {
            if (PlayerPrefs.GetString("EnableReplay") == "ON")
                EnableReplay = true;
            else
                EnableReplay = false;
        }
        else
            EnableReplay = true;

         // Input mode
        if (PlayerPrefs.HasKey("CurrentInputMode"))
        {
            CurrentInputMode = (InputMode)Enum.Parse(typeof(InputMode),PlayerPrefs.GetString("CurrentInputMode").ToUpper(), true);
        }
        else
        {
            CurrentInputMode = InputMode.FORWARD;
        }

        // Minhelp
        if (PlayerPrefs.HasKey("MinHelp"))
        {
            if (PlayerPrefs.GetString("MinHelp") == "ON")
                MinHelp = true;
            else
                MinHelp = false;
        }
        else
            MinHelp = true;
        if (PlayerPrefs.HasKey("MaxHelp"))
        {
            if (PlayerPrefs.GetString("MaxHelp") == "ON")
                MaxHelp = true;
            else
                MaxHelp = false;
        }
        else
            MaxHelp = true;

        if (PlayerPrefs.HasKey("TimeHelp"))
        {
            if (PlayerPrefs.GetString("TimeHelp") == "ON")
                TimeHelp = true;
            else
                TimeHelp = false;
        }
        else
            TimeHelp = true;

    

    }

    public void SavePreferences()
    {
        if (EnableSound)
            PlayerPrefs.SetString("EnableSound","ON");
        else
            PlayerPrefs.SetString("EnableSound", "OFF");

        if (EnableReplay)
            PlayerPrefs.SetString("EnableReplay", "ON");
        else
            PlayerPrefs.SetString("EnableReplay", "OFF");

        if (MinHelp)
            PlayerPrefs.SetString("MinHelp", "ON");
        else
            PlayerPrefs.SetString("MinHelp", "OFF");


        if (MaxHelp)
            PlayerPrefs.SetString("MaxHelp", "ON");
        else
            PlayerPrefs.SetString("MaxHelp", "OFF");

        if (TimeHelp)
            PlayerPrefs.SetString("TimeHelp", "ON");
        else
            PlayerPrefs.SetString("TimeHelp", "OFF");

        PlayerPrefs.SetString("CurrentInputMode", CurrentInputMode.ToString());
        PlayerPrefs.Save();
    }

}
