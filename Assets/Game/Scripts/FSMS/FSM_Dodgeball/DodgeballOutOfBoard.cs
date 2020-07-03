using UnityEngine;
using System.Collections;

public class DodgeballOutOfBoard : FSMState {

    public DodgeBall CurrentDodgeball;
    public float DelayForProcessing = 2F;

    public override void Enter(FSMState lastState)
    {
        //base.Enter(lastState);
       
     

    }

    public override FSMEvent Process()
    {
        return new FSMEvent("Loading");
    }

    public override void Leave(FSMState nextState)
    {
        CurrentDodgeball.rigidbody.velocity = Vector3.zero;
        CurrentDodgeball.transform.position = CurrentDodgeball.SpawnPoint;
        //base.Leave(nextState);
    }
}
