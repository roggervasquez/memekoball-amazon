
using UnityEngine;
using System.Collections;
public class ChallengeHitOutOfTime : FSMState
{

    public ChallengeGameLogic GameLogicScript;
    public float DelayBeforeProcessing = 1F;

    public AudioClip SoundOutOfTime;
    private float elapsedTime;

    public override void Enter(FSMState lastState)
    {
       // base.Enter(lastState);
        elapsedTime = 0F;
        Managers.Audio.Play(SoundOutOfTime, GameLogicScript.CurrentMemeko.transform.position, 1F);
        GameLogicScript.DisableTopBarButtons();
        GameLogicScript.ReplayControl.StopRecording();
        GameLogicScript.ShowRecordIndicator(false);
        
    }

    public override FSMEvent Process ()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > DelayBeforeProcessing)
        {
            return new FSMEvent("AfterLost");
        }
        return null;
	}

    public override void Leave(FSMState nextState)
    {
        //base.Leave(nextState);
    }

}
