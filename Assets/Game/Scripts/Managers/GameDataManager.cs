
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using UnityEngine;
using System;



#if UNITY_WINRT && !UNITY_EDITOR  
   using LegacySystem.IO;  

#else
    using System.IO;
#endif


[Serializable]


public class GameDataManager : MonoBehaviour
{

   
    
  
    void Start()
    {

     
     
    

    }

    
   
    public  int  GetLevelTopScore(string packName, string levelName,GameType type)
    {
        int Score = 0;
      
        SyncableLevelScore slevel = WhisperPlayerScores.Instance.GetSyncLevelScore(packName,
                                   levelName, type);
        if (slevel != null)
        {

            Score = slevel.score.BestScore.AsInt();
        }
        
        return Score;
    }
    public int GetLevelTopScore(string packName, int levelNumber,GameType type)
    {
        int Score = 0;
       

        SyncableLevelScore slevel = WhisperPlayerScores.Instance.GetSyncLevelScore(packName,
                                      "LEVEL-" + levelNumber.ToString(), type);

        if (slevel != null)
        {
          
            Score = slevel.score.BestScore.AsInt();
        }
        
        return Score;
    }


    public string GetLeaderBoardName(string currentPackName)
    {
        if (currentPackName == "PACK-1")
            return Globals.Constants.LeaderBoardPack1;
        if (currentPackName == "PACK-2")
            return Globals.Constants.LeaderBoardPack2;
        if (currentPackName == "PACK-3")
            return Globals.Constants.LeaderBoardPack3;
        if (currentPackName == "PACK-4")
            return Globals.Constants.LeaderBoardPack4;
        else
        {
            return string.Empty;
        }

    }
}
