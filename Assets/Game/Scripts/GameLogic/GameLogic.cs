using UnityEngine;
using System.Collections;

public abstract class GameLogic : MonoBehaviour
{
    public Camera mainCamera;
    public FSM FSMGameLogic;
    public InputControllerScript InputController;
    public ReplayManager ReplayControl;
    public UILabel LabelTime;
    public UIButton ButtonBack;
    public UIButton ButtonPause;
    public UIButton ButtonRetry;
    public UISprite RecordIndicator;


    [HideInInspector]
    public int globalSeconds;
    [HideInInspector]
    public float CurrenTopScore;
    [HideInInspector]
    public float CurrentScore;


    [HideInInspector]
    public DodgeBall [] DodgeBalls;
    [HideInInspector]
    public MemekoBall CurrentMemeko;

    public virtual void LoadLevelObjects()
    {
        NewLevelLoader.LoadLevel(Managers.Game.Preferences.CurrentPackName, Managers.Game.Preferences.CurrentLevelName);
        var currentMemekoObject = GameObject.FindWithTag(Globals.Tags.Player);
        CurrentMemeko = currentMemekoObject.GetComponent<MemekoBall>();
        //Disable input logic while loading.
        CurrentMemeko.EnableInputLogic = false;
        CurrentMemeko.Power = Managers.Game.CurrentLevelXmlInfo.MemekoForce;
        mainCamera.orthographicSize = Managers.Game.CurrentLevelXmlInfo.CameraSize;
        // Find the DodgeBalls

        // Get all the Enemies in the scene
        var dodgeballObjects = GameObject.FindGameObjectsWithTag(Globals.Tags.Dodgeball);
        DodgeBalls = new DodgeBall [dodgeballObjects.Length];
        for (int i = 0; i < dodgeballObjects.Length; i++)
        {
            DodgeBalls[i] = dodgeballObjects[i].GetComponent<DodgeBall>();
          
        }
    }

    public void EnableTopBarButtons()
    {

        ButtonBack.isEnabled = true;
        ButtonPause.isEnabled = true;
        ButtonRetry.isEnabled = true;
    }

    public void DisableTopBarButtons()
    {
        ButtonBack.isEnabled = false;
        ButtonPause.isEnabled = false;
        ButtonRetry.isEnabled = false;
    }
    public bool AllDodgeBallsOntheHole()
    {
        for (int i = 0; i < DodgeBalls.Length; i++)
        {
            if (DodgeBalls[i].FSMCharacter.CurrentState.StateName != Globals.MemekoStates.OnTheHole)
                return false;
        }
        return true;
    }

    public void ShowRecordIndicator(bool flag)
    {
        if(ReplayControl.isSupported)
           RecordIndicator.gameObject.SetActive(flag);
    }


    public bool AllDodgeBallsStationary()
    {
        for (int i = 0; i < DodgeBalls.Length; i++)
        {
            if (!(
                  (DodgeBalls[i].FSMCharacter.CurrentState.StateName == Globals.MemekoStates.Idle)||
                  (DodgeBalls[i].FSMCharacter.CurrentState.StateName == Globals.MemekoStates.OnTheHole)
                 )
                )
                return false;
        }
        return true;
		
    }
    public abstract int getCurrentTurns();
    public abstract int getCurrentHits();
    public abstract float getCurrentScore();
    public abstract float getTopLevelScore();

}
