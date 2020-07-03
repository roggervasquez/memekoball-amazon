using UnityEngine;
using System.Collections;

public class ChallengeAfterLost : FSMState
{
    public ChallengeGameLogic GameLogicScript;
    public float DelayBeforeProcessing = 1F;
    public AudioClip SoundLost;
    private float elapsedTime;
    public override void Enter(FSMState lastState)
    {
        // base.Enter(lastState);
        Time.timeScale = 0F;

        Managers.Audio.Play(SoundLost, GameLogicScript.CurrentMemeko.transform.position, 1F,1F,1F);
        WindowLoader.Show("LoseGameWindow", "Anchor(Center)");
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
