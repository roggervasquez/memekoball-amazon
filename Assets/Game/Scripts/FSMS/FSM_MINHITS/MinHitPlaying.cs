
using UnityEngine;
using System.Collections;
public class MinHitPlaying : FSMState{

    public MinHitGameLogic GameLogicScript;
    public float DelayBeforeProcessing = 1F;
  
    private float elapsedTime;
    public override void Enter(FSMState lastState)
    {
        //base.Enter(lastState);
        elapsedTime = 0F;
    }

    public override FSMEvent Process ()
	{
        elapsedTime += Time.deltaTime;
        if (elapsedTime > DelayBeforeProcessing)
        {
          
            if (GameLogicScript.CurrentMemeko.FSMCharacter.CurrentState.StateName == Globals.MemekoStates.OnTheHole)
            {
                return new FSMEvent("Lost");
            }
            else if (GameLogicScript.CurrentMemeko.FSMCharacter.CurrentState.StateName == Globals.MemekoStates.Idle)
            {
                // Check the status of the dodgeballs
                if (GameLogicScript.AllDodgeBallsOntheHole())
                    return new FSMEvent("Won");
                else if (GameLogicScript.AllDodgeBallsStationary()
                    && GameLogicScript.CurrentMemeko.FSMCharacter.CurrentState.StateName == Globals.MemekoStates.Idle)
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
