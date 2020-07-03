using UnityEngine;
using System;
using System.Collections;

public class LevelTransitionScript : MonoBehaviour {

	  IEnumerator Start() {
        AsyncOperation async = Resources.UnloadUnusedAssets();
        yield return async;

       
        if (Managers.Game.Preferences.GameType== GameType.MINHITS)
        {
            Application.LoadLevel(Globals.Constants.MinHitScene);
        }
        if (Managers.Game.Preferences.GameType == GameType.MAXHITS)
        {
            Application.LoadLevel(Globals.Constants.MaxHitScene);
        }
        if (Managers.Game.Preferences.GameType == GameType.CHALLENGE)
        {
            Application.LoadLevel(Globals.Constants.ChallengeScene);
        }

     }
}
