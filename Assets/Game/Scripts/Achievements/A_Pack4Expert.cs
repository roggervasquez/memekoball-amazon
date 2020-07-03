using UnityEngine;
using System.Linq;
using System.Collections;

public class A_Pack4Expert : Achievement {

    public A_Pack4Expert()
        : base()
    {
        Name = "pack4expert";
        Description = "Complete ALL levels(except level 20) of PACK-4 with a score equal or above 3500 in all 3 Modes (Minhit, Maxturn, TimeChallenge) , Good Luck!!";

    }

    public override float  HasEarnedAchievement()
    {
        float Progress = 0F;
        var ListaScores = WhisperPlayerScores.Instance.GetCurrentLocalScores().Where
                 (
                     x => x.score.PackName.GetValue() == "PACK-4"
                     && (x.score.BestScore.AsInt() >= 3000)
                 );

        if (ListaScores.Any())
        {
            //PAra no iterar, ya se sabe que son 3 modos para cada pack y son 20 levels cada uno
            // Pero para el pack4 hay que quitar el level 20 de los 3 modos, asi que en realidad
            // solo son 19 en total x 3
            Progress = ((float)ListaScores.Count() / (float)(3 * 19)) * 100;

        }

        return Progress;

    }
}
