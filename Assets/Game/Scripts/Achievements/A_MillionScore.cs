using UnityEngine;
using System.Linq;
using System.Collections;

public class A_MillionScore : Achievement {

    public A_MillionScore()
        : base()
    {
        Name = "millionscoreaward";
        Description = "Obtain a Global Score  of 1 million or more(Sum of scores of all levels in any mode) , Good Luck!!";

    }

    public override float  HasEarnedAchievement()
    {
        float Progress = 0F;
        int globalscore = WhisperPlayerScores.Instance.GetGlobalScore();

        if (globalscore >= 1000000)
            Progress = 100F;
        else
        {
            Progress = ((float)globalscore / (float)(1000000)) * 100;
            
        }

        
        return Progress;

    }
}
