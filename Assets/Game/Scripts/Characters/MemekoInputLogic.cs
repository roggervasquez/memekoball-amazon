using UnityEngine;
using System.Collections;

public class MemekoInputLogic : MonoBehaviour
{

    public MemekoBall CurrentMemekoBall;
    
    private  Arrow directionArrow;
    private Vector3 initialPanPosition;
    private float initialPanTime;
	// Use this for initialization
	void Start () {
        Messenger.AddListener<InputData>(Globals.InputEvents.TapEvent_InputData, OnTap);
        Messenger.AddListener<InputData>(Globals.InputEvents.LongTapEvent_InputData, OnLongTap);
        Messenger.AddListener<InputData>(Globals.InputEvents.InitPanEvent_InputData, OnInitPan);
        Messenger.AddListener<InputData>(Globals.InputEvents.MiddlePanEvent_InputData, OnMiddlePan);
        Messenger.AddListener<InputData>(Globals.InputEvents.EndPanEvent_InputData, OnEndPan);
        Messenger.AddListener(CurrentMemekoBall.gameObject.GetInstanceID() + "EnterSelected", OnArrowStart);
        Messenger.AddListener(CurrentMemekoBall.gameObject.GetInstanceID() + "LeaveSelected", OnArrowStop);
	}

    void OnArrowStart()
    {
       
        var directionArrowObject = (GameObject)Instantiate(Resources.Load("SimpleArrowPrefab"));
        directionArrow = directionArrowObject.GetComponent<Arrow>();
        directionArrow.transform.position = CurrentMemekoBall.transform.position;
        directionArrow.startFrom = CurrentMemekoBall.transform.position;
        directionArrow.setEndTo(transform.position);

    }

    void OnArrowStop()
    {
        Destroy(directionArrow.gameObject);
        directionArrow = null;
    }

    void OnTap(InputData ID)
	{
        if (CurrentMemekoBall.EnableInputLogic == false)  // Dont process Input for the player
	   	return;   
	}

    private void OnLongTap(InputData ID)
    {
    }

    private void OnInitPan(InputData ID)
    {
        if (CurrentMemekoBall.EnableInputLogic == false)  // Dont process Input for the player
            return;

        if (CurrentMemekoBall.FSMCharacter.CurrentState.StateName != Globals.MemekoStates.Idle)
            return;


        int layerMask = 1 << Globals.Constants.LayerInputNumber;
        Ray ray = Camera.main.ScreenPointToRay(ID.InputPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
               
                initialPanPosition[0] = hit.point.x;
                initialPanPosition[1] = CurrentMemekoBall.transform.position.y;
                initialPanPosition[2] = hit.point.z;
                
                CurrentMemekoBall.FSMCharacter.SendMessageToCurrentState("SelectPlayer");
               
        }
    }

    private void OnMiddlePan(InputData ID)
    {
        if (CurrentMemekoBall.EnableInputLogic == false)  // Dont process Input for the player
            return;

        if (CurrentMemekoBall.FSMCharacter.CurrentState.StateName != Globals.MemekoStates.Selecting)
            return;
   
        int layerMask = 1 << Globals.Constants.LayerInputNumber;
        Ray ray = Camera.main.ScreenPointToRay(ID.InputPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 temp = new Vector3(hit.point.x, CurrentMemekoBall.transform.position.y, hit.point.z);
            Vector3 v = temp - this.initialPanPosition;

            float distance = v.magnitude;

            if (distance >= Globals.GameValues.BallMaxDragDistance)
                distance = Globals.GameValues.BallMaxDragDistance;

            Vector3 newPos;
            if(Managers.Game.Preferences.CurrentInputMode==InputMode.FORWARD)
                newPos = CurrentMemekoBall.transform.position + v.normalized * distance;
            else
                newPos = CurrentMemekoBall.transform.position - v.normalized * distance;

            newPos[1] = CurrentMemekoBall.transform.position.y;
         
            directionArrow.setEndTo(newPos);
       
       }	
    }

    private void OnEndPan(InputData ID)
    {
        if (CurrentMemekoBall.EnableInputLogic == false)  // Dont process Input for the player
            return;

        if (CurrentMemekoBall.FSMCharacter.CurrentState.StateName != Globals.MemekoStates.Selecting)
            return;

       int layerMask = 1 << Globals.Constants.LayerInputNumber;
        Ray ray = Camera.main.ScreenPointToRay(ID.InputPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            
            float deltaDist = (Globals.GameValues.BallColliderSize / 2F);
            if (directionArrow.getDistance() <= deltaDist) //  
            {
                CurrentMemekoBall.FSMCharacter.SendMessageToCurrentState("UnSelectPlayer");
            }
            else
            {
                Vector3 newDirection = hit.point;
                newDirection[1] = CurrentMemekoBall.transform.position.y;
                AttackDirectionInfo directionInfo = new AttackDirectionInfo(initialPanPosition, newDirection,
                                      directionArrow.getDistance());

                CurrentMemekoBall.FSMCharacter.SendMessageToCurrentState("LaunchBall", directionInfo);
            }
        }
       

    }


}
