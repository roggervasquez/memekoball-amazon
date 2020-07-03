using UnityEngine;
using System.Linq;
using System.Collections;

public class A_ChosenOne : Achievement {

    public A_ChosenOne()
        : base()
    {
        Name = "trolllevelchallenge";
        Description = "Complete Level 20 of PACK-4 in all 3 modes (Minhit,Maxturn, TimeChallenge) Good Luck!!";

    }

    public override float HasEarnedAchievement()
    {
        float Progress = 0F;
        var ListaScores = WhisperPlayerScores.Instance.GetCurrentLocalScores().Where
                 (
                     x => x.score.PackName.GetValue() == "PACK-4"
                     && x.score.LevelName.GetValue()=="LEVEL-20"
                     && x.score.BestScore.AsInt() >  0
                 );

        if (ListaScores.Any())
        {
            Progress = ((float)ListaScores.Count() / (float)(3) ) * 100;

        }

        return Progress;

    }

}
