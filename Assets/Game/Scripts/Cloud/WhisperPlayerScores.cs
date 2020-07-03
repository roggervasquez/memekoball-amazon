using System.Linq;

using UnityEngine;
using System.Collections.Generic;
using System;


public class SyncableScore
{

    public AGSSyncableString LevelName;
    public AGSSyncableString GameType;
    public AGSSyncableString PackName;
    public AGSSyncableNumber TimeUsed;
    public AGSSyncableNumber BestScore;
    public AGSSyncableNumber Hits;
    public AGSSyncableNumber Turns;
    public AGSSyncableNumber MaxTurns;
}

//Cada level es un mapa dentro de whispersync
public class SyncableLevelScore
{
    public AGSGameDataMap map;
    public  SyncableScore score = new SyncableScore();

}

public class WhisperPlayerScores
{
    #region singleton

    private static WhisperPlayerScores instance = null;

    public static WhisperPlayerScores Instance
    {
        get
        {
            if (instance == null)
                instance = new WhisperPlayerScores();
            return instance;
        }
    }

    private WhisperPlayerScores()
    {  
        ScoreList.Clear();
       
    }

    #endregion


    private AGSGameDataMap GlobaldataMap = null; // Referencia al root 

    private List<SyncableLevelScore> ScoreList = new List<SyncableLevelScore>();


    public void Initialize()
    {


            GlobaldataMap = AGSWhispersyncClient.GetGameData();
            if (GlobaldataMap == null)
            {
                Debug.Log("Globaldata map es null");
                Managers.GameCircleAmazon.WhisperInitialized = false;
                return;
            }
        
           Debug.Log("Inicializando WhisperScores");
            ScoreList.Clear();
            foreach (var packName in Globals.Constants.PackNameArray)
                for (var i = 1; i <= Globals.Constants.LevelsPerPack; i++)
                {
                    foreach (var enumVal in Enum.GetValues(typeof (GameType)))
                    {
                      
                        SyncableLevelScore synclevelscore = new SyncableLevelScore();
                      
                      
                        synclevelscore.map =
                            GlobaldataMap.GetMap(packName + "@" + enumVal.ToString() + "@LEVEL-" + i.ToString());

                        synclevelscore.score.BestScore = synclevelscore.map.GetHighestNumber("BestScore");
                        if (!synclevelscore.score.BestScore.IsSet())
                            synclevelscore.score.BestScore.Set(0);
                         synclevelscore.score.PackName = synclevelscore.map.GetLatestString("PackName");
                         synclevelscore.score.GameType = synclevelscore.map.GetLatestString("GameType");
                         synclevelscore.score.LevelName = synclevelscore.map.GetLatestString("LevelName");
                        synclevelscore.score.PackName.Set(packName);
                        synclevelscore.score.GameType.Set(enumVal.ToString());
                        synclevelscore.score.LevelName.Set("LEVEL-" + i.ToString());

                       /* synclevelscore.score.PackName = synclevelscore.map.GetLatestString("PackName");
                          synclevelscore.score.GameType = synclevelscore.map.GetLatestString("GameType");
                           synclevelscore.score.LevelName = synclevelscore.map.GetLatestString("LevelName");
                          synclevelscore.score.Hits = synclevelscore.map.GetLatestNumber("Hits");
                          synclevelscore.score.MaxTurns = synclevelscore.map.GetLatestNumber("MaxTurns");
                          synclevelscore.score.TimeUsed = synclevelscore.map.GetLatestNumber("TimeUsed");

                        */
                        ScoreList.Add(synclevelscore);
                    }
                }

      
    }

   

    public SyncableLevelScore GetSyncLevelScore(string packName, string levelName, GameType gameType)
    {
   

       SyncableLevelScore Synlevel= ScoreList.FirstOrDefault(x => x.score.PackName.GetValue() == packName && x.score.GameType.GetValue() == gameType.ToString()
                                      && x.score.LevelName.GetValue() == levelName);


       return Synlevel;
        
    }



    public List<SyncableLevelScore> GetCurrentLocalScores()
    {

        return ScoreList;
    }
   
    public void SynchronizeScores()
    {
       
       AGSWhispersyncClient.Synchronize();
    }


    public int GetPackScore(string currentPackName)
    {
        int packScore =
            ScoreList.Where(x => x.score.PackName.GetValue() == currentPackName).Sum(x => x.score.BestScore.AsInt());

        return packScore;
    }

    public int GetGlobalScore()
    {

        int GlobalScore =
           ScoreList.Sum(x => x.score.BestScore.AsInt());

        return GlobalScore;
    }
}




