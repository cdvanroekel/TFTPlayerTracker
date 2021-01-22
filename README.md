# TFTPlayerTracker
A tool used to track the order via voice commands of who you've played in Team Fight Tactics

The Player Tracker is prompted by the following phrases:
"Track Player [Name]"
"Clear Player List"

For best results, populate the left panel with the player names in the game you are wanting to track.
Once the list is populated, click "Update Player List".

Saying "Track Player [Name]" will add the name to the right column. Saying "Clear Player List" will clear the list on the right column.

If you do not wish to list players, keeping the player list empty or clicking "Update Player List" after deleting all players from the list
will tell the tracker to add whatever it hears/recognizes following "Add Player". This mode is often incredibly inaccurate.

Control Panel > Ease of Access > Speech REcognition > Train your computer to understand you better is not required to use the player tracker,
but it will improve results according to the documentation on the Microsoft Cognitive Speech API.


# Setting up in StreamLabs OBS:

1) Wherever you place the executable, add a file named "TrackedPlayersList.txt" in the same directory.
2) Open Stream Labs and add a new Text(GDI+) Source then Add Source.
3) Toggle "Add a new source instead" name it "Tracker" or whatever you want to call it, then Add Source.
4) In the settings, adjust the looks to what you want, but check the "Read From File" checkbox for the Text property.
5) Point to the "TrackedPlayerList.txt" file you created in step 1.
6) StreamLabs should update this source as the TrackedPlayerList file updates when Players are added/cleared to the right column on the Player Tracker list.
