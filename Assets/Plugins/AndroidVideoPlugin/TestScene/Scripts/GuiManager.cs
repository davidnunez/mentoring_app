using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO; 
public class GuiManager : MonoBehaviour
{
	bool[] tog = new bool[ 5 ];
	float buttonHeight = Screen.height / 5;
	float buttonWidth = ( Screen.width / 2 ) - 20;

	
	void Start()
	{
		tog = new bool[]{ false, false, false, false, true };
	}

	void Update() 
	{
		buttonHeight = Screen.height / 5;
		buttonWidth =  ( Screen.width / 2 ) - 20;
	}
	
	void OnGUI ()
	{
		GUI.color = Color.black;
		float buttonPositionX = 10;
		float buttonPositionY = 10;
		
		GUI.color = Color.white;
		if( GUI.Button( new Rect( buttonPositionX, buttonPositionY, buttonWidth*2, buttonHeight ), "Play Video" ) )
		{
			AndroidVideoPlayerBinding.instance.PlayVideo("video_test");
		}
		
		
		buttonPositionY += buttonHeight + 10;
		buttonPositionX = 10;
		if( !tog[ 0 ] )
		{
			GUI.color = Color.green;
			if( GUI.Button( new Rect( buttonPositionX, buttonPositionY, buttonWidth, buttonHeight ), "Add Play/Pause Button" ) )
			{
					AndroidVideoPlayerBinding.instance.addPlayPauseButton(100,100);
					tog[ 0 ] = !tog[ 0 ];
			}
		}
		else
		{
			GUI.color = Color.red;
			if( GUI.Button( new Rect( buttonPositionX, buttonPositionY, buttonWidth, buttonHeight ), "Remove Play/Pause Button" ) )
			{
				AndroidVideoPlayerBinding.instance.removePlayPauseButton();
				tog[ 0 ] = !tog[ 0 ];
			}
		}
				
		buttonPositionX += buttonWidth + 10;
		if( !tog[ 2 ] )
		{
			GUI.color = Color.green;
			if( GUI.Button( new Rect( buttonPositionX, buttonPositionY, buttonWidth, buttonHeight ), "Add Stop Button" ) )
			{
					AndroidVideoPlayerBinding.instance.addStopButton(100,200);
					tog[ 2 ] = !tog[ 2 ];
			}
		}
		else
		{
			GUI.color = Color.red;
			if( GUI.Button( new Rect( buttonPositionX, buttonPositionY, buttonWidth, buttonHeight ), "Remove Stop Button" ) )
			{
					AndroidVideoPlayerBinding.instance.removeStopButton();
					tog[ 2 ] = !tog[ 2 ];
			}
		}
			
		buttonPositionY += buttonHeight + 10;
		buttonPositionX = 10;
		if( !tog[ 3 ] )
		{
			GUI.color = Color.green;
			if( GUI.Button( new Rect( buttonPositionX, buttonPositionY, buttonWidth, buttonHeight ), "Add Seeking Bar" ) )
			{
					AndroidVideoPlayerBinding.instance.addSeekingBar(100,600);
					tog[ 3 ] = !tog[ 3 ];
			}
		}
		else
		{
			GUI.color = Color.red;
			if( GUI.Button( new Rect( buttonPositionX, buttonPositionY, buttonWidth, buttonHeight ), "Remove Seeking Bar" ) )
			{
					AndroidVideoPlayerBinding.instance.removeSeekingBar();
					tog[ 3 ] = !tog[ 3 ];
			}
		}
		
		buttonPositionX += buttonWidth + 10;
		if( !tog[ 1 ] )
		{
			GUI.color = Color.green;
			if( GUI.Button( new Rect( buttonPositionX, buttonPositionY, buttonWidth, buttonHeight ), "Add Skip Button" ) )
			{
				AndroidVideoPlayerBinding.instance.addSkipButton(100,300);
				tog[ 1 ] = !tog[ 1 ];
			}
		}
		else
		{
			GUI.color = Color.red;
			if( GUI.Button( new Rect( buttonPositionX, buttonPositionY, buttonWidth, buttonHeight ), "Remove Skip Button" ) )
			{
				AndroidVideoPlayerBinding.instance.removeSkipButton();
				tog[ 1 ] = !tog[ 1 ];
			}
		}
	}
}