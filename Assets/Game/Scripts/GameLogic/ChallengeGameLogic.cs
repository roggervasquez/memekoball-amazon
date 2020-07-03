using System;
using UnityEngine;
using System.Collections;

public class ChallengeGameLogic : GameLogic
{

 
    private float endTime;
    private int timeLeft;
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
            ReplayControl.ActivateReplay();
        else
        {
            ReplayControl.DeActivateReplay();
        }
        Messenger.AddListener(Globals.GameEvents.MemekoLaunching, () => FSMGameLogic.RaiseEvent("Playing"));
        Messenger.AddListener(Globals.GameEvents.PauseGame, () => FSMGameLogic.RaiseEvent("Pause"));
        Messenger.AddListener(Globals.GameEvents.ResumeGame, OnResumeGame);
        Messenger.AddListener(Globals.GameEvents.RetryButtonPressed, OnExitScene);
        Messenger.AddListener(Globals.GameEvents.BackButtonPressed, OnExitScene);
      
       
       
	    CurrenTopScore = Managers.GameData.GetLevelTopScore(Managers.Game.Preferences.CurrentPackName,
                       Managers.Game.Preferences.CurrentLevelName, Managers.Game.Preferences.GameType);

        
        //Load the level from the pack-#.xml the base class has the implementation 
        base.LoadLevelObjects();
	    MAXTURNS = Managers.Game.CurrentLevelXmlInfo.Maxturns;
	    endTime = Time.time + Managers.Game.CurrentLevelXmlInfo.ChallengeTime+1;
	    timeLeft = Managers.Game.CurrentLevelXmlInfo.ChallengeTime+1;
         if (LabelTime != null)
             LabelTime.text = timeLeft.ToString();

         Messenger.AddListener(CurrentMemeko.gameObject.GetInstanceID() + "EnterSelected", () => FSMGameLogic.RaiseEvent("Selecting"));
         Messenger.AddListener(CurrentMemeko.gameObject.GetInstanceID() + "UnSelectPlayer", () => FSMGameLogic.RaiseEvent("ReadyToPlay"));
  
	}
    private void OnEnteringSelect()
    {
     /*   if (Managers.Game.Preferences.EnableReplay)
        {
            ReplayControl.StopRecording();

            ReplayControl.StartRecording();

            ShowRecordIndicator(true);
        } */

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

	    if (FSMGameLogic.CurrentState.StateName != Globals.Constants.WonState
	        && FSMGameLogic.CurrentState.StateName != Globals.Constants.AfterWonState
            && FSMGameLogic.CurrentState.StateName != Globals.Constants.StopRecordingState
            )
	    {
	        if (timeLeft > 0)
	            RefreshGameTime();
	    }
	}

    void RefreshGameTime()
    {
       timeLeft = (int)(endTime - Time.time);

      
        if (timeLeft <= 0)
        {
            FSMGameLogic.RaiseEvent("OutOfTime");
            timeLeft = 0;
            
        }
        if (LabelTime != null)
            LabelTime.text = timeLeft.ToString();
      
    
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
        if (getCurrentTurns() > 1)
        {
            score = score - ((getCurrentTurns()-1)*Globals.Constants.ScorePenaltyByTurn);
            
        }
        // Substract the amount of time elapsed
        score = score - (Managers.Game.CurrentLevelXmlInfo.ChallengeTime - timeLeft);
        
        // en caso extremo
        if (score < 0) return 100F;
        return score;
    }

    public override float getTopLevelScore()
    {
        return CurrenTopScore;
    }
}
