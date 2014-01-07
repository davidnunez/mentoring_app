using UnityEngine;
using System.Collections;

public class SampleListener : MonoBehaviour
{

	void Start ()
	{
		AndroidVideoPlayerBinding.onVideoPlaybackCompleteEvent += VideoPlayBackCompletedNormally;
		AndroidVideoPlayerBinding.onVideoEndBySkipEvent += VideoPlaybackEndedBySkipButton;
	}
	
	void VideoPlayBackCompletedNormally()
	{
		Debug.Log( "Video playback complete" );
	}
	
	void VideoPlaybackEndedBySkipButton()
	{
		Debug.Log( "Video playback ended by Skip Button" );
	}
}
