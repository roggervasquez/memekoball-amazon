using UnityEngine;
using System.Collections;

public class DodgeballLoading : FSMState
{

    public DodgeBall CurrentDodgeball;
    public float TimeLoading = 2F;


    private float elapsedTime ;
    public override void Enter(FSMState lastState)
    {
       // base.Enter(lastState);
        elapsedTime = 0F;
    }

    public override FSMEvent Process()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > TimeLoading)
            return new FSMEvent("Idle");
        return null;
    }

    public override void Leave(FSMState nextState)
    {
        // base.Leave(nextState);
    }
}
