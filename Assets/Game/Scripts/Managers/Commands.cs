using UnityEngine;
using System;

public enum CommandType
{
    Camera,  GameType, CurrentPack, CurrentLevel, CurrentMemekoTurns, 
    CurrentMemekoHits, CurrentLevelBestScore, CurrentLevelScore, CurrentInputMode , NextLevelName,
    SoundFX, ReplayLastRecorded, ReplayStatus, ShowEveryPlay,MinHelp,
     HelpPageToLoad , CurrentLeaderBoard, CurrentMaxTurns, CurrentEarnedAchievements,
    CurrentAchievementSelected
}

public class Commands : MonoBehaviour
{

    #region Singleton
    private static Commands instance;

  


    public static Commands Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject("Commands");
                var newInstance = obj.AddComponent<Commands>();
                instance = newInstance;
            }
            return instance;
        }
    }
    #endregion

    public GameLogic CurrenGameLogic; 

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
    }


    string ArgumentError(string msg)
    {

        Debug.LogWarning(msg);
        return "Error:" + msg;
    }

    public object Command(string command, string value)
    {
        try
        {
           
            CommandType cmd = (CommandType)Enum.Parse(typeof(CommandType), command, true);

            if (cmd == CommandType.Camera)
                return CameraCommand(value);
            else if (cmd == CommandType.GameType)
                return GameType(value);
            else if (cmd == CommandType.CurrentPack)
                return CurrentPack(value);
            else if (cmd == CommandType.CurrentLevel)
                return CurrentLevel(value);
            else if (cmd == CommandType.CurrentMemekoTurns)
                return CurrentMemekoTurns(value);
            else if (cmd == CommandType.CurrentMemekoHits)
                return CurrentMemekoHits(value);
            else if (cmd == CommandType.CurrentLevelBestScore)
                return CurrentLevelBestScore(value);
            else if (cmd == CommandType.CurrentLevelScore)
                return CurrentLevelScore(value);
            else if (cmd == CommandType.CurrentInputMode)
                return CurrentInputMode(value);
            else if (cmd == CommandType.NextLevelName)
                return LoadNextLevel(value);
            else if (cmd == CommandType.SoundFX)
                return SoundFX(value);
            else if (cmd == CommandType.ReplayLastRecorded)
                return ReplayLastRecorded(value);
            else if (cmd == CommandType.ReplayStatus)
                return ReplayStatus(value);
             else if (cmd == CommandType.ShowEveryPlay)
                return ShowEveryPlay(value);
            else if (cmd == CommandType.MinHelp)
                return MinHelp(value);
          
            else if (cmd == CommandType.HelpPageToLoad)
                return HelpPageToLoad(value);
            else if (cmd == CommandType.CurrentLeaderBoard)
                return CurrentLeaderBoard(value);
            else if (cmd == CommandType.CurrentMaxTurns)
                return CurrentMaxTurns(value);
            else if (cmd == CommandType.CurrentEarnedAchievements)
                return CurrentEarnedAchievements(value);
            else if (cmd == CommandType.CurrentAchievementSelected)
                return CurrentAchievementSelected(value);

            
        }
        catch
        {
            Debug.LogWarning(command + " not found");
            return "Command not found";
        }
        return "";
    }


    string CameraCommand(string value)
    {

        if (value == "" || value == null)
            return Managers.Game.Preferences.CameraType.ToString();
        try
        {
          
            CameraType argument = (CameraType)Enum.Parse(typeof(CameraType), value.ToUpper(), true);
            return (Managers.Game.Preferences.CameraType = argument).ToString();
        }
        catch
        {
            return value + " is not a valid camera argument";
        }
    }

 
    string GameType(string value)
    {
        if (value == "" || value == null)
        {
             //PARCHO solo para que Maxhits visualmente se vea MAXTURNS sin cambiar el enum
            // ya que en los scores ya se guardo como MAXHITS
            if (Managers.Game.Preferences.GameType == global::GameType.MAXHITS)
                return "MAXTURNS";

            return Managers.Game.Preferences.GameType.ToString();

        }
        try
        {
            
            GameType argument = (GameType)Enum.Parse(typeof(GameType), value.ToUpper(), true);
            return (Managers.Game.Preferences.GameType = argument).ToString();
        }
        catch
        {
            return value + " is not a valid camera argument";
        }

    }

    string CurrentPack(string value)
    {
        if (value == "" || value == null)
            return Managers.Game.Preferences.CurrentPackName;
        try
        {
            return (Managers.Game.Preferences.CurrentPackName = value);
        }
        catch
        {
            return value + " is not a valid pack argument";
        }

    }
    string CurrentAchievementSelected(string value)
    {
        if (value == "" || value == null)
            return Managers.Game.CurrentAchievementSelected;
        try
        {
            return (Managers.Game.CurrentAchievementSelected = value);
        }
        catch
        {
            return value + " is not a argument";
        }

    }
    string CurrentEarnedAchievements(string value)
    {
        if (value == "" || value == null)
            return Managers.Game.AchievementMessage;
        try
        {
            return (Managers.Game.AchievementMessage = value);
        }
        catch
        {
            return value + " is not a argument";
        }

    }
    string CurrentLeaderBoard(string value)
    {
        if (value == "" || value == null)
            return Managers.Game.Preferences.CurrentLeaderBoard;
        try
        {
            return (Managers.Game.Preferences.CurrentLeaderBoard = value);
        }
        catch
        {
            return value + " is not a valid  argument";
        }

    }


    string CurrentLevel(string value)
    {
        if (value == "" || value == null)
            return Managers.Game.Preferences.CurrentLevelName;
        try
        {
            return (Managers.Game.Preferences.CurrentLevelName = value);
        }
        catch
        {
            return value + " is not a valid level argument";
        }

    }

    string CurrentMemekoTurns(string value)
    {
        if (value == "" || value == null)
        {
            if (CurrenGameLogic == null)
                return "Game is not playing";
            return CurrenGameLogic.getCurrentTurns().ToString();
        }
        try
        {
           return (CurrenGameLogic.CurrentMemeko.CurrentTurns = Int32.Parse(value)).ToString();
        }
        catch
        {
            return value + " is not a valid int argument";
        }
    }

    string CurrentMaxTurns(string value)
    {
        if (value == "" || value == null)
        {
            if (CurrenGameLogic == null)
                return "Game is not playing";
            return Managers.Game.CurrentLevelXmlInfo.Maxturns.ToString();
        }
        try
        {
            return (Managers.Game.CurrentLevelXmlInfo.Maxturns = Int32.Parse(value)).ToString();
        }
        catch
        {
            return value + " is not a valid int argument";
        }
    }
    string CurrentMemekoHits(string value)
    {
        if (value == "" || value == null)
        {
            if (CurrenGameLogic == null)
                return "Game is not playing";
            return CurrenGameLogic.getCurrentHits().ToString();
        }
        try
        {
           return (CurrenGameLogic.CurrentMemeko.CurrentHits = Int32.Parse(value)).ToString();
        }
        catch
        {
            return value + " is not a valid int argument";
        }
    }


      string CurrentLevelBestScore(string value)
    {
        if (value == "" || value == null)
        {
            if (CurrenGameLogic == null)
                return "Game is not playing";
            return CurrenGameLogic.getTopLevelScore().ToString();
        }
        try
        {
           return (CurrenGameLogic.CurrenTopScore  = float.Parse(value)).ToString();
        }
        catch
        {
            return value + " is not a valid int argument";
        }
    }
      string CurrentLevelScore(string value)
      {
          if (value == "" || value == null)
          {
              if (CurrenGameLogic == null)
                  return "Game is not playing";
              return CurrenGameLogic.CurrentScore.ToString();
          }
          try
          {
              return (CurrenGameLogic.CurrentScore = float.Parse(value)).ToString();
          }
          catch
          {
              return value + " is not a valid int argument";
          }
      }
      string CurrentInputMode(string value)
      {

          if (value == "" || value == null)
              return Managers.Game.Preferences.CurrentInputMode.ToString();
          try
          {

              InputMode argument = (InputMode)Enum.Parse(typeof(InputMode), value.ToUpper(), true);
              Managers.Game.Preferences.CurrentInputMode = argument;
              Managers.Game.Preferences.SavePreferences();

              return Managers.Game.Preferences.CurrentInputMode.ToString();
          }
          catch
          {
              return value + " is not a valid InputMode argument";
          }
      }
      string LoadNextLevel(string value)
      {
          if (value == "" || value == null)
          {
              if (CurrenGameLogic == null)
                  return "Game is not playing";
              return Managers.Game.CurrentLevelXmlInfo.NextLevelName;
          }
          try
          {
             
              Managers.Game.Preferences.CurrentLevelName = Managers.Game.CurrentLevelXmlInfo.NextLevelName;
              
              return (Managers.Game.Preferences.CurrentLevelName);
          }
          catch
          {
              return value + " is not a valid int argument";
          }
      }
      string SoundFX(string value)
      {
          if (value == "" || value == null)
              return Managers.Game.Preferences.EnableSound ? "ON" : "OFF";
          try
          {
              if (value.ToUpper() == "ON")
              {
                  Managers.Game.Preferences.EnableSound = true;
                  NGUITools.soundVolume = 1F;
              }
              else
              {
                  Managers.Game.Preferences.EnableSound = false;
                  NGUITools.soundVolume = 0F;
              }
              Managers.Game.Preferences.SavePreferences();

              return Managers.Game.Preferences.EnableSound.ToString();
          }
          catch
          {
              return value + " is not a valid Sound argument";
          }
      }
      string ReplayStatus(string value)
      {
          if (value == "" || value == null)
              return Managers.Game.Preferences.EnableReplay ? "ON" : "OFF";
          try
          {
            

              if (value.ToUpper() == "ON")
              {
                  if (CurrenGameLogic != null)
                      CurrenGameLogic.ReplayControl.ActivateReplay();
                  
                  Managers.Game.Preferences.EnableReplay = true;
              }
              else
              {
                  if (CurrenGameLogic != null)
                       CurrenGameLogic.ReplayControl.DeActivateReplay();
                  Managers.Game.Preferences.EnableReplay = false;
              }
              Managers.Game.Preferences.SavePreferences();
              return Managers.Game.Preferences.EnableReplay.ToString();
          }
          catch
          {
              return value + " is not a valid Replay status argument";
          }
      }

    private string ReplayLastRecorded(string value)
    {
        if (value == "" || value == null)
            if (CurrenGameLogic == null)
                return "Game is not playing";
            else
            {
                return CurrenGameLogic.ReplayControl.ThumbnailPath;
            }

        try
        {
            if (CurrenGameLogic == null)
                return "Game is not playing";
            else
            {
                CurrenGameLogic.ReplayControl.ReplaySavedRecording();
                return "PLAYED";

            }
        }
        catch
        {
            return value + " is not a valid Sound argument";
        }

    }
    string ShowEveryPlay(string value)
    {
        if (value == "" || value == null)
            return  "Not Showing";
        try
        {
            if (value.ToUpper() == "Show")
            {
                Everyplay.SharedInstance.Show();
                return "Showing";

            }
            else
            {
                return "Invalid Argument";
            }
        }
        catch
        {
            return value + " is not a argument";
        }
    }

    string MinHelp(string value)
    {
        if (value == "" || value == null)
            return Managers.Game.Preferences.MinHelp ? "ON" : "OFF";
        try
        {
            if (value.ToUpper() == "ON")
                Managers.Game.Preferences.MinHelp = true;
            else
                Managers.Game.Preferences.MinHelp = false;

            Managers.Game.Preferences.SavePreferences();

            return Managers.Game.Preferences.MinHelp.ToString();
        }
        catch
        {
            return value + " is not a valid  argument";
        }
    }
   
   
    string HelpPageToLoad(string value)
    {
        if (value == "" || value == null)
            return Managers.Game.HelpPageToLoad.ToString();
        try
        {
            int page = System.Convert.ToInt32(value);
            return (Managers.Game.HelpPageToLoad= page).ToString();
        }
        catch
        {
            return value + " is not a valid page index argument";
        }

    }
}
