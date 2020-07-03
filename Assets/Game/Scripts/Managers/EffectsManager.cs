using UnityEngine;
using System.Collections;

public class EffectsManager : MonoBehaviour {
	

	public void PlayText (Vector3 position, Quaternion rotation, string svalue, float duration, float speed)
	{
		GameObject go = (GameObject)Instantiate(Resources.Load("RisingTextPrefab"),position,
					           rotation);
			
		 RisingText rt = go.GetComponent<RisingText>();
		 rt.setup(svalue,duration,speed);	
			  
	}

    public GameObject PlayConstantEffect(GameObject particleResource, Vector3 pos)
    {
        if (Managers.Game.Preferences.EnableFx == false) return null;

        if (particleResource != null)
        {


            var goP = (GameObject)Instantiate(particleResource, pos, Quaternion.identity);

            var ps = goP.GetComponent<ParticleSystem>();

            ps.Play();

            return goP;

        }
        return null;
    }

  
	public GameObject PlayConstantEffect(string particleResource,Vector3 pos)
	{
		if (Managers.Game.Preferences.EnableFx==false) return null;
		
		if(particleResource!=string.Empty)
		{
		    
			
		 var goP = (GameObject)Instantiate(Resources.Load(particleResource),pos,Quaternion.identity);
		  	
	      var  ps= goP.GetComponent<ParticleSystem>();
	  
	      ps.Play();
		  
		  return goP;
		
		}
		return null;
	}

    public void  PlayEffect(GameObject particleResource, Vector3 pos)
    {
        if (Managers.Game.Preferences.EnableFx == false) return ;

        if (particleResource != null)
        {


            var goP = (GameObject)Instantiate(particleResource, pos, Quaternion.identity);

            var ps = goP.GetComponent<ParticleSystem>();

            ps.Play();

         
            Destroy(goP, ps.duration);

        }
       
    }
	//This is for a particle resource with no looping
	public void PlayEffect(string particleResource,Vector3 pos,Quaternion rotation, float duration,bool loadbestfit=true,
		                   bool useDefault=true)
	{
		if (Managers.Game.Preferences.EnableFx==false) return;

		if(particleResource!=string.Empty)
		{
		  string temp;
		  GameObject goP;
		  GameObject particlePrefab=null;
		  if (loadbestfit)
			{
				temp = particleResource + Managers.Platform.GetPlatformResolutionPostFix();
				
		   	    particlePrefab = Resources.Load(temp) as GameObject;
		  
			} 
		  //---------
			
		  if (particlePrefab==null&&useDefault==false)
					return;
		  if (particlePrefab==null)
			{
			  	 particlePrefab = Resources.Load(particleResource) as GameObject;
				
			}
			
			if (rotation==Quaternion.identity)
		  	     goP = (GameObject)Instantiate(particlePrefab,pos,particlePrefab.transform.rotation);
			else
				 goP = (GameObject)Instantiate(particlePrefab,pos,rotation);
			
			var ps= goP.GetComponent<ParticleSystem>();
		    ps.Play();
		   Destroy(goP,duration);
			
		}
	}
}
