using System;
using UnityEngine;
using System.Collections;

public class MinHitGameLogic : GameLogic
{
	private float timecount ;
    private float starttime;
    [HideInInspector] 
    public int MAXTURNS = 1;
    void Awake()
    {
        Time.timeScale = 1F;
        Messenger.Cleanup();
        Commands.Instance.CurrenGameLogic = this;
    }
	void Start ()
	{

       
        RecordIndicator.gameObject.SetActive(false);
	    if (Managers.Game.Preferences.EnableReplay)
	    {
	        ReplayControl.ActivateReplay();
         
	    }
	    else
	    {
	        ReplayControl.DeActivateReplay();
	    }

	    Messenger.AddListener(Globals.GameEvents.MemekoLaunching, () => FSMGameLogic.RaiseEvent("Playing"));
        Messenger.AddListener(Globals.GameEvents.PauseGame, () => FSMGameLogic.RaiseEvent("Pause"));
        Messenger.AddListener(Globals.GameEvents.ResumeGame, OnResumeGame);
        Messenger.AddListener(Globals.GameEvents.RetryButtonPressed, OnExitScene);
        Messenger.AddListener(Globals.GameEvents.BackButtonPressed, OnExitScene);
      
       
        starttime = Time.time;
	    globalSeconds = 0;
     
        CurrenTopScore = Managers.GameData.GetLevelTopScore(Managers.Game.Preferences.CurrentPackName,
                       Managers.Game.Preferences.CurrentLevelName, Managers.Game.Preferences.GameType);

        
        //Load the level from the pack-#.xml the base class has the implementation 
        base.LoadLevelObjects();
	    MAXTURNS = Managers.Game.CurrentLevelXmlInfo.Maxturns;
      //  Messenger.AddListener(CurrentMemeko.gameObject.GetInstanceID() + "EnterSelected", () => FSMGameLogic.RaiseEvent("Selecting"));
        Messenger.AddListener(CurrentMemeko.gameObject.GetInstanceID() + "EnterSelected", () => FSMGameLogic.RaiseEvent("Selecting"));
      
        Messenger.AddListener(CurrentMemeko.gameObject.GetInstanceID() + "UnSelectPlayer", () => FSMGameLogic.RaiseEvent("ReadyToPlay"));
  

	   
	}

    private void OnEnteringSelect()
    {
        //First Stopped

       /* if (Managers.Game.Preferences.EnableReplay)
        {
            ReplayControl.StopRecording();
           
            ReplayControl.StartRecording();

            ShowRecordIndicator(true);
        }*/
        FSMGameLogic.RaiseEvent("Selecting");
    }
    private void OnExitScene()
    {
       
        ReplayControl.StopRecording();
      
    }
    private void OnResumeGame()
    {
       FSMGameLogic.SendMessageToCurrentState("ResumeGame");
    }

    // Update is called once per frame
	void Update () {
      RefreshGameTime();

	 
	}

    void RefreshGameTime()
    {
        timecount = Time.time - starttime;
        globalSeconds = (int)timecount;
        if (LabelTime!=null)
           LabelTime.text = globalSeconds.ToString();

    }

    public override int getCurrentTurns()
    {
        if (CurrentMemeko != null)
            return CurrentMemeko.CurrentTurns;
        else
            return 0;

    }

    public override int getCurrentHits()
    {
        if (CurrentMemeko != null)
            return CurrentMemeko.CurrentHits;
        else
            return 0;
    }

    public override float getCurrentScore()
    {
        float score = Globals.Constants.MaxScore;
        //substract the penalty by the amount of hits
        score = score - getCurrentHits()*Globals.Constants.ScorePenaltyByHit;
        // substract the penalty by turns, but for the first turn there is no penalty
       /* 
         if (getCurrentTurns()>MAXTURNS)
              score = score - (getCurrentTurns() - MAXTURNS)*Globals.Constants.ScorePenaltyByTurn;
         Substract the amount of time elapsed
        if (globalSeconds > Globals.Constants.SecondsOfGrace)
        {
            score = score - (globalSeconds - Globals.Constants.SecondsOfGrace);
        }
        */
        // en caso extremo
        if (score < 0) return 100F;
        return score;
    }

    public override float getTopLevelScore()
    {
        return CurrenTopScore;
    }
}
