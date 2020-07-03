
using UnityEngine;
using System.Collections;
public class MaxHitPlaying : FSMState{

    public MaxHitGameLogic GameLogicScript;

    public override void Enter(FSMState lastState)
    {
        //base.Enter(lastState);
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
                else if (GameLogicScript.AllDodgeBallsStationary())
                {
                  //  if (GameLogicScript.getCurrentTurns() == GameLogicScript.MAXTURNS)
                   //     return new FSMEvent("Lost");

                    return new FSMEvent("ReadyToPlay");
                }
	        }
	    
        
        return null;	
	}

    public override void Leave(FSMState nextState)
    {
        //base.Leave(nextState);
    }

}
