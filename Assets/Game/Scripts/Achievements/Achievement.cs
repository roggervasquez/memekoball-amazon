using System.Collections.Generic;
using System.Security.Cryptography;

using UnityEngine;
using System.Collections;

public abstract class Achievement  {

    public string Name { get; set; }
    public string Description { get; set; }
   
    protected bool Earnedflag;

    public Achievement()
    {
        Name = string.Empty;
        Description = string.Empty;
        Earnedflag = false;
    }

    public void ResetEarnedFlag()
    {
        Earnedflag = false;
    }

    // Solo para testing luego quitaarla
    public void ActivateEarnedFlag()
    {
        Earnedflag = true;
    }
    
    public bool Earned()
    {
        return Earnedflag;
    }
    public abstract float HasEarnedAchievement();

  

}
