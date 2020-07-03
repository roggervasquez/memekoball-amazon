using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {


    public string tag;
    public AudioClip SoundEffect;
    public float SoundVolume = 1F;
    public bool ApplyRandomForce = false;
    public float MaxRandomForce = 20F;
    public GameObject RandomForceEffect;

    void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == tag)
        {
            if (SoundEffect != null)
            {
                Managers.Audio.Play(SoundEffect, other.transform.position, SoundVolume);
            }
            if (ApplyRandomForce)
            {
                if (RandomForceEffect != null)
                {
                    Vector3 pos = transform.position + new Vector3(0F, .5F, 0F);
                    Managers.Effects.PlayEffect(RandomForceEffect, pos);
                }
                Rigidbody coll = other.attachedRigidbody;
                if (coll != null)
                {
                    Vector3 ForceVector = new Vector3(Random.Range(-MaxRandomForce, MaxRandomForce), 0,
                                                      Random.Range(-MaxRandomForce, MaxRandomForce));
                    coll.AddForce(ForceVector, ForceMode.Impulse);
                }
            }
        }

    }

}
