using System;

public class FSMEvent
{
	
	string name;
	
	public string Name {
		get {
			return this.name;
		}
	}
	
	public FSMEvent(string name)
	{
		this.name = name;
	}
}

