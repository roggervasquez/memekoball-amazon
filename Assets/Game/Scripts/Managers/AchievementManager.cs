using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class AchievementManager : MonoBehaviour {

    private List< Achievement> Achievements;
    public bool AchievementsRequestSent = false;

    void Start()
    {
        Achievements = new List <Achievement>();
        Achievements.Clear();

        //instantiate all achievements classes and add them

        Achievement a = new A_MinhitNoob();
        Achievements.Add(a);
        
        a = new A_MaxturnNoob();
        Achievements.Add(a);

        a = new A_TimeNoob();
        Achievements.Add(a);

        a = new A_MinhitPlayer();
        Achievements.Add(a);

        a = new A_MaxturnPlayer();
        Achievements.Add(a);

        a = new A_TimechallengePlayer();
        Achievements.Add(a);

        a= new A_Pack1Expert();
        Achievements.Add(a);

        a = new A_Pack2Expert();
        Achievements.Add(a);

        a = new A_Pack3Expert();
        Achievements.Add(a);
        
        a = new A_Pack4Expert();
        Achievements.Add(a);

        a = new A_MillionScore();
        Achievements.Add(a);

        a = new A_ChosenOne();
        Achievements.Add(a);

        a = new A_MemekoExpert();
        Achievements.Add(a);

   
    }

    public List<Achievement> GetAllAchievements()
    {
        return Achievements;
    }
    

    public float AchievementProgress(string achievementid)
    {
        //Debug.Log("Chequear progess:" + achievementid);
        Achievement a = Achievements.FirstOrDefault(x => x.Name == achievementid);
        if (a != null)
        {
          //  Debug.Log("Encontre achivement");
            return a.HasEarnedAchievement();
        }
        else
        {
            Debug.Log("No encontre achievement");
            return 0;
        }

    }
    
   
}
