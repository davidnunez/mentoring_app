ANDROID VIDEO PLAYER PRO
for Unity Android Projects

by weRplay
werplay.com

Please read this read me thoroughly as it contains vital imformation for setting up the project.

1.	Import the package to a Unity Android project.

2.	Modify the Manifest file:
	
	If you do not already have an AndroidManifest file in the Assets>Plugins>Android folder, copy the AndrdoiManifest file from Assets>Plugins>GooglePlayServices and paste into the Assets>Plugins>Android folder. Your manifest file is now ready.

	If you already have an AndroidManifest file in the Assets>Plugins>Android folder, then make the following changes:


	a.	Add the following between the <application .... > and </application> tags in the manifest file:

			<activity
            	android:name="com.werplay.androidvideoplugin.VideoPluginActivity"
            	android:label="@string/app_name" 
            	android:configChanges="fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen">
        	</activity>

	c.	After the additions in the previous step, make sure your manifest file looks similar to the following format:


		....
		....
		....
		<manifest ....
		.... >
			....
			....

			<uses-sdk ....
			.... >
				....
				....

				<application ....
				.... >
					....
					....

					<activity
            			android:name="com.werplay.androidvideoplugin.VideoPluginActivity"
            			android:label="@string/app_name" 
            			android:configChanges="fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen">
        			</activity>

					....
					....

				</application>
			</uses-sdk>
		</manifest>


		Your manifest file is now ready.

3.	Place the video file(s) in the Assets>Plugins>Android>res>raw folder.
	Also keep in mind the following rules:
	
	a.	Make sure that the video file name consisits only of lower case alphabets ( a - z ), numbers ( 0 - 9 ) and / or the underscore ( _ ). Uppercase case characters and spaces are not allowed.

	b.	Only MP4 and 3GP ( H.263, H.264 AVC, MPEG-4 SP ) file formats are supported.
		( http://developer.android.com/guide/appendix/media-formats.html )

4.	Replace the demo button images in the Assets>Plugins>Android>res>drawable folder with your own images, to use them in the video player.
	You can use any PNG file for the button. But you must the same name as the demo images.
	The names are:
	a.	For the Skip Button:	avp_skip_button.png
	b.	For the Play Button:	avp_play_button.png
	c.	For the Pause Button:	avp_pause_button.png
	d.	For the Stop Button:	avp_stop_button.png
	e.	For Seek Bar Track Covered:		avp_seekbar_track_covered.png
	f.	For Seek Bar Track Remaining:	avp_seekbar_track_remaining.png
	g.	For Seek Bar Thumb Down:		avp_seekbar_thumb_down.png
	h.	For Seek Bar Thumb Normal:		avp_seekbar_thumb_normal.png

	You can make other floders besides the "drawable" folder, named drawable-hdpi, drawable-ldpi, drawable-mdpi, drawable-xhdpi and drawable-xxhdpi. You can place different resolution images in these folders to be used for different resolution devices, depending on display resolutions.

		For example:
			res/drawable-mdpi/avp_skip_button.png		// for medium density
			res/drawable-hdpi/avp_skip_button.png		// for high density
			res/drawable-xhdpi/avp_skip_button.png		// for extra high density

5.	Run the demo scene ( Plugins>AndroidVideoPlugin>TestScene>TestScene ) and take a look at the GuiManager.cs and SampleListener.cs scripts ( Plugins>AndroidVideoPlugin>TestScene>Script ) to check out the working of the plugin.

////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////


Usage:
------

1.	Drag and drop the AndroidVideoPlayer prefab from the Project pane ( Assets>Plugins>AndroidVideoPlugin>Prefab>AndroidVideoPlayer ) to the Hierarchy pane in Unity.

2.	Call the functions on this object like following:

		AndroidVideoPlayerBinding.instance.PlayVideo( videoName );

Please note that Unity is paused during video playback and resumes when video playback is finished.


////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////


List of Functions:
------------------

There are 11 functions of note in the AndroidVideoPlayerBinding.cs script:

public void PlayVideo( string name )
	-	Plays a video file of given name
	-	Leave out the file extension from the video name when calling this function
	-	The video file must be placed in the Assets>Plugins>Android>res>raw folder


public void addPlayPauseButton(float x, float y )
	-	To show the Play / Pause button during video playback
	-	The arguments x and y set the position of the button on screen

public void addStopButton( float x, float y )
	-	To show the Stop button during video playback
	-	The arguments x and y set the position of the button on screen

public void addSkipButton( float x, float y )
	-	To show the Skip button during video playback
	-	The arguments x and y set the position of the button on screen

public void addSeekingBar( float x, float y )
	-	To show the Seek Bar during video playback, using given images for the covered and remaining seek bar; and the thumb
	-	The arguments x and y set the position of the bar on screen. The ( x, y ) coordinate is set as the position of the top left corner of the bar

public void removePlayPauseButton()
	-	Removes the Play/Pause button from screen

public void removeSkipButton()
	-	Removes the Skip button from screen

public void removeStopButton()
	-	Removes the Stop button from screenv

public void removeSeekingBar()
	-	Removes the Seeking bar from screen

public void removeAllButtons()
	-	Removes all the control buttons from screen and also removes the seek bar


One of the following events is fired at the end of video playback:

public static event Action onVideoPlaybackCompleteEvent;
public static event Action onVideoEndBySkipEvent;

These events are listed near the end of the IOSVideoPlayerBinding.cs file.
To understand the usage of these events, see the SampleListener.cs script ( in AsAssets>Plugin>IOSVideoPlayer>TestScene>Scripts ).


For further queries or information, please email us at unitysupport@werplay.com