
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class ChallengeHitStopRecording : FSMState
{

    public ChallengeGameLogic GameLogicScript;
    public float DelayBeforeProcessing = 1F;
    private float elapsedTime;
    public override void Enter(FSMState lastState)
    {
       // base.Enter(lastState);
        // base.Enter(lastState);
       // Time.timeScale = 0F;
        GameLogicScript.ReplayControl.StopRecording();
      
        GameLogicScript.ShowRecordIndicator(false);

        GameLogicScript.ReplayControl.SetMetaData("Pack", Managers.Game.Preferences.CurrentPackName);
        GameLogicScript.ReplayControl.SetMetaData("Level", Managers.Game.Preferences.CurrentLevelName);
        GameLogicScript.ReplayControl.SetMetaData("Mode", Managers.Game.Preferences.GameType.ToString());
        GameLogicScript.ReplayControl.SetMetaData("Score", GameLogicScript.CurrentScore);

        WindowLoader.Show("WinGameWindow", "Anchor(Center)");
       
        if (GameLogicScript.CurrentScore>GameLogicScript.CurrenTopScore)
            Managers.GameCircleAmazon.UpdateAchievements();
    }

    public override FSMEvent Process ()
	{
       
        return null;
	}

    public override void Leave(FSMState nextState)
    {
        //base.Leave(nextState);
    }

}
