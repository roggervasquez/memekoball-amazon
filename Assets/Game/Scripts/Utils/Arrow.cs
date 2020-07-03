using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	
	public Vector3 startFrom;
	public Vector3 endTo;

    void Start () {
	
	   startFrom = transform.position;
	   endTo =transform.position;
	}
	
	public float getDistance()
	{
	  return (Vector3.Distance(startFrom,endTo));	
	}
	
	public void setEndTo(Vector3 v)
	{
		    endTo = v;
			Vector3 diff=endTo - startFrom;
		   
		    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,diff.magnitude/2F);
			
			
		    transform.LookAt(endTo);
		    Vector3 dir = (endTo-startFrom).normalized;
			//transform.position = startFrom+dir*(Globals.GameValues.BallColliderSize/1.7F);
	        transform.position = startFrom;
	
	}
		
	
}
