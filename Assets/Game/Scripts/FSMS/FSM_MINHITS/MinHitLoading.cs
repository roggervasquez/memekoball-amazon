
using UnityEngine;
using System.Collections;
public class MinHitLoading : FSMState
{

    public MinHitGameLogic GameLogicScript;
    public float TimeLoading = 1F;
    private float elapsedTime;

    public override void Enter(FSMState lastState)
    {
       // base.Enter(lastState);
        GameLogicScript.DisableTopBarButtons();


       
    }

    public override FSMEvent Process ()
	{
        elapsedTime += Time.deltaTime;
        if (elapsedTime > TimeLoading)
            return new FSMEvent("ReadyToPlay");
        return null;
	}

    public override void Leave(FSMState nextState)
    {
        // base.Leave(nextState);
        GameLogicScript.EnableTopBarButtons();
        if (Managers.Game.Preferences.EnableReplay)
        {
            GameLogicScript.ShowRecordIndicator(true);
            GameLogicScript.ReplayControl.StartRecording();
        }
    }

}
