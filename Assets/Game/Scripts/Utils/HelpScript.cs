using UnityEngine;
using System.Collections;

public class HelpScript : MonoBehaviour
{

    public Transform guiCamera;
    public float deltax= 460F;

  
	// Use this for initialization

    void Awake()
    {
        if (Managers.Game.CheckHelpFirstTime)
        {
            if (Managers.Game.Preferences.GameType == GameType.MINHITS)
            {

                if (Managers.Game.Preferences.MinHelp == false)
                {
                    Application.LoadLevel(Managers.Game.NextSceneToLoad);
                }
                else
                {
                    Managers.Game.Preferences.MinHelp = false;
                }
            }

            if (Managers.Game.Preferences.GameType == GameType.MAXHITS)
            {

                if (Managers.Game.Preferences.MaxHelp == false)
                {
                    Application.LoadLevel(Managers.Game.NextSceneToLoad);
                }
                else
                {
                    Managers.Game.Preferences.MaxHelp = false;
                }
            }
            if (Managers.Game.Preferences.GameType == GameType.CHALLENGE)
            {

                if (Managers.Game.Preferences.TimeHelp == false)
                {
                    Application.LoadLevel(Managers.Game.NextSceneToLoad);
                }
                else
                {
                    Managers.Game.Preferences.TimeHelp = false;
                }
            }


            Managers.Game.Preferences.SavePreferences();
        }
    }
	void Start () {
        guiCamera.localPosition = new Vector3((deltax * Managers.Game.HelpPageToLoad),
                                              guiCamera.localPosition.y,
                                              guiCamera.localPosition.z);

	}
	
}
