using UnityEngine;
using System.Collections;

public class MemekoSelecting : FSMState {

    public MemekoBall CurrentMemeko;
    public float DelayForProcessing = 2F;
    public GameObject SelectionEffectPrefab;
    public Vector3 OffSetPos;

    private GameObject EffectGameObject;
    public override void Enter(FSMState lastState)
    {
        //base.Enter(lastState);
        EffectGameObject= Managers.Effects.PlayConstantEffect(SelectionEffectPrefab, CurrentMemeko.transform.position + OffSetPos);
        Messenger.Broadcast(CurrentMemeko.gameObject.GetInstanceID() + "EnterSelected");
    }

    public override FSMEvent Process()
    {
        return null;
    }

    public override void Leave(FSMState nextState)
    {
        //base.Leave(nextState);
        if (EffectGameObject != null)
        {
            var ps = EffectGameObject.GetComponent<ParticleSystem>();
            ps.Stop();
            Destroy(EffectGameObject);
        }
        Messenger.Broadcast(CurrentMemeko.gameObject.GetInstanceID() + "LeaveSelected");
    }

    public void UnSelectPlayer()
    {
        Messenger.Broadcast(CurrentMemeko.gameObject.GetInstanceID() + "UnSelectPlayer");
        CurrentMemeko.FSMCharacter.RaiseEvent("Idle");
    }
    public void LaunchBall(AttackDirectionInfo attackDirection)
    {

        CurrentMemeko.AttackDirection = attackDirection;
        CurrentMemeko.FSMCharacter.RaiseEvent("Launching");

    }
}
