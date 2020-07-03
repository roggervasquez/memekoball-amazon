
using UnityEngine;
using System.Collections;
public class MaxHitWon : FSMState{

    public MaxHitGameLogic GameLogicScript;
    public float DelayBeforeProcessing = 1F;
    private float elapsedTime;
    public override void Enter(FSMState lastState)
    {
       // base.Enter(lastState);
        elapsedTime = 0F;
      
       
        GameLogicScript.DisableTopBarButtons();
       
    }

    public override FSMEvent Process ()
	{
        elapsedTime += Time.deltaTime;
        if (elapsedTime > DelayBeforeProcessing)
        {
            return new FSMEvent("AfterWon");
        }
        return null;
	}

    public override void Leave(FSMState nextState)
    {
        //base.Leave(nextState);
    }

}
