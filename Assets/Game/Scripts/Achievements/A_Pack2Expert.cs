﻿using UnityEngine;
using System.Linq;
using System.Collections;

public class A_Pack2Expert : Achievement {

    public A_Pack2Expert()
        : base()
    {
        Name = "pack2expert";
        Description = "Complete ALL levels of PACK-2 with a score equal or above 4000 in all 3 Modes (Minhit, Maxturn, TimeChallenge) , Good Luck!!";

    }

    public override float HasEarnedAchievement()
    {


        float Progress = 0F;
        var ListaScores = WhisperPlayerScores.Instance.GetCurrentLocalScores().Where
                 (
                     x => x.score.PackName.GetValue() == "PACK-2"
                     && (x.score.BestScore.AsInt() >= 4000)
                 );

        if (ListaScores.Any())
        {
            //PAra no iterar, ya se sabe que son 3 modos para cada pack y son 20 levels cada uno 
            Progress = ((float)ListaScores.Count() / (float)(3 * 20)) * 100;

        }

        return Progress;
    }
}
