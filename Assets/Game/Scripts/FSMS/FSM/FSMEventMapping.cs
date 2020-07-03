using UnityEngine;
using System.Collections;


public class FSMEventMapping : MonoBehaviour {

	public FSM MyFSM;
	
	public FSMEventTransition [] EventTransitions;
	
	// Use this for initialization
	void Start () {
		
		foreach(var eventTransition in EventTransitions)
			 MyFSM.AddEventTransition(eventTransition);
		
		
	}
}
