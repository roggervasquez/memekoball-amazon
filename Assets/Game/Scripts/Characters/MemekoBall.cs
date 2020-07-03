using UnityEngine;
using System.Collections;

public class MemekoBall : MonoBehaviour
{


    public bool EnableInputLogic;
    public float Power = 40F;
    public FSM FSMCharacter;
    [HideInInspector]
    public int CurrentHits;
    [HideInInspector]
    public int CurrentTurns;

    [HideInInspector]
    public AttackDirectionInfo AttackDirection;

	void Start ()
	{
	    CurrentHits = 0;
	    CurrentTurns = 0;
	}

    public  void Launch()
    {
        var forceVector = GetDirectionOfTheAttack(Power);
        //transform.rigidbody.AddForce(forceVector, ForceMode.Impulse);
      
        Vector3 myPosition =  new Vector3(transform.position.x - forceVector.normalized.x,
                                          transform.position.y,
                                          transform.position.z - forceVector.normalized.z);

        transform.rigidbody.AddForceAtPosition(forceVector,myPosition, ForceMode.Impulse);
    }

    public Vector3 GetDirectionOfTheAttack(float power)
    {
        Vector3 dir = AttackDirection.EndPos - AttackDirection.InitPos;
        Vector3 posFinal;
         if(Managers.Game.Preferences.CurrentInputMode==InputMode.FORWARD)
             posFinal = transform.position + dir.normalized * AttackDirection.ArrowDistance;
         else
             posFinal = transform.position - dir.normalized * AttackDirection.ArrowDistance;
          
        posFinal[1] = transform.position.y;

        dir = posFinal - transform.position;
        float forceToApply = (AttackDirection.ArrowDistance * power) / Globals.GameValues.BallMaxDragDistance;

        var forceVector = dir.normalized * forceToApply;


        return forceVector;
    }

    void OnCollisionEnter(Collision col)
    {
       if (col.collider.gameObject.tag == Globals.Tags.Wall
           ||
           col.collider.gameObject.tag == Globals.Tags.Obstacle
           )
       {
           this.CurrentHits++;
       }
        
    }

    void OnTriggerEnter(Collider other)
    {
        //print(other.tag);
        if (other.tag == Globals.Tags.DangerZone) // Out of the Board
            FSMCharacter.SendMessageToCurrentState("OutOfBoard");
        else if (other.tag == Globals.Tags.WinZone) // enter the Hole
            FSMCharacter.SendMessageToCurrentState("OnTheHole");
    }
}
