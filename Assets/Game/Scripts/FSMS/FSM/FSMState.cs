using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public abstract class FSMState : MonoBehaviour {
	
	
	public bool StartState;
	protected FSM MyFSM;
	
	//Esta variable es usada en el editor para recordar donde fue posicionada la ultima ves
	[HideInInspector()]
	public Rect Frame;
	//Esta variable es usada en el editor para recordar  sus transiciones
	[HideInInspector]
	public List<FSMStateTransition> Transitions;
	
	public virtual FSMEvent Process()
	{
		return null;
	}
	
	public void Awake()
	{
		MyFSM = transform.parent.gameObject.GetComponent<FSM>();
	}
	
	public virtual void Enter(FSMState lastState)
	{
	}
	public virtual void Leave(FSMState nextState)
	{
	}
	
	
	public string StateName
	{
		get
		{
			return name;
		}
	}
	
	
}
