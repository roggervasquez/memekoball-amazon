using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FSM : MonoBehaviour {
	
    
	
	Dictionary<string,FSMState>  States;
    Dictionary<string,FSMState>  EventsTransitions;
   
	
	
	public FSMState CurrentState;

    public string DefaultScriptPath = "Assets";
    public string BaseStateScriptClass = "FSMState";

	private bool ProcessUpdate=true;
	
	void Awake () {
	       EventsTransitions = new Dictionary<string, FSMState> ();
		   var statesList = GetComponentsInChildren<FSMState>();
		   States = new Dictionary<string, FSMState>();
		   foreach(var state in statesList)
		   {
			   States.Add(state.StateName,state);
			   if(state.StartState)
				 CurrentState = state;
		   }
		              
		   BuildTransitions();
		  
		   //CurrentState.Enter(null);
	}
	
	void Start()
	{
			   CurrentState.Enter(null);
	}
	void BuildTransitions()
	{
		foreach(var state in States.Values)
			foreach(var transition in state.Transitions)
				if(States.ContainsKey(transition.ToState))
			            EventsTransitions.Add(state.StateName+":"+transition.EventName ,States[transition.ToState]);
	}
	
	

	void Update () {
		if (ProcessUpdate)
		{	
		 FSMEvent ev =  CurrentState.Process();
		 RaiseEvent(ev);
		}
    }
	
	void ChangeState(FSMState nextState)
	{
	   ProcessUpdate=false;	
	   CurrentState.Leave(nextState);
	   nextState.Enter(CurrentState);
	   CurrentState = nextState;
	   ProcessUpdate = true;	
	}
	
	
	
	public void RaiseEvent(FSMEvent evnt)
	{
		if(evnt!=null)
		{
		   if(EventsTransitions.ContainsKey(CurrentState.StateName+":"+evnt.Name))
		   {
		     FSMState nextState = EventsTransitions[CurrentState.StateName+":"+evnt.Name];
		     ChangeState(nextState);
		   }
		   else 
			 Debug.LogWarning("Event "+evnt.Name+" not mapped at "+CurrentState.StateName);
		}
		
	}
	
	public void RaiseEvent(string eventName)
	{

		RaiseEvent(new FSMEvent(eventName));
	}
	
	
	
	//TODO: lo mas seguro hay que borrar esta
	public void AddEventTransition(FSMEventTransition eventTransition)
	{
		foreach(var transition in eventTransition.Transitions)
		     EventsTransitions.Add(transition.FromState.StateName+":"+eventTransition.EventName,States[transition.ToState.StateName]);
	}
	
	public void SendMessageToCurrentState(string methodName )
	{
		if(CurrentState!=null)
		   CurrentState.SendMessage(methodName,SendMessageOptions.DontRequireReceiver);
	}
	
	public void SendMessageToCurrentState(string methodName,object value)
	{
		if(CurrentState!=null)
		   CurrentState.SendMessage(methodName,value,SendMessageOptions.DontRequireReceiver);
		
	}
	
	
}
