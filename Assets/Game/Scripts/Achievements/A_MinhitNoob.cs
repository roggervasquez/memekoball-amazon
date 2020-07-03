using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class A_MinhitNoob : Achievement {

    public A_MinhitNoob() :base()
    {
        Name = "minhitnoob";
        Description = "To earn this achievement you have to complete the first 5 levels of all packs in Minimum Hit Mode, Good Luck!!";

    }

    // La lista de level scores debe venir actualizada, o sea sincronizada
    // previamente
    public override float  HasEarnedAchievement( )
    {
       // Debug.Log("Dentro de minhitnoob");
        float Progress = 0F;
        var ListaScores =  WhisperPlayerScores.Instance.GetCurrentLocalScores().Where
                 (x => x.score.GameType.GetValue() == GameType.MINHITS.ToString() 
                     &&
                     (x.score.LevelName.GetValue() == "LEVEL-1" || x.score.LevelName.GetValue() == "LEVEL-2"||
                     x.score.LevelName.GetValue() == "LEVEL-3" || x.score.LevelName.GetValue() == "LEVEL-4"||
                     x.score.LevelName.GetValue() == "LEVEL-5"
)
                 );

        if (ListaScores.Any())
        {
         //   Debug.Log("Minhitnoob hay datos en lista" + ListaScores.Count());
            int passed = 0;
            foreach (SyncableLevelScore levelScore in ListaScores)
            {
                if (levelScore.score.BestScore.AsInt() > 0)
                    passed++;
            }

            Progress = ((float)passed / (float)ListaScores.Count()) * 100;
           // Debug.Log("Passed " + passed.ToString() + " PRogress" + Progress.ToString());
        }

        return Progress;

    }
}
