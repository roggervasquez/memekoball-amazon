using System;
using UnityEngine;

using System.Collections;

public class LoadGameCircleUserInfo : MonoBehaviour
{

    public UILabel Info;
    public UILabel Message;



    private void Start()
    {
        if (AGSClient.IsServiceReady() && AGSPlayerClient.IsSignedIn())
        {
            if (Managers.GameCircleAmazon.achievementList==null)
                Managers.GameCircleAmazon.RequestAchievements();

        }

	}

  
    void Update()
    {
        if (AGSPlayerClient.IsSignedIn())
        {
                Info.text = "You are signed in!!!";
          
        }
      
        else
        {
            Info.text = "Not signed in :(";
        }
        

        

    }
   
}
