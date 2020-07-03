using UnityEngine;
using System.Collections;

public class DodgeballMoving : FSMState
{

    public DodgeBall CurrentDodgeball;
    public float DelayForProcessing = 1F;

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

            if (CurrentDodgeball.rigidbody.velocity.sqrMagnitude < Globals.GameValues.BallStopMagnitude)
            {
                CurrentDodgeball.rigidbody.velocity = Vector3.zero;
                CurrentDodgeball.rigidbody.angularVelocity = Vector3.zero;
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
        CurrentDodgeball.FSMCharacter.RaiseEvent("OutOfBoard");
    }

    public void OnTheHole()
    {
        CurrentDodgeball.FSMCharacter.RaiseEvent("OnTheHole");

    }
}
