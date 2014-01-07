using UnityEngine;
using System.Collections;
using FourthSky.Android;
using System.Diagnostics;

public class Settings : MonoBehaviour {
	bool showGui = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTripleTap(TapGesture gesture) { /* your code here */ 
	
		
		UnityEngine.Debug.Log("found triple tap" + gesture.Taps);
		showGui = !showGui;
		
	}
	
	void OnGUI() {
		if (showGui) {
#if UNITY_ANDROID
		
			
		if (GUI.Button(new Rect(950, 10, 20, 20), "X")){
				showGui = false;
			}
			
		if (GUI.Button(new Rect(10, 10, 150, 100), "Settings")) {
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject pm = jo.Call<AndroidJavaObject>("getPackageManager");
			AndroidJavaObject intent = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage", "com.android.settings");
			jo.Call("startActivity", intent);	
			showGui = false;

			
		}
			
		if (GUI.Button(new Rect(10, 150, 150, 100), "Password Manager")) {
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject pm = jo.Call<AndroidJavaObject>("getPackageManager");
			AndroidJavaObject intent = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage", "com.carrotapp.protect");
			jo.Call("startActivity", intent);	
			showGui = false;

			
		}
		if (GUI.Button(new Rect(10, 290, 150, 100), "Trebuchet")) {
		
				
				
			AndroidSystem.SendBroadcast("edu.mit.media.prg.funffilemover.TrebuchetLauncher", new Hashtable());

				
			showGui = false;
	
		}	
					
			
			
/*		if (GUI.Button(new Rect(10, 300, 150, 100), "test video")) {
				
				
			AndroidJavaClass intentClass = new AndroidJavaClass(AndroidSystem.INTENT);
			AndroidJavaObject intentObject = new AndroidJavaObject(AndroidSystem.INTENT);			
			intentObject.Call<AndroidJavaClass>("setAction", intentClass.GetStatic<string>("ACTION_VIEW"));
			intentObject.Call("setDataAndType", "file://mnt/sdcard/Movies/ANTARCTIC_ANTICS4.mp4", "video/mp4");
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject pm = jo.Call<AndroidJavaObject>("getPackageManager");
			jo.Call("startActivity", intentObject);	
			showGui = false; */
//			showGui = false;
//			Handheld.PlayFullScreenMovie ("file:///sdcard/Movies/TINGA_TINGA_2-5.mp4", Color.black, FullScreenMovieControlMode.Full);
//			EtceteraAndroid.playMovie("/sdcard/Movies/ANTARCTIC_ANTICS4.mp4", 0x000000FF, true, EtceteraAndroid.ScalingMode.AspectFit, false);
//		}
			
 
		//GUI.Label(new Rect(10, 300, 150, 100), 	 Application.persistentDataPath + "/" + "test.txt");

//		if (GUI.Button(new Rect(10, 150, 150, 100), "Trebuchet")) {
				
			//AndroidJavaClass runtimeClass = new AndroidJavaClass("java.lang.Runtime");
			//AndroidJavaObject runtimeObject = runtimeClass.CallStatic<AndroidJavaObject>("getRuntime");
			//runtimeObject.Call("exec", "am start -n com.cyanogenmod.trebuchet/com.cyanogenmod.trebuchet.Launcher");				
		//	Process.Start("am", "start -n com.cyanogenmod.trebuchet/com.cyanogenmod.trebuchet.Launcher");
				
	//		showGui = false;
	//	}
		   
#endif        
		}
	}
	

}
