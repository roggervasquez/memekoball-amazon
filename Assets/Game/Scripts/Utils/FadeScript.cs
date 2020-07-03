using UnityEngine;
using System.Collections;

public class FadeScript : MonoBehaviour
{

    public float DelayTime = 2F;
    public float deltaValue = 0.009F;
    private float elapsedTime = 0F;
    private TextMesh textmesh;
    // Use this for initialization
    private void Start()
    {
        textmesh = gameObject.GetComponent<TextMesh>();
    }

    private void FixedUpdate()
    {

        elapsedTime += Time.deltaTime;
        Color color = textmesh.color;
        color.a -= deltaValue;
        textmesh.color = color;
        if (elapsedTime > DelayTime)
        {
            Destroy(transform.gameObject);
        }
    }
}
