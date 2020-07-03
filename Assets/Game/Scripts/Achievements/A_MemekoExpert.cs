using UnityEngine;
using System.Linq;
using System.Collections;

public class A_MemekoExpert : Achievement {

    public A_MemekoExpert()
        : base()
    {
        Name = "memekoballexpert";
        Description = "Earn ALL Achievements, Good Luck!!";

    }

    public override float  HasEarnedAchievement()
    {
        float Progress = 0F;

        var ListaAchievements = Managers.GameCircleAmazon.achievementList.Where(x => x.isUnlocked == true);

        // Si los achievements unlock son igual a 11, de los 12   que son, es que ya se logreo este
        if (ListaAchievements.Count() == 11)
            Progress = 100F;
        else
        {
            Progress = ((float) ListaAchievements.Count()/(float)11)*100;
        }
        return Progress;
    }
}
