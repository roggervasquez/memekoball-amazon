
using UnityEngine;
using System.Collections;
public class ChallengeHitLost : FSMState{

    public ChallengeGameLogic GameLogicScript;
    public float DelayBeforeProcessing = 1F;
    private float elapsedTime;

    public override void Enter(FSMState lastState)
    {
       // base.Enter(lastState);
        elapsedTime = 0F;
        elapsedTime = 0F;
        GameLogicScript.ReplayControl.StopRecording();
        GameLogicScript.ShowRecordIndicator(false);
        GameLogicScript.DisableTopBarButtons();

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
