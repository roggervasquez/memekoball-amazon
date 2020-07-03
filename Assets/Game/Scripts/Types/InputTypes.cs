using UnityEngine;
using System.Collections;
using System;


// User for Input Events that generates a Vector2 parameter for a position and a float as a time
// Events such as  TapEvent, LongTapEvent etc
[Serializable]
public class InputData
{
	 public InputData()
	 {
		InputPosition = Vector2.zero;
		InputTime = 0F;
	 }
	 public InputData(Vector2 TPos, float Ttime, string taghit="")	
	{
		InputPosition = TPos;
		InputTime = Ttime;
		TagHit = taghit;
	}
	
	 public Vector2 InputPosition;
	 public float InputTime;
	 public string TagHit;
	 
	
}

[Serializable]
public class AttackDirectionInfo
{
	public Vector3 InitPos;
	public Vector3 EndPos;
	public float  ArrowDistance;
	public AttackDirectionInfo()
	{
	   InitPos = Vector3.zero;
	   EndPos = Vector3.zero;
	   ArrowDistance = 0F;	
	}
	public AttackDirectionInfo(Vector3 IP, Vector3 EP, float d)
	{
	   InitPos = IP;
	   EndPos = EP;
	   ArrowDistance = d;	
	}


}


