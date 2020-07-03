using UnityEngine;
using System.Linq;
using System.Collections;

public class A_TimeNoob : Achievement {


    public A_TimeNoob()
        : base()
    {
        Name = "timenoob";
        Description = "To earn this achievement you have to complete the first 5 levels of all packs in Time Challenge Mode, Good Luck!!";

    }

    public override float HasEarnedAchievement()
    {

        float Progress = 0F;
        var ListaScores = WhisperPlayerScores.Instance.GetCurrentLocalScores().Where
                 (x => x.score.GameType.GetValue() == GameType.CHALLENGE.ToString()
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
