using Holoville.HOTween;
using UnityEngine;
using System.Collections;

public class MoveObject : MonoBehaviour {

	// Use this for initialization
    public Vector3 OffSetPos;
    public float duration = 2F;
    public string Propiedad = "position";
    public bool LoopMove = true;

    private void Start()
    {
        if (LoopMove)
           HOTween.To(transform, duration, new TweenParms().Prop(Propiedad,transform.position+OffSetPos).Ease(EaseType.Linear ).Loops(-1, LoopType.Yoyo));
        else
        {
            HOTween.To(transform, duration, new TweenParms().Prop(Propiedad, transform.position + OffSetPos).Ease(EaseType.Linear));
            
        }
    
    }

	
	
}
