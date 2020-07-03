
using UnityEngine;
using System.Collections;
public class MinHitSelecting : FSMState{

    public MinHitGameLogic GameLogicScript;

    public override void Enter(FSMState lastState)
    {
      
       
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
