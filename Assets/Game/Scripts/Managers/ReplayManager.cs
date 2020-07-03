using UnityEngine;
using System.Collections;

public class ReplayManager : MonoBehaviour
{

    public bool lowMemoryDevice= true;
    public bool EnableReplay = false;
    [HideInInspector]
    public bool isRecording = false;
    [HideInInspector]
    public bool isPaused = false;
    [HideInInspector]
    public bool isRecordingFinished = false;
    [HideInInspector]
    public bool isSupported = false;
    [HideInInspector]
    public string ThumbnailPath;

   
    [HideInInspector]
    public  Texture2D previousThumbnail;

    private void Awake()
    {
       
    }
    private void Start()
    {
      
        if (Everyplay.SharedInstance != null)
        {
           
            isSupported = Everyplay.SharedInstance.IsRecordingSupported();
            Everyplay.SharedInstance.SetLowMemoryDevice(lowMemoryDevice);
            Everyplay.SharedInstance.RecordingStarted += RecordingStarted;
            Everyplay.SharedInstance.RecordingStopped += RecordingStopped;
            Everyplay.SharedInstance.ThumbnailReadyAtFilePath += ThumbnailReadyAtFilePath;


        }

    }
    void OnDestroy()
    {
        if (Everyplay.SharedInstance != null)
        {
            Everyplay.SharedInstance.RecordingStarted -= RecordingStarted;
            Everyplay.SharedInstance.RecordingStopped -= RecordingStopped;
            Everyplay.SharedInstance.ThumbnailReadyAtFilePath -= ThumbnailReadyAtFilePath;
        }
      
    }

    public void ActivateReplay()
    {
        if (Everyplay.SharedInstance != null)
        {
            isSupported = Everyplay.SharedInstance.IsRecordingSupported();
            if (Everyplay.SharedInstance.IsRecording())
                Everyplay.SharedInstance.StopRecording();

        }
        isRecording = false;
        isPaused = false;
        isRecordingFinished = false;
        EnableReplay = true;
    }

    public void DeActivateReplay()
    {
        if (Everyplay.SharedInstance != null)
        {
            isSupported = Everyplay.SharedInstance.IsRecordingSupported();
            if (Everyplay.SharedInstance.IsRecording())
                Everyplay.SharedInstance.StopRecording();

        }
        
        isRecording = false;
        isPaused = false;
        isRecordingFinished = false;

        EnableReplay = false;
    }
    public void PauseRecording()
    {
        if (!EnableReplay) return;
        if (isRecording && !isPaused)
        {
            if (isSupported)
                 Everyplay.SharedInstance.PauseRecording();
            
            isPaused = true;
        }
    }
    public void ResumeRecording()
    {
        if (!EnableReplay) return;
        if (isRecording && isPaused)
        {
            if (isSupported)
                Everyplay.SharedInstance.ResumeRecording();

            isPaused = false;
        }
    }

    public void StopRecording()
    {
        if (!EnableReplay) return;
        if ( isRecording)
           if (isSupported)
              Everyplay.SharedInstance.StopRecording();
           else
           {
               RecordingStopped(); // For debuggin purpose on the editor

           }
    }
    public void StartRecording()
    {
        if (!EnableReplay) return;
        if ( !isRecording)
            if (isSupported)
                Everyplay.SharedInstance.StartRecording();
            else
            {
                RecordingStarted();
            }
    }

    public void ReplaySavedRecording()
    {
        if (!EnableReplay) return;
       
        if (isSupported && isRecordingFinished)
        {
            print("REplaying");
            Everyplay.SharedInstance.PlayLastRecording();
        }
    }

    public void SetMetaData(string key, object value)
    {
        if (!EnableReplay) return;
        if (isSupported )
        {
            Everyplay.SharedInstance.SetMetadata(key, value);
        }
    }
    public void LoadReplayThumbNail()
    {
        if (!EnableReplay) return;
        if(isSupported&&isRecordingFinished)
            Everyplay.SharedInstance.TakeThumbnail();
    }
    private void ThumbnailReadyAtFilePath(string path)
    {
        ThumbnailPath = path;
        Everyplay.SharedInstance.LoadThumbnailFromFilePath(path, ThumbnailSuccess, ThumbnailError);
    }
    private void ThumbnailSuccess(Texture2D texture)
    {
        if (texture != null)
        {
            previousThumbnail = texture;
        }
    }

    private void ThumbnailError(string error)
    {
        Debug.Log("Thumbnail loading failed: " + error);
    }

    private void RecordingStarted()
    {
        isRecording = true;
        isPaused = false;
        isRecordingFinished = false;
    }

    private void RecordingStopped()
    {
        isRecording = false;
        isRecordingFinished = true;
    }

  
      
}
