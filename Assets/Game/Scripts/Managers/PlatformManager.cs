﻿using UnityEngine;
using System.Collections;

public class PlatformManager : MonoBehaviour {

    public bool IsTouchSupported()
    {
        if (Application.platform == UnityEngine.RuntimePlatform.FlashPlayer ||
            Application.platform == UnityEngine.RuntimePlatform.LinuxPlayer ||
            Application.platform == UnityEngine.RuntimePlatform.OSXEditor ||
             Application.platform == UnityEngine.RuntimePlatform.OSXPlayer ||
             Application.platform == UnityEngine.RuntimePlatform.OSXWebPlayer ||
             Application.platform == UnityEngine.RuntimePlatform.WindowsEditor ||
             Application.platform == UnityEngine.RuntimePlatform.WindowsPlayer ||
             Application.platform == UnityEngine.RuntimePlatform.WindowsWebPlayer ||
             Application.platform == UnityEngine.RuntimePlatform.PS3 ||
             Application.platform == UnityEngine.RuntimePlatform.WiiPlayer

             )
        {
            return false;
        }
        else
        {
            //|| Android, Ios, Application.platform== RuntimePlatform.WP8Player), just 4.2 above

            return true;
        }
    }

    public string GetPlatformResolutionPostFix()
    {
        string temp = "";
        if (Managers.Game.Preferences.RuntimePlatform == RuntimePlatform.ANDROID)
            temp += "A";
        else if (Managers.Game.Preferences.RuntimePlatform == RuntimePlatform.IOS)
            temp += "I";
        else if (Managers.Game.Preferences.RuntimePlatform == RuntimePlatform.WP8)
            temp += "W";
        else
            temp = "P";


     
        return temp;
    }

}
