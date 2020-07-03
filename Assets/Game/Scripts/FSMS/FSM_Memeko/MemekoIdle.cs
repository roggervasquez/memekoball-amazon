using UnityEngine;
using System.Collections;

public class MemekoIdle : FSMState {

    public MemekoBall CurrentMemeko;
    public float DelayForProcessing = 2F;

    private float elapsedTime;
    public override void Enter(FSMState lastState)
    {
       // base.Enter(lastState);
        elapsedTime = 0F;
        Messenger.Broadcast(CurrentMemeko.gameObject.GetInstanceID() + "EnterIdle");
    }

    public override FSMEvent Process()
    {

        elapsedTime += Time.deltaTime;
        if (elapsedTime > DelayForProcessing)
        {
            // Someone hitme and moved me
            if (CurrentMemeko.rigidbody.velocity != Vector3.zero )
                return new FSMEvent("Moving");
         }
        return null;
    }

    public override void Leave(FSMState nextState)
    {
        //base.Leave(nextState);
    }

    public void SelectPlayer()
    {
       
        CurrentMemeko.FSMCharacter.RaiseEvent("Selecting");

    }
    public void OutOfBoard()
    {
        CurrentMemeko.FSMCharacter.RaiseEvent("OutOfBoard");
    }

    public void OnTheHole()
    {
        CurrentMemeko.FSMCharacter.RaiseEvent("OnTheHole");

    }
}

