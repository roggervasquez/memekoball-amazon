using UnityEngine;
using System.Collections;

public class NavigationScript : MonoBehaviour
{

    public string SceneNameBackButton;
    public bool isMainMenu = false;

   

    // Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMainMenu)
            {
              Debug.Log("Quit");
              Application.Quit();   
            }
            else
            {
                Application.LoadLevel(SceneNameBackButton);
            }
  
        }
	}
}
