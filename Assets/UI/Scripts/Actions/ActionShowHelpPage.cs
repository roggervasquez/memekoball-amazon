using UnityEngine;
using System.Collections;

public class ActionShowHelpPage : Action
{

    public Transform objecttransform;
    public float deltax;
    public int page;
	
	public override void ActionPerformed ()
	{
	   objecttransform.localPosition = new Vector3((deltax * page),
                                              objecttransform.localPosition.y ,
                                              objecttransform.localPosition.z);
	}
}
