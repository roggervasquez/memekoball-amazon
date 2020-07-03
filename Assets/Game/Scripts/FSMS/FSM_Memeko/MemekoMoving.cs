using UnityEngine;
using System.Collections;

public class MemekoMoving : FSMState {

    public MemekoBall CurrentMemeko;
    public float DelayForProcessing = 2F;

    private float elapsedTime;

    public override void Enter(FSMState lastState)
    {
      //  base.Enter(lastState);
        elapsedTime = 0F;

    }

    public override FSMEvent Process()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > DelayForProcessing)
        {

            if (CurrentMemeko.rigidbody.velocity.sqrMagnitude < Globals.GameValues.BallStopMagnitude)
            {
                CurrentMemeko.rigidbody.velocity = Vector3.zero;
                CurrentMemeko.rigidbody.angularVelocity = Vector3.zero;
                return new FSMEvent("Idle");
            }

        }
        return null;
    }

    public override void Leave(FSMState nextState)
    {
       // base.Leave(nextState);
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
