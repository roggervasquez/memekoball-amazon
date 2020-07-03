using UnityEngine;
using System.Collections;

public class ColliderScript : MonoBehaviour
{

    public string tag;
    public AudioClip SoundEffect;
    public float SoundVolume = 1F;
    public GameObject ParticleEffect;

	void Start () {
	
	}
	void OnCollisionEnter(Collision collision)
	{

	    if (collision.gameObject.tag == tag)
	    {
	       
            if (SoundEffect != null)
	        {
	            Managers.Audio.Play(SoundEffect, collision.transform.position, SoundVolume);
	        }
	        if (ParticleEffect != null)
	        {
                ContactPoint contact = collision.contacts[0];
                //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 pos = contact.point;
	            Managers.Effects.PlayEffect(ParticleEffect,pos);
	        }
	        

	    }

	    
	}
}
