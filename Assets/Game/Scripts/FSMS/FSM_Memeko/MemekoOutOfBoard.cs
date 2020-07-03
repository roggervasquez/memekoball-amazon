using UnityEngine;
using System.Collections;

public class MemekoOutOfBoard : FSMState {

    public MemekoBall CurrentMemeko;
    public float DelayForProcessing = 2F;

    public override void Enter(FSMState lastState)
    {
        //base.Enter(lastState);
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
