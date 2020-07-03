
using UnityEngine;
using System.Collections;
public class MaxHitReadyToPlay : FSMState{


    public MaxHitGameLogic GameLogicScript;


    public override void Enter(FSMState lastState)
    {
        //base.Enter(lastState);
       // GameLogicScript.ReplayControl.StopRecording();
       // GameLogicScript.ShowRecordIndicator(false);
        GameLogicScript.CurrentMemeko.EnableInputLogic = true;
    }

    public override FSMEvent Process ()
	{
      

        if (GameLogicScript.CurrentMemeko.FSMCharacter.CurrentState.StateName == Globals.MemekoStates.OnTheHole)
        {
            return new FSMEvent("Lost");
        }
        else
            if (GameLogicScript.CurrentMemeko.FSMCharacter.CurrentState.StateName == Globals.MemekoStates.Idle)
            {
                // Check the status of the dodgeballs
                if (GameLogicScript.AllDodgeBallsOntheHole())
                    return new FSMEvent("Won");
        
            }
        if (GameLogicScript.getCurrentTurns() == GameLogicScript.MAXTURNS)
            return new FSMEvent("Lost");
		return null;	
	}

    public override void Leave(FSMState nextState)
    {
        //base.Leave(nextState);
      
      
    }

}
