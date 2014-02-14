using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

public class AndroidVideoPlayerBinding  : MonoBehaviour {
	private static AndroidJavaObject jo;
	private static AndroidJavaClass jc;
	public static AndroidVideoPlayerBinding instance;
	
	
	void Start () {
		instance = this;
		
		if( ( gameObject != null ) && ( gameObject.name != null ) )
		{
			if( !Application.isEditor )
			{	
				
				jc = new AndroidJavaClass ("com.werplay.androidvideoplugin.CallActivity"); 
				jo = jc.CallStatic <AndroidJavaObject> ("getInstance");
				jo.Call ("setObjectName", gameObject.name);
			}
		}
	}
	
	//Play Video with the given name
	//The video file must be placed in the Assets>Plugins>Android>res>raw folder
	//Leave out the file extension from the video name when calling this function
	//e.g. PlayVideo( video_test ) to play video_test.mp4
	public void PlayVideo( string name )
	{
		Debug.Log( "playVideo" );
	
		if ( !Application.isEditor )
		{
			if(name.Length > 0)
				jo.Call("playVideo", name);
			else
				Debug.Log("Error: invalid file name");
		}	
	}
	
	//To show the Play / Pause button during video playback
	//The arguments x and y set the position of the button on screen
	public void addPlayPauseButton(float x, float y )
	{
		Debug.Log( "Add Play Pause Button" );
		
		if ( !Application.isEditor )
		{
			jo.Call("addPlayPauseButton", x, y);
		}
	}
	
	//Removes the Play/Pause button from screen
	public void removePlayPauseButton()
	{
		Debug.Log( "remove Play Pause Button" );
		
		if ( !Application.isEditor )
		{
			jo.Call("removePlayPauseButton");
		}
	}
	
	//To show the Stop button during video playback
	//The arguments x and y set the position of the button on screen
	public void addStopButton(float x, float y )
	{
		Debug.Log( "addStopButton" );
		
		if ( !Application.isEditor )
		{
			jo.Call("addStopButton", x, y);
		}
	}
	
	//Removes the Stop button from screen
	public void removeStopButton()
	{
		Debug.Log( "removeStopButton" );
		
		if ( !Application.isEditor )
		{
			jo.Call("removeStopButton");
		}
	}
	
	//To show the Skip button during video playback
	//The arguments x and y set the position of the button on screen
	public void addSkipButton(float x, float y )
	{
		Debug.Log( "addSkipButton" );
		
		if ( !Application.isEditor )
		{
			jo.Call("addSkipButton", x, y);
		}
	}
	
	//Removes the Skip button from screen
	public void removeSkipButton()
	{
		Debug.Log( "removeSkipButton" );
		
		if ( !Application.isEditor )
		{
			jo.Call("removeSkipButton");
		}
	}
	
	//To show the Seek Bar during video playback, using given images for the covered and remaining seek bar; and the normal and pressed state of thumb
	//The arguments x and y set the position of the bar on screen. The ( x, y ) coordinate is set as the position of the top left corner of the bar
	public void addSeekingBar(float x, float y){
		Debug.Log( "addSeekingBar" );
		
		if ( !Application.isEditor )
		{
			jo.Call("addSeekingBar", x, y);
		}
	}
	
	//Removes the Seeking bar from screen
	public void removeSeekingBar(){
		Debug.Log( "removeSeekingBar" );
		
		if ( !Application.isEditor )
		{
			jo.Call("removeSeekingBar");
		}
	}

	//Removes all the control buttons from screen
	//Also removes the seek bar	
	public void removeAllButtons()
	{
		if( !Application.isEditor )
		{
			removePlayPauseButton();
			removeSkipButton();
			removeStopButton();
			removeSeekingBar();
		}
	}
	
	#if UNITY_ANDROID
	public static event Action onVideoPlaybackCompleteEvent;
	public static event Action onVideoEndBySkipEvent;

	//Called when the video playback is complete either because the video reached its end
	//or the Skip Button was pushed
	//The argument msg contains:
	//the string "Done" for video reaching its end;
	//the string "Skip" for when the Skip button is tapped
	public void VideoDonePlaying( string msg )
	{
		if( msg == "Done" )
		{
			if( onVideoPlaybackCompleteEvent != null )
			{
				onVideoPlaybackCompleteEvent();
			}
		}
		else if( msg == "Skip" )
		{
			if( onVideoEndBySkipEvent != null )
			{
				onVideoEndBySkipEvent();
			}
		}
	}

	#endif
}
