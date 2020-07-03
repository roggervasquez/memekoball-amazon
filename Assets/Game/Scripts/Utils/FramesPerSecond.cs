using UnityEngine;
using System.Collections;

public class FramesPerSecond : MonoBehaviour {
	

	public  float updateInterval = 0.5F;
	//Donde aparecera el texto del FPS
	public int ScreenX = 0;
	public int ScreenY = 0;
	
	 
	private float accum   = 0; // FPS accumulated over the interval
	private int   frames  = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval
	private string fpsText="";
	
	
void Start()
{
	
     timeleft = updateInterval;  
}
 
void Update()
{
    timeleft -= Time.deltaTime;
    accum += Time.timeScale/Time.deltaTime;
    ++frames;
    
    // Interval ended - update GUI text and start new interval
    if( timeleft <= 0.0 )
    {
        // display two fractional digits (f2 format)
	    float fps = accum/frames;
	    string format = System.String.Format("{0:F2} FPS",fps);
	    fpsText = format;
				
	    timeleft = updateInterval;
	    accum = 0.0F;
	    frames = 0;
    }
}
	
	
void OnGUI()
{
  GUI.Label(new Rect(this.ScreenX,this.ScreenY,100,100),fpsText);

}
	
}
