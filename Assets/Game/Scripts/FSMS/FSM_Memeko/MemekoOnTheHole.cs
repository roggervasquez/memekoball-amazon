using UnityEngine;
using System.Collections;

public class MemekoOnTheHole : FSMState {

    public MemekoBall CurrentMemeko;
    public float DelayForProcessing = 2F;

    public override void Enter(FSMState lastState)
    {
        //base.Enter(lastState);
        print("Memeko in the Hole");
    }

    public override FSMEvent Process()
    {
        return null;
    }

    public override void Leave(FSMState nextState)
    {
        //base.Leave(nextState);
    }
}
