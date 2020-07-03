using UnityEngine;
using System.Collections;

public class MemekoLaunching : FSMState {

    public MemekoBall CurrentMemeko;
    public float DelayForProcessing = 2F;
    public AudioClip LaunchingSound;
    public float SoundVolume = 1F;

    private float elapsedTime;
    public override void Enter(FSMState lastState)
    {
        //base.Enter(lastState);
        Messenger.Broadcast(Globals.GameEvents.MemekoLaunching);
        CurrentMemeko.Launch();
        Managers.Audio.Play(LaunchingSound, CurrentMemeko.transform.position, SoundVolume);
        CurrentMemeko.CurrentTurns++;
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
        //base.Leave(nextState);
    }
    public void OutOfTheBoard()
    {
        CurrentMemeko.FSMCharacter.RaiseEvent("OutOfBoard");
    }

    public void OnTheHole()
    {
        CurrentMemeko.FSMCharacter.RaiseEvent("OnTheHole");

    }
}
