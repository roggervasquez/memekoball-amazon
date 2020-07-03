using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class A_MaxturnNoob : Achievement {

    public A_MaxturnNoob()
        : base()
    {
        Name = "maxturnnoob";
        Description = "To earn this achievement you have to complete the first 5 levels of all packs in Max Turns Mode, Good Luck!!";

    }

    // La lista de level scores debe venir actualizada, o sea sincronizada
    // previamente
    public override float HasEarnedAchievement( )
    {
     
        float Progress = 0F;
        var ListaScores = WhisperPlayerScores.Instance.GetCurrentLocalScores().Where
                 (x => x.score.GameType.GetValue() == GameType.MAXHITS.ToString()
                     &&
                     (x.score.LevelName.GetValue() == "LEVEL-1" || x.score.LevelName.GetValue() == "LEVEL-2" ||
                     x.score.LevelName.GetValue() == "LEVEL-3" || x.score.LevelName.GetValue() == "LEVEL-4" ||
                     x.score.LevelName.GetValue() == "LEVEL-5")
                 );

        if (ListaScores.Any())
        {
           
            int passed = 0;
            foreach (SyncableLevelScore levelScore in ListaScores)
            {
                if (levelScore.score.BestScore.AsInt() > 0)
                    passed++;
            }

            Progress = ((float)passed / (float)ListaScores.Count()) * 100;
            
        }

        return Progress;
    }
}
