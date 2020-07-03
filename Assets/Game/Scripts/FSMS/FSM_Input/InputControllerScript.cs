using UnityEngine;
using System.Collections;



public class InputControllerScript : MonoBehaviour {
	   public float deltaTAP=0.2F;
	   public float zoomDelta=0.5F;
	   public float deltaPanVsLongTap = 0.3F;
	   public float minZoomFingerDistance=2.5F;
	   public float deltaMouseTAP = 0.12F;
	   public float deltaMouseLONGTAP = 2F;
	   public bool TOUCHINTERFACEDETECTED = false; 
        
	   //Assocaite NGUI camera 
	   public Camera nGuiCamera=null;
	   public bool enableInputController = true;
	  
	   public FSM InputFSM;
	   // Private variables
	   [HideInInspector]
	   public float initialDistanceZoom = 0F;
	   [HideInInspector]
	   public float lastZoomDistance = 0F;
	
	   [HideInInspector]
	   public Vector2 PanTouchInitialPos;
	   [HideInInspector]
	   public Vector2 PanTouchEndPos;
	   [HideInInspector]
	   public float PanTouchInitialTime;
	   [HideInInspector]
	   public float PanTouchEndTime;
	    [HideInInspector]
	   public bool possibleTap;
	
	 [HideInInspector]
	  public bool possibleLongTAP;
	 [HideInInspector]
	  public bool startedMovingPanning;
	
	
	   
	
	   // keep tracking of 3 fingers
	 [HideInInspector]
	  public Touch theTouch0;
	 [HideInInspector]
	  public Touch theTouch1;
	 [HideInInspector]
	  public Touch theTouch2;
	// Use this for initialization
	
	
	
	
	
	void Start ()
	{

	    TOUCHINTERFACEDETECTED = Managers.Platform.IsTouchSupported();


	}


   
}
