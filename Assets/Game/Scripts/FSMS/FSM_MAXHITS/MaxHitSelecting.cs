
using UnityEngine;
using System.Collections;
public class MaxHitSelecting : FSMState{

    public MaxHitGameLogic GameLogicScript;

    public override void Enter(FSMState lastState)
    {
      
        //base.Enter(lastState);
    }

    public override FSMEvent Process ()
	{
		return null;	
	}

    public override void Leave(FSMState nextState)
    {
        GameLogicScript.CurrentMemeko.EnableInputLogic = false;
        //base.Leave(nextState);
    }

}
