
using System;

using UnityEngine;
using System.Collections;
public class MinAfterWon : FSMState
{

    public MinHitGameLogic GameLogicScript;
    public float DelayBeforeProcessing = 1F;
    public AudioClip SoundWon;
    private float elapsedTime;
    public override void Enter(FSMState lastState)
    {
       // base.Enter(lastState);
        elapsedTime = 0F;
       
        
        GameLogicScript.CurrentScore = GameLogicScript.getCurrentScore();
      
        Managers.Audio.Play(SoundWon, GameLogicScript.CurrentMemeko.transform.position, 1F,1F,1F);


        Managers.GameCircleAmazon.UpdateWhisperScoresAndLeaderboards(Managers.Game.Preferences.CurrentPackName,
                                                                    Managers.Game.Preferences.CurrentLevelName,
                                                                    Managers.Game.Preferences.GameType,
                                                                    GameLogicScript.CurrentScore);


        
       
    }

    public override FSMEvent Process ()
	{


        elapsedTime += Time.deltaTime;
        if (elapsedTime > DelayBeforeProcessing)
        {
            return new FSMEvent("StopRecording");
        }
        return null;
	}

    public override void Leave(FSMState nextState)
    {
        //base.Leave(nextState);
    }

}
