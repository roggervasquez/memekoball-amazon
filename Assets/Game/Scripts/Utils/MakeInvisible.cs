using UnityEngine;
using System.Collections;

public class MakeInvisible : MonoBehaviour
{
    public float DelayTime = 1F;
    private bool isInvisible = false;
    private MeshRenderer objectmesh;
    private float elapsedTime=0F;
	// Use this for initialization
    public MeshRenderer [] meshes;
	void Start ()
	{
	    objectmesh = gameObject.GetComponent<MeshRenderer>() as MeshRenderer;
	    if (objectmesh != null)
	    {
	        isInvisible = true;
	        objectmesh.enabled = false;
            meshes = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer meshobject in meshes)
            {
                meshobject.enabled = false;
            }
	    }
	}

    void FixedUpdate()
    {
        if (isInvisible == false)
        {
            elapsedTime += Time.deltaTime;
            Color color = renderer.material.color;
            color.a -= 0.01f;
            renderer.material.color = color;
            if (elapsedTime > DelayTime)
            {
                isInvisible = true;
                objectmesh.enabled = false;
                foreach (MeshRenderer meshobject in meshes)
                {
                    meshobject.enabled = false;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (objectmesh != null)
        {
            if (isInvisible)
            {
                isInvisible = false;
                elapsedTime = 0;
                objectmesh.enabled = true;
                Color color = renderer.material.color;
                color.a = 1F;
                renderer.material.color = color;
                foreach (MeshRenderer meshobject in meshes)
                {
                    meshobject.enabled = true;
                }
               // Invoke("Dissapear",DelayTime);
            }

        }
    }

    private void Dissapear()
    {
        isInvisible = true;
        objectmesh.enabled = false;
    }

}
