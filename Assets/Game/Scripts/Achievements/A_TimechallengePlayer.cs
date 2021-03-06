﻿using UnityEngine;
using System.Linq;
using System.Collections;

public class A_TimechallengePlayer : Achievement {

    public A_TimechallengePlayer()
        : base()
    {
        Name = "timeplayer";
        Description = "Complete ALL levels of ALL packs(except level-20 of PACK-4) in Time Challenge Mode, Good Luck!!";

    }

    public override float  HasEarnedAchievement()
    {

        float Progress = 0F;
        var ListaScores = WhisperPlayerScores.Instance.GetCurrentLocalScores().Where
                 (x => x.score.GameType.GetValue() == GameType.CHALLENGE.ToString()
                     && (x.score.BestScore.AsInt() > 0) &&
                    !(x.score.PackName.GetValue() == "PACK-4" && x.score.LevelName.GetValue() == "LEVEL-20")
                 );

        if (ListaScores.Any())
        {
            //PAra no iterar, ya se sabe que son 4 packs x 20 levels cada uno en un modo
            // asi que es el gran total , el count cuenta todos aquellos levels de minhits de
            // todos los packs con score > 0 ,o sea ya lo paso, excepto el level 20 del pack4
            Progress = ((float)ListaScores.Count() / (float)((4 * 20) - 1)) * 100;

        }

        return Progress;
    }
}
