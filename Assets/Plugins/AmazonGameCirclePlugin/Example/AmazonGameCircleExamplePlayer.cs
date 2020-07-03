/**
 * © 2012-2014 Amazon Digital Services, Inc. All rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"). You may not use this file except in compliance with the License. A copy
 * of the License is located at
 *
 * http://aws.amazon.com/apache2.0/
 *
 * or in the "license" file accompanying this file. This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
 */
using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Amazon GameCircle example implementation of player retrieval.
/// </summary>
public class AmazonGameCircleExamplePlayer : AmazonGameCircleExampleBase {
    
    #region Local variables
    // These strings are updated once
    // player begins retrieval.
    private string playerStatus = null;
    private string playerStatusMessage = null;
    // the player information.
    private AGSPlayer player = null;
    private Boolean haveSubscribedToSignedInStateChangeEvents = false;
    private System.DateTime? lastSignInStateChangeEvent = null;
    private Boolean haveGotStateChangeEvent = false;
    private Boolean signedInStateChange;
    #endregion
    
    #region Local const strings
    // The title of this menu
    private const string playerMenuTitle = "Player";
    // UI labels for player retrieval callbacks.
    private const string playerReceivedLabel = "Retrieved local player data";
    private const string playerFailedLabel = "Failed to retrieve local player data";
    // label for the button that begins player retrieval
    private const string playerRetrieveButtonLabel = "Retrieve local player data";
    // label for displaying player information
    private const string playerLabel = "ID: {0} Alias: {1}\nAvatarUrl: {2}";
    // label for displaying that player retrieval has begun.
    private const string playerRetrievingLabel = "Retrieving local player data...";
    // label for displaying whether the player is signed in.
    private const string isSignedInLabel = "Signed In: {0}";
    // label for showing how long since the player signed in.
    private const string signedInEventLabel = "Player signed in {0,5:N1} seconds ago.";
    // label for showing how long since the player signed out.
    private const string signedOutEventLabel = "Player signed out {0,5:N1} seconds ago.";
    // displaying "null" instead of an empty string looks nicer in the UI
    private const string nullAsString = "null";
    
    #endregion
        
    #region base class implementation
    /// <summary>
    /// The title of the menu.
    /// </summary>
    /// <returns>
    /// The title of the menu.
    /// </returns>
    public override string MenuTitle() {
        return playerMenuTitle;
    }
    /// <summary>
    /// Draws the GameCircle Player Menu. Note that this must be called from an OnGUI function.
    /// </summary>
    public override void DrawMenu() {     
        // Once the Status string is not null, player retrieval has begun.
        // This button begins the player retrieval process.
        if(GUILayout.Button(playerRetrieveButtonLabel)) {
            RequestLocalPlayerData();
        }

        if (!string.IsNullOrEmpty(playerStatus)) {
            AmazonGUIHelpers.CenteredLabel(playerStatus);
            // If there is a status / error message, display it.
            if(!string.IsNullOrEmpty(playerStatusMessage)) {
                AmazonGUIHelpers.CenteredLabel(playerStatusMessage);    
            }
            // player has been received, display it.
            if(null != player) {
                // When the player information is null (for guest accounts), 
                // displaying "null" looks nicer than an empty string
                string playerId = !string.IsNullOrEmpty(player.playerId) ? player.playerId : nullAsString;
                string alias = !string.IsNullOrEmpty(player.alias) ? player.alias : nullAsString;
                string avatarUrl = !string.IsNullOrEmpty(player.avatarUrl) ? player.avatarUrl : nullAsString;
                 
                AmazonGUIHelpers.CenteredLabel(string.Format(playerLabel,playerId, alias, avatarUrl));
            }
        }

        AmazonGUIHelpers.CenteredLabel (string.Format(isSignedInLabel, AGSPlayerClient.IsSignedIn() ? "true" : "false" ));

        // Always listen for signed in state change events.
        if (!haveSubscribedToSignedInStateChangeEvents) {
            AGSPlayerClient.OnSignedInStateChangedEvent += OnSignedInStateChanged;
            haveSubscribedToSignedInStateChangeEvents = true;
        }

        // If a signed in state change event has happened, display when it happened.
        if (haveGotStateChangeEvent && lastSignInStateChangeEvent != null) {
            double timeElapsed = (System.DateTime.Now - lastSignInStateChangeEvent.Value).TotalSeconds;
            if (signedInStateChange) {
                AmazonGUIHelpers.CenteredLabel(string.Format(signedInEventLabel, timeElapsed));
            } else {
                AmazonGUIHelpers.CenteredLabel(string.Format(signedOutEventLabel, timeElapsed));
            }
        }

    }
    #endregion
       
    #region GameCircle plugin functions
    /// <summary>
    /// Requests the local player data from the GameCircle plugin.
    /// </summary>
    void RequestLocalPlayerData() {
        // Need to subscribe to callback messages to receive the player from GameCircle.
        SubscribeToPlayerEvents();
        // Request the player from the GameCircle plugin
        AGSPlayerClient.RequestLocalPlayer();

        // update the menu to show that the retrieval process has begun.
        playerStatus = playerRetrievingLabel;
    }

    /// <summary>
    /// Subscribes to GameCircle player events.
    /// </summary>
    void SubscribeToPlayerEvents() {
        AGSPlayerClient.PlayerReceivedEvent += PlayerReceived;
        AGSPlayerClient.PlayerFailedEvent += PlayerFailed;
    }
    
    /// <summary>
    /// Unsubscribes from GameCircle player events.
    /// </summary>
    void UnsubscribeFromPlayerEvents() {
        AGSPlayerClient.PlayerReceivedEvent -= PlayerReceived;
        AGSPlayerClient.PlayerFailedEvent -= PlayerFailed;
    }

    #endregion

    #region Callbacks
    /// <summary>
    /// Callback for receiving player information.
    /// </summary>
    /// <param name='player'>
    /// GameCircle player information
    /// </param>
    private void PlayerReceived(AGSPlayer player) {
        // Update the menu information to show the received player.
        playerStatus = playerReceivedLabel;
        playerStatusMessage = null;
        this.player = player;
        
        // no longer need to subscribe after a callback has occured.
        UnsubscribeFromPlayerEvents();
    }

    /// <summary>
    /// Callback for handling errors attempting to retrieve the local player.
    /// </summary>
    /// <param name='errorMessage'>
    /// Error message.
    /// </param>
    private void PlayerFailed(string errorMessage) {
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
    private void OnSignedInStateChanged(Boolean isSignedIn) {
        this.haveGotStateChangeEvent = true;
        this.signedInStateChange = isSignedIn;
        this.lastSignInStateChangeEvent = System.DateTime.Now;
    }

    #endregion

}
