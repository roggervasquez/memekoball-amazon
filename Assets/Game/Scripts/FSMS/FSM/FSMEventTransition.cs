using System;

[Serializable()]
public class FSMTransition
{
	public FSMState FromState;
	public FSMState ToState;
}


[Serializable()]
public class FSMEventTransition
{
	
	public string EventName;
	public FSMTransition [] Transitions;
}

