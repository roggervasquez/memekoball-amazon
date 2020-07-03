
using UnityEngine;
using System.Collections;
public class MinHitPause : FSMState{

    public MinHitGameLogic GameLogicScript;

    private FSMState LastState;
    private GameType ActualGameType;
    public override void Enter(FSMState lastState)
    {
        LastState = lastState;
        ActualGameType = Managers.Game.Preferences.GameType;

        GameLogicScript.ReplayControl.PauseRecording();
     
        GameLogicScript.DisableTopBarButtons();
        GameLogicScript.CurrentMemeko.EnableInputLogic = false;
        // base.Enter(lastState);
    }

    public override FSMEvent Process ()
	{
		return null;	
	}

    public override void Leave(FSMState nextState)
    {
        //base.Leave(nextState);
        GameLogicScript.ReplayControl.ResumeRecording();
        GameLogicScript.EnableTopBarButtons();
        GameLogicScript.CurrentMemeko.EnableInputLogic = true;
    }

    public void ResumeGame()
    {
        GameLogicScript.ShowRecordIndicator(Managers.Game.Preferences.EnableReplay);

        // Si no cambio modo de Game
        if (ActualGameType==Managers.Game.Preferences.GameType)
            GameLogicScript.FSMGameLogic.RaiseEvent(LastState.StateName);
        else
        {   
            //Reload the scene, calling loading transition scene
            Application.LoadLevel("LoadingTransitionScene");
        }
    }

}
