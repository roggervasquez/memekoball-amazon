using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class AmazonCloudManager : MonoBehaviour
{

    #region Initialization Stuff
    public enum EInitializationStatus
    {
        Uninitialized,
        InitializationRequested,
        Ready,
        Unavailable,
    }
    public EInitializationStatus initializationStatus = EInitializationStatus.Uninitialized;
    bool usesLeaderboards = true;
    bool usesAchievements = true;
    bool usesWhispersync = true;

    bool enablePopups = true;
    private GameCirclePopupLocation toastLocation = GameCirclePopupLocation.TOP_CENTER;

    public  bool WhisperInitialized = false;
    public List<AGSAchievement> achievementList = null;
    #endregion

    #region Player Stuff

    public string playerStatus = null;
    public string playerStatusMessage = null;

    // the player information.
    public AGSPlayer player = null;
    private Boolean haveSubscribedToSignedInStateChangeEvents = false;
  
    #endregion

    #region Local const strings
    // The title of this menu
   
    // UI labels for player retrieval callbacks.
    private const string playerReceivedLabel = "Retrieved local player data";
    private const string playerFailedLabel = "Failed to retrieve local player data";
    private const string playerRetrievingLabel = "Retrieving local player data...";
  
   

    #endregion
    // Use this for initialization

    private bool initialized = false;
	void Start () {
	  InitializeGameCircle();

      AGSPlayerClient.OnSignedInStateChangedEvent += OnSignedInStateChanged;

	}

    void InitializeGameCircle()
    {
        //Revisar si service readu
        if (AGSClient.IsServiceReady()) return;
        // Step the initialization progress forward.
        initializationStatus = EInitializationStatus.InitializationRequested;
      
        
        SubscribeToGameCircleInitializationEvents();
      //  SubscribeToSubmitAchievementEvents();
        
        // Begin GameCircle initialization.
        Debug.Log("Llamando a Init GameCircle");
        AGSClient.Init(usesLeaderboards, usesAchievements, usesWhispersync);
      

    }
   
    private void SubscribeToGameCircleInitializationEvents()
    {
        AGSClient.ServiceReadyEvent += ServiceReadyHandler;
        AGSClient.ServiceNotReadyEvent += ServiceNotReadyHandler;
    }
    
    private void UnsubscribeFromGameCircleInitializationEvents()
    {
        AGSClient.ServiceReadyEvent -= ServiceReadyHandler;
        AGSClient.ServiceNotReadyEvent -= ServiceNotReadyHandler;
    }
    private void ServiceNotReadyHandler(string error)
    {
        Debug.Log("my Service not ready Handler " + error);
        initializationStatus = EInitializationStatus.Unavailable;
       // Once the callback is received, these events do not need to be subscribed to.
        UnsubscribeFromGameCircleInitializationEvents();
    }
    /// <summary>
    /// Callback when GameCircle is initialized and ready to use.
    /// </summary>
    private void ServiceReadyHandler()
    {

      
        initializationStatus = EInitializationStatus.Ready;
        Debug.Log("Dentro ServiceREadyHandler");
        if (AGSPlayerClient.IsSignedIn())
        {
            if (player == null)
            {
                Debug.Log("Dentro service, Request achievements");
                WhisperPlayerScores.Instance.SynchronizeScores();
                RequestLocalPlayerData();
             
             
            }
        }
     

        // Once the callback is received, these events do not need to be subscribed to.
         UnsubscribeFromGameCircleInitializationEvents();


        // Tell the GameCircle plugin the popup information set here.
        // Calling this after GameCircle initialization is safest.
        AGSClient.SetPopUpEnabled(enablePopups);
        AGSClient.SetPopUpLocation(toastLocation);



    }


    public void ShowSignInPage()
    {
        if (AGSClient.IsServiceReady())
        {
            Debug.Log("Show sigin page Service Ready");
            AGSClient.ShowSignInPage();
        }
        else
        {
            Debug.Log("Show sigin page Service NOOOOOOT Ready");

        }
    }

    public void ShowGameCircle()
    {
        if (AGSClient.IsServiceReady())
        {
            AGSClient.ShowGameCircleOverlay();
        }
        else
        {
            Debug.Log("Show sigin page Service NOOOOOOT Ready");

        }
    }

    public void ShowLeaderBoardPage()
    {
        if (AGSClient.IsServiceReady())
        {
            AGSLeaderboardsClient.ShowLeaderboardsOverlay();
        }
        else
        {
            Debug.Log("Show sigin page Service NOOOOOOT Ready");
        }
    }

    public void ShowAchievementsPage()
    {
        if (AGSClient.IsServiceReady())
        {
            AGSAchievementsClient.ShowAchievementsOverlay();
        }
        else
        {
            Debug.Log("Show sigin page Service NOOOOOOT Ready");
        }
    }

    public bool IsPlayerSignedIn()
    {
        return AGSPlayerClient.IsSignedIn();
    }
   

    public void RequestLocalPlayerData()
    {
        // Need to subscribe to callback messages to receive the player from GameCircle.
      
        SubscribeToPlayerEvents();
        // Request the player from the GameCircle plugin
        AGSPlayerClient.RequestLocalPlayer();

        // update the menu to show that the retrieval process has begun.
        playerStatus = playerRetrievingLabel;
    }
    void SubscribeToPlayerEvents()
    {
        AGSPlayerClient.PlayerReceivedEvent += PlayerReceived;
        AGSPlayerClient.PlayerFailedEvent += PlayerFailed;
    }

    /// <summary>
    /// Unsubscribes from GameCircle player events.
    /// </summary>
    void UnsubscribeFromPlayerEvents()
    {
        AGSPlayerClient.PlayerReceivedEvent -= PlayerReceived;
        AGSPlayerClient.PlayerFailedEvent -= PlayerFailed;
    }
    #region Callbacks for Player Stuff
    /// <summary>
    /// Callback for receiving player information.
    /// </summary>
    /// <param name='player'>
    /// GameCircle player information
    /// </param>
    private void PlayerReceived(AGSPlayer player)
    {
        // Update the menu information to show the received player.
        playerStatus = playerReceivedLabel;
        playerStatusMessage = null;
        this.player = player;
        RequestAchievements();  // Get achievements of current player
     
        UnsubscribeFromPlayerEvents();
    }

    /// <summary>
    /// Callback for handling errors attempting to retrieve the local player.
    /// </summary>
    /// <param name='errorMessage'>
    /// Error message.
    /// </param>
    private void PlayerFailed(string errorMessage)
    {
        playerStatus = playerFailedLabel;
        playerStatusMessage = errorMessage;
        this.player = null;
      
        // no longer need to subscribe after a callback has occured.
        UnsubscribeFromPlayerEvents();
    }

    /// <summary>
    /// Raises the signed in state changed event.
    /// </summary>
    /// <param name="isSignedIn">If set to <c>true</c>, the local player is signed in.</param>
    private void OnSignedInStateChanged(Boolean isSignedIn)
    {
        if (isSignedIn)
        {
           Debug.Log("Signedin");
            WhisperPlayerScores.Instance.SynchronizeScores();
            RequestLocalPlayerData();
        
        }
        else
        {
            Debug.Log("NotSignedin");
            achievementList.Clear();
            achievementList = null;
            player = null;
          
           
        }
   
     
    }

    #endregion

    #region LEaderBoards Stuff

    public void SubmitScoreToLeaderboard(string leaderboardId, long scoreValue)
    {
        // Subscribe to the events to receive the score submission status.
        SubscribeToScoreSubmissionEvents();
        // Submit the score update to GameCircle plugin.
        AGSLeaderboardsClient.SubmitScore(leaderboardId, scoreValue);
    }
    private void SubscribeToScoreSubmissionEvents()
    {
        AGSLeaderboardsClient.SubmitScoreFailedEvent += SubmitScoreFailed;
        AGSLeaderboardsClient.SubmitScoreSucceededEvent += SubmitScoreSucceeded;
    }
    private void UnsubscribeFromScoreSubmissionEvents()
    {
        AGSLeaderboardsClient.SubmitScoreFailedEvent -= SubmitScoreFailed;
        AGSLeaderboardsClient.SubmitScoreSucceededEvent -= SubmitScoreSucceeded;
    }
    private void SubmitScoreFailed(string leaderboardId, string error)
    {
        Debug.Log(error);
        UnsubscribeFromScoreSubmissionEvents();
    }

    /// <summary>
    /// Callback for when score submission succeeds.
    /// </summary>
    /// <param name='leaderboardId'>
    /// Leaderboard identifier.
    /// </param>
    private void SubmitScoreSucceeded(string leaderboardId)
    {
      
        UnsubscribeFromScoreSubmissionEvents();
    }
    public void UpdateWhisperScoresAndLeaderboards(string PackName, string LevelName,
       GameType gameType, float currentScore)
    {
        //Obtain current Level info (scores)
        SyncableLevelScore slevel = WhisperPlayerScores.Instance.GetSyncLevelScore(PackName, LevelName, gameType);

        if (slevel == null)
        {
            Debug.Log("Something went wrong, no puede ser nulo, revisar");
            return;
        }
        //Si el score que viene de parametro para ese nivel en ese modo en ese pack, entonces actualizar
        // el score de ese level en el GameDataMap de whisper sync. 
        if (slevel != null && currentScore > slevel.score.BestScore.AsInt())
        {
            Debug.Log("Deberia registrar el score");
            slevel.score.BestScore.Set(currentScore);
            // Revisar si estoy logueado para poder hacer update de leaderboards
            if (AGSPlayerClient.IsSignedIn())
            {
              
                // Update all pack leaderboards 
                foreach (var packName in Globals.Constants.PackNameArray)
                {
                    string packleaderboard = Managers.GameData.GetLeaderBoardName(packName);
                    int packscore = WhisperPlayerScores.Instance.GetPackScore(packName);
                  
                    Managers.GameCircleAmazon.SubmitScoreToLeaderboard(packleaderboard, packscore);
                }
                int globalscore = WhisperPlayerScores.Instance.GetGlobalScore();
                Managers.GameCircleAmazon.SubmitScoreToLeaderboard(Globals.Constants.LeaderBoardGlobal, globalscore);
               

            }

        }
    }

    
    #endregion


    #region Achievements Stuff

    public void UpdateAchievements()
    {
        if (AGSClient.IsServiceReady() && AGSPlayerClient.IsSignedIn())
        {

            if (achievementList != null)
            {

                foreach (AGSAchievement agsAchievement in achievementList)
                {

                    if (agsAchievement.isUnlocked == false)
                    {
                        Debug.Log("Achievement no esta lock" + agsAchievement.id);
                        float progress = Managers.Achievements.AchievementProgress(agsAchievement.id);
                        if (progress > 0)
                        {
                            SubmitAchievement(agsAchievement.id, progress);
                        }
                    }
                }
            }
            else // La lista de achievements no se cargo, tratar de cargarla entonces
            {
                
            }
            
        }

    }
    public void SubmitAchievement(string achievementId, float progress)
    {
       
        // Submit the achievement update to the GameCircle plugin.
        AGSAchievementsClient.UpdateAchievementProgress(achievementId, progress);
      
    }
    /// <summary>
    /// Subscribes to achievement submission events.
    /// </summary>
    private void SubscribeToSubmitAchievementEvents()
    {
        AGSAchievementsClient.UpdateAchievementFailedEvent += UpdateAchievementsFailed;
        AGSAchievementsClient.UpdateAchievementSucceededEvent += UpdateAchievementsSucceeded;
    }

    /// <summary>
    /// Unsubscribes from achievement submission events.
    /// </summary>
    private void UnsubscribeFromSubmitAchievementEvents()
    {
        AGSAchievementsClient.UpdateAchievementFailedEvent -= UpdateAchievementsFailed;
        AGSAchievementsClient.UpdateAchievementSucceededEvent -= UpdateAchievementsSucceeded;
    }
    private void UpdateAchievementsFailed(string achievementId, string error)
    {
       Debug.Log(error);
    }

    
    private void UpdateAchievementsSucceeded(string achievementId)
    {
      Debug.Log("Achievement has been updated ." + achievementId);
    }

    public void RequestAchievements()
    {
        if (AGSPlayerClient.IsSignedIn() == false)
            return;

        // subscribe to the events to receive the achievement list.
        SubscribeToAchievementRequestEvents();
        // request the achievement list from the GameCircle plugin.
        AGSAchievementsClient.RequestAchievements();
      
    }
    private void SubscribeToAchievementRequestEvents()
    {
        AGSAchievementsClient.RequestAchievementsFailedEvent += RequestAchievementsFailed;
        AGSAchievementsClient.RequestAchievementsSucceededEvent += RequestAchievementsSucceeded;
    }

    /// <summary>
    /// Unsubscribes from achievement request events.
    /// </summary>
    private void UnsubscribeFromAchievementRequestEvents()
    {
        AGSAchievementsClient.RequestAchievementsFailedEvent -= RequestAchievementsFailed;
        AGSAchievementsClient.RequestAchievementsSucceededEvent -= RequestAchievementsSucceeded;
    }
    private void RequestAchievementsFailed(string error)
    {
        Debug.Log("Aca en error de achievements" + error);
        achievementList.Clear();
        achievementList = null;
        // Once the callback is received, these events do not need to be subscribed to.
        UnsubscribeFromAchievementRequestEvents();
    }

   
    private void RequestAchievementsSucceeded(List<AGSAchievement> achievements)
    {
    //    Debug.Log("Se recibieron achievements");

        achievementList = achievements;

        UnsubscribeFromAchievementRequestEvents();

    }
    #endregion


 
    void Update()
    {
        if (WhisperInitialized == false)
        {
            if (AGSClient.IsServiceReady())
            {
                Debug.Log("UPdate de amazon");
                WhisperInitialized = true;
                WhisperPlayerScores.Instance.Initialize();
            }


        }

    }

    public void OnApplicationFocus(Boolean focusStatus)
    {


        if (!AGSClient.ReinitializeOnFocus)
        {
            return;
        }

        if (focusStatus)
        {
            Debug.Log("init again");
            //AGSClient.Init(true,true,true);
            InitializeGameCircle();

        }
        else
        {
            Debug.Log("Release again");
            AGSClient.release();
        }
    }
   
}
