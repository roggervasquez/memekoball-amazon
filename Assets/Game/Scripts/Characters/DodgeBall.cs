using UnityEngine;
using System.Collections;

public class DodgeBall : MonoBehaviour
{

    public  FSM FSMCharacter;

    public Vector3 SpawnPoint;
    [HideInInspector]
    public int HitsReceived;
    
	// Use this for initialization
	void Start ()
	{
	    HitsReceived = 0;
	    SpawnPoint = transform.position;
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.tag == Globals.Tags.Player)
        {
            this.HitsReceived++;
        }

    }
    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == Globals.Tags.DangerZone) // Out of the Board
            FSMCharacter.SendMessageToCurrentState("OutOfBoard");
        else if (other.tag == Globals.Tags.WinZone) // enter the Hole
            FSMCharacter.SendMessageToCurrentState("OnTheHole");
    }
}
